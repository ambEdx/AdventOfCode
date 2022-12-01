using Days.Services;

namespace Days;

public partial class App : Application
{
    public static DataService LocalDataService { get; private set; }
    public App(DataService dataService)
	{
		InitializeComponent();

		MainPage = new AppShell();
        LocalDataService = dataService;
    }
}
