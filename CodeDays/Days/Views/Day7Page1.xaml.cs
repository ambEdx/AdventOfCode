using Days.ViewModels;

namespace Days.Views;

public partial class Day7Page1 : ContentPage
{
	public Day7Page1(Day7ViewModel viewModel)
	{
		InitializeComponent();
        BindingContext = viewModel;
    }
}