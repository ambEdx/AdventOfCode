using Days.ViewModels;

namespace Days.Views;

public partial class Day11Page2 : ContentPage
{
	public Day11Page2(Day11ViewModel viewModel)
	{
		InitializeComponent();
        BindingContext = viewModel;
    }
}