using System.Collections.ObjectModel;
using System.Windows.Input;
using Backend.Data.Models;
using Backend.Services;

public class FavoriteJokesViewModel
{
    private readonly IJokeService _jokeService;
    private string _searchText;

    public ObservableCollection<Joke> FavoriteJokes { get; } = new();
    public ObservableCollection<Joke> FilteredJokes { get; } = new();

    public string SearchText
    {
        get => _searchText;
        set
        {
            _searchText = value;
            PerformSearch();
        }
    }

    public ICommand PerformSearchCommand => new Command(PerformSearch);

    public FavoriteJokesViewModel(IJokeService jokeService)
    {
        _jokeService = jokeService;
        LoadFavoriteJokesAsync();
    }

    private async void PerformSearch()
    {
        if (string.IsNullOrWhiteSpace(SearchText))
        {
            foreach (var joke in FavoriteJokes)
                if (!FilteredJokes.Contains(joke))
                    FilteredJokes.Add(joke);
        }
        else
        {
            var filtered = FavoriteJokes.Where(j => j.Text.ToLowerInvariant().Contains(SearchText.ToLowerInvariant())).ToList();
            FilteredJokes.Clear();
            foreach (var joke in filtered)
                FilteredJokes.Add(joke);
        }
    }

    public async Task LoadFavoriteJokesAsync()
    {
        FavoriteJokes.Clear();
        var favoriteJokes = await _jokeService.GetFavoriteJokesAsync();
        foreach (var joke in favoriteJokes)
        {
            FavoriteJokes.Add(joke);
            FilteredJokes.Add(joke);
        }
    }
}
