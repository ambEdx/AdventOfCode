using Days.ViewModels;

namespace Days.Views;

public partial class Day9Page1 : ContentPage
{
	public Day9Page1(Day9ViewModel viewModel)
	{
		InitializeComponent();
        BindingContext = viewModel;
    }
}