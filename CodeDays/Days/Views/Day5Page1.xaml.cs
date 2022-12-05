using Days.ViewModels;

namespace Days.Views;

public partial class Day5Page1 : ContentPage
{
	public Day5Page1(Day5ViewModel viewModel)
	{
		InitializeComponent();
        BindingContext = viewModel;
    }
}