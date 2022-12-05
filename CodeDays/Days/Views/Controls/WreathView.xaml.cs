namespace Days.Views.Controls;

public partial class WreathView : ContentView
{
	public static readonly BindableProperty DayProperty = BindableProperty.Create(nameof(Day), typeof(int), typeof(WreathView), 0);
	public WreathView()
	{
		InitializeComponent();
	}

	public int Day
	{
		get => (int)GetValue(WreathView.DayProperty);
		set => SetValue(WreathView.DayProperty, value);
	}
}