using Days.Services;
using Days.ViewModels;
using Days.Views;
using Days.Views.Day1;
using Days.Views.Day2;

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
        builder.Services.AddTransient<Day1Page1>();
		builder.Services.AddTransient<Day1Page2>();
        builder.Services.AddTransient<Day1Page3>();
        builder.Services.AddSingleton(s => ActivatorUtilities.CreateInstance<Day1ViewModel>(s));
        builder.Services.AddTransient<Day2Page1>();
        builder.Services.AddTransient<Day2Page2>();
        builder.Services.AddSingleton(s => ActivatorUtilities.CreateInstance<Day2ViewModel>(s));

        return builder.Build();
	}
}
