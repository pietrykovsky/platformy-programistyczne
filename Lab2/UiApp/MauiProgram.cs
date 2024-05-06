using Microsoft.Extensions.Logging;

using Backend.Services;
using Backend.Data;
using Microsoft.EntityFrameworkCore;

namespace UiApp;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });

        // Register services
        builder.Services.AddSingleton(new HttpClient());
		builder.Services.AddDbContext<JokeContext>(options => options.UseSqlite($"Data Source={Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "jokes.db")}"));
        builder.Services.AddTransient<IJokeService, JokeService>(services =>
        {
            var httpClient = services.GetRequiredService<HttpClient>();
			var context = services.GetRequiredService<JokeContext>();
            return new JokeService(httpClient, context);
        });
        builder.Services.AddSingleton<MainPageViewModel>();
        builder.Services.AddSingleton<MainPage>();
		builder.Services.AddSingleton<FavoriteJokesViewModel>();
        builder.Services.AddSingleton<FavoriteJokesPage>();

#if DEBUG
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
}
