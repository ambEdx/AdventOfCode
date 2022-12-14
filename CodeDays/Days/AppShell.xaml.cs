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
        Routing.RegisterRoute("Day12Pg1", typeof(Day12Page1));
        Routing.RegisterRoute("Day12Pg2", typeof(Day12Page2));
        Routing.RegisterRoute("Day12Pg3", typeof(Day12Page3));
        Routing.RegisterRoute("Day13Pg1", typeof(Day13Page1));
        Routing.RegisterRoute("Day13Pg2", typeof(Day13Page2));
        Routing.RegisterRoute("Day14Pg1", typeof(Day14Page1));
        Routing.RegisterRoute("Day14Pg2", typeof(Day14Page2));
        Routing.RegisterRoute("Day14Pg3", typeof(Day14Page3));
        Routing.RegisterRoute("Day15Pg1", typeof(Day15Page1));
        Routing.RegisterRoute("Day15Pg2", typeof(Day15Page2));
    }
}
