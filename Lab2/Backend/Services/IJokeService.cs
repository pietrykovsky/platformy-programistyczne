using System.Collections.ObjectModel;
using Backend.Data.Models;

namespace Backend.Services;

public interface IJokeService
{
    /// <summary>
    /// Fetches a random joke from the API.
    /// </summary>
    /// <returns>Returns a <see cref="Joke"/> object representing the fetched joke.</returns>
    Task<Joke> FetchRandomJokeAsync();

    Task AddToFavorites(string jokeText);

    Task<ObservableCollection<Joke>> GetFavoriteJokesAsync();

    Task RemoveFromFavorites(string jokeText);
}