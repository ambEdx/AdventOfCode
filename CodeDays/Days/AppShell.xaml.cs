using Days.Views;

namespace Days;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();
        Routing.RegisterRoute("Home", typeof(MainPage));
        Routing.RegisterRoute("Day1", typeof(Day1Page));
    }
}
