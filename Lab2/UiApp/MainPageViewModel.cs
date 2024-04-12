using System.ComponentModel;
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

    public MainPageViewModel(IJokeService jokeService)
    {
        _jokeService = jokeService;
        Task.Run(UpdateJokeAsync);
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
