using Days.ViewModels;

namespace Days.Views;

public partial class Day11Page1 : ContentPage
{
	public Day11Page1(Day11ViewModel viewModel)
	{
		InitializeComponent();
        BindingContext = viewModel;
    }
}