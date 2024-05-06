using System.ComponentModel;
using System.Windows.Input;
using Backend.Services;

namespace UiApp;

public class MainPageViewModel : INotifyPropertyChanged
{
    private string _jokeText;
    readonly IJokeService _jokeService;

    public event PropertyChangedEventHandler PropertyChanged;

    public string JokeText
    {
        get => _jokeText;
        set
        {
            if (_jokeText != value)
            {
                _jokeText = value;
                OnPropertyChanged(nameof(JokeText));
            }
        }
    }

    public ICommand NavigateCommand { get; }
    public ICommand FavoriteCommand { get; }
    public ICommand RandomJokeCommand { get; }

    public MainPageViewModel(IJokeService jokeService)
    {
        _jokeService = jokeService;
        Task.Run(UpdateJokeAsync);

        RandomJokeCommand = new Command(async () =>
        {
            await UpdateJokeAsync();
        });
        NavigateCommand = new Command(async () =>
        {
            await Shell.Current.GoToAsync("FavoriteJokes");
        });
        FavoriteCommand = new Command(async () =>
        {
            await _jokeService.AddToFavorites(JokeText);
        });
    }

    public async Task UpdateJokeAsync()
    {
        var joke = await _jokeService.FetchRandomJokeAsync();
        JokeText = joke.Text;
    }

    protected virtual void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
