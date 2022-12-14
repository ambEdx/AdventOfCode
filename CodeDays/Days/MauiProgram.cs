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
                fonts.AddFont("CONSOLA.TTF", "Consolas");
			});

		builder.Services.AddSingleton(s => ActivatorUtilities.CreateInstance<DataService>(s));


        builder.Services.AddSingleton<MainPage>();
        builder.Services.AddSingleton<MainPageViewModel>();
        builder.Services.AddSingleton<Day1Page1>();
		builder.Services.AddSingleton<Day1Page2>();
        builder.Services.AddSingleton<Day1ViewModel>();
        builder.Services.AddSingleton<Day2Page1>();
        builder.Services.AddSingleton<Day2Page2>();
        builder.Services.AddSingleton<Day2ViewModel>();
        builder.Services.AddSingleton<Day3Page1>();
        builder.Services.AddSingleton<Day3Page2>();
        builder.Services.AddSingleton<Day3ViewModel>();
        builder.Services.AddSingleton<Day4Page1>();
        builder.Services.AddSingleton<Day4Page2>();
        builder.Services.AddSingleton<Day4ViewModel>();
        builder.Services.AddSingleton<Day5Page1>();
        builder.Services.AddSingleton<Day5Page2>();
        builder.Services.AddSingleton<Day5ViewModel>();
        builder.Services.AddSingleton<Day6Page1>();
        builder.Services.AddSingleton<Day6Page2>();
        builder.Services.AddSingleton<Day6ViewModel>();
        builder.Services.AddSingleton<Day7Page1>();
        builder.Services.AddSingleton<Day7Page2>();
        builder.Services.AddSingleton<Day7ViewModel>();
        builder.Services.AddSingleton<Day8Page1>();
        builder.Services.AddSingleton<Day8Page2>();
        builder.Services.AddSingleton<Day8ViewModel>();
        builder.Services.AddSingleton<Day9Page1>();
        builder.Services.AddSingleton<Day9Page2>();
        builder.Services.AddSingleton<Day9ViewModel>();
        builder.Services.AddSingleton<Day10Page1>();
        builder.Services.AddSingleton<Day10Page2>();
        builder.Services.AddSingleton<Day10ViewModel>();
        builder.Services.AddSingleton<Day11Page1>();
        builder.Services.AddSingleton<Day11Page2>();
        builder.Services.AddSingleton<Day11ViewModel>();
        builder.Services.AddSingleton<Day12Page1>();
        builder.Services.AddSingleton<Day12Page2>();
        builder.Services.AddSingleton<Day12ViewModel>();

        return builder.Build();

	}

}
