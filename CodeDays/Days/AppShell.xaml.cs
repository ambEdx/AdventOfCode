using Days.Views;
using Days.Views.Day1;
using Days.Views.Day2;

namespace Days;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();
        Routing.RegisterRoute("Home", typeof(MainPage));
        Routing.RegisterRoute("Day1Pg1", typeof(Day1Page1));
        Routing.RegisterRoute("Day1Pg2", typeof(Day1Page2));
        Routing.RegisterRoute("Day1Pg3", typeof(Day1Page3));
        Routing.RegisterRoute("Day2Pg1", typeof(Day2Page1));
        Routing.RegisterRoute("Day2Pg2", typeof(Day2Page2));
    }
}
