namespace UiApp;

public partial class FavoriteJokesPage : ContentPage
{
    public FavoriteJokesPage(FavoriteJokesViewModel viewModel)
    {
        InitializeComponent();
		BindingContext = viewModel;
    }

	protected override async void OnAppearing()
	{
		base.OnAppearing();
		await ((FavoriteJokesViewModel)this.BindingContext).LoadFavoriteJokesAsync();
	}
}