using System.Collections.ObjectModel;
using System.Text.Json;
using Backend.Data;
using Backend.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Backend.Services;

public class JokeService : IJokeService
{
    private readonly HttpClient _httpClient;
    private readonly string _apiUrl = "https://icanhazdadjoke.com/";
    private readonly JokeContext _context;


    public JokeService(HttpClient httpClient, JokeContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        _httpClient.DefaultRequestHeaders.Add("Accept", "application/json");
    }

    public async Task<Joke> FetchRandomJokeAsync()
    {
        try
        {
            HttpResponseMessage response = await _httpClient.GetAsync(_apiUrl);
            response.EnsureSuccessStatusCode(); // Throws exception for non-success status codes

            var content = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            var jokeData = JsonSerializer.Deserialize<JokeApiResponse>(content, options);

            if (jokeData == null || string.IsNullOrEmpty(jokeData.Joke))
                throw new Exception("Invalid joke data received.");

            return new Joke
            {
                Text = jokeData.Joke,
                ApiId = jokeData.Id,
                CreatedAt = DateTime.Now
            };
        }
        catch (Exception ex)
        {
            // Log exception details here
            throw new Exception($"Failed to fetch joke due to: {ex.Message}", ex);
        }
    }

    public async Task AddToFavorites(string jokeText)
    {
        // Check if the joke exists in the database
        var joke = await _context.Jokes.FirstOrDefaultAsync(j => j.Text == jokeText);

        // If not, add it
        if (joke == null)
        {
            joke = new Joke
            {
                Text = jokeText,
                ApiId = Guid.NewGuid().ToString(),  // Assuming you have a way to uniquely identify jokes
                CreatedAt = DateTime.Now
            };
            _context.Jokes.Add(joke);
            await _context.SaveChangesAsync();
        }

        // Check if already favorited
        var favorite = await _context.Favourites.FirstOrDefaultAsync(f => f.JokeId == joke.Id);
        if (favorite == null)
        {
            favorite = new Favourite
            {
                JokeId = joke.Id,
                FavouritedAt = DateTime.Now
            };
            _context.Favourites.Add(favorite);
            await _context.SaveChangesAsync();
        }
    }

    public Task<ObservableCollection<Joke>> GetFavoriteJokesAsync()
    {
        // Fetch favorite jokes from the database
        var favoriteJokes = _context.Favourites
            .Include(f => f.Joke)
            .Select(f => f.Joke)
            .ToList();

        return Task.FromResult(new ObservableCollection<Joke>(favoriteJokes));
    
    }

    public Task RemoveFromFavorites(string jokeText)
    {
        // Find the joke in the database
        var joke = _context.Jokes.FirstOrDefault(j => j.Text == jokeText);
        if (joke == null)
            return Task.CompletedTask;

        // Find the favorite entry
        var favorite = _context.Favourites.FirstOrDefault(f => f.JokeId == joke.Id);
        if (favorite == null)
            return Task.CompletedTask;

        // Remove the favorite entry
        _context.Favourites.Remove(favorite);
        _context.SaveChanges();

        return Task.CompletedTask;
    }


    private class JokeApiResponse
    {
        public string Id { get; set; }
        public string Joke { get; set; }
    }
}

