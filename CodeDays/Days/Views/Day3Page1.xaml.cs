using Days.ViewModels;

namespace Days.Views;

public partial class Day3Page1 : ContentPage
{
	public Day3Page1(Day3ViewModel viewModel)
	{
		InitializeComponent();
        BindingContext = viewModel;
    }
}