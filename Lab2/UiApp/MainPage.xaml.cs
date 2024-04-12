namespace UiApp;

public partial class MainPage : ContentPage
{
    public MainPage(MainPageViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }

    private async void OnJokeClicked(object sender, EventArgs e)
    {
        await (BindingContext as MainPageViewModel).UpdateJokeAsync();
    }
}

