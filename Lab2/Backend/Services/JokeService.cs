using System.Text.Json;
using Backend.Data.Models;

namespace Backend.Services;

public class JokeService : IJokeService
{
    private readonly HttpClient _httpClient;
    private readonly string _apiUrl = "https://icanhazdadjoke.com/";

    public JokeService(HttpClient httpClient)
    {
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

    private class JokeApiResponse
    {
        public string Id { get; set; }
        public string Joke { get; set; }
    }
}

