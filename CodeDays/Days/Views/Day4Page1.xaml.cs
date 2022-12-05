using Days.ViewModels;

namespace Days.Views;

public partial class Day4Page1 : ContentPage
{
	public Day4Page1(Day4ViewModel viewModel)
	{
		InitializeComponent();
        BindingContext = viewModel;
    }
}