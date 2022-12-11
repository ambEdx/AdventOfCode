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
        Routing.RegisterRoute("Day6Pg1", typeof(Day6Page1));
        Routing.RegisterRoute("Day6Pg2", typeof(Day6Page2));
        Routing.RegisterRoute("Day7Pg1", typeof(Day7Page1));
        Routing.RegisterRoute("Day7Pg2", typeof(Day7Page2));
        Routing.RegisterRoute("Day8Pg1", typeof(Day8Page1));
        Routing.RegisterRoute("Day8Pg2", typeof(Day8Page2));
        Routing.RegisterRoute("Day9Pg1", typeof(Day9Page1));
        Routing.RegisterRoute("Day9Pg2", typeof(Day9Page2));
        Routing.RegisterRoute("Day10Pg1", typeof(Day10Page1));
        Routing.RegisterRoute("Day10Pg2", typeof(Day10Page2));
        Routing.RegisterRoute("Day11Pg1", typeof(Day11Page1));
        Routing.RegisterRoute("Day11Pg2", typeof(Day11Page2));
    }
}
