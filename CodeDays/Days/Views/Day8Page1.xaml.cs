using Days.ViewModels;

namespace Days.Views;

public partial class Day8Page1 : ContentPage
{
	public Day8Page1(Day8ViewModel viewModel)
	{
		InitializeComponent();
        BindingContext = viewModel;
    }
}