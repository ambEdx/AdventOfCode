using Days.Services;
using Days.ViewModels;
using Days.Views;

namespace Days;

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

		builder.Services.AddSingleton(s => ActivatorUtilities.CreateInstance<DataService>(s));


        builder.Services.AddTransient<MainPage>();
        builder.Services.AddTransient<MainPageViewModel>();
        builder.Services.AddTransient<Day1Page>();
        builder.Services.AddTransient<Day1ViewModel>();

        return builder.Build();
	}
}
