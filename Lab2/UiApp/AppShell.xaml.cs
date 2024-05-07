namespace UiApp;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();
        Routing.RegisterRoute("FavoriteJokes", typeof(FavoriteJokesPage));
    }
}
