using Days.ViewModels;

namespace Days.Views;

public partial class Day10Page1 : ContentPage
{
	public Day10Page1(Day10ViewModel viewModel)
	{
		InitializeComponent();
        BindingContext = viewModel;
    }
}