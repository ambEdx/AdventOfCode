using Days.Views;

namespace Days;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();
        Routing.RegisterRoute("Home", typeof(MainPage));
        Routing.RegisterRoute("Day1Pg1", typeof(Day1Page1));
        Routing.RegisterRoute("Day1Pg2", typeof(Day1Page2));
        Routing.RegisterRoute("Day2Pg1", typeof(Day2Page1));
        Routing.RegisterRoute("Day2Pg2", typeof(Day2Page2));
        Routing.RegisterRoute("Day3Pg1", typeof(Day3Page1));
        Routing.RegisterRoute("Day3Pg2", typeof(Day3Page2));
        Routing.RegisterRoute("Day4Pg1", typeof(Day4Page1));
        Routing.RegisterRoute("Day4Pg2", typeof(Day4Page2));
        Routing.RegisterRoute("Day5Pg1", typeof(Day5Page1));
        Routing.RegisterRoute("Day5Pg2", typeof(Day5Page2));
    }
}
