using Days.ViewModels;

namespace Days.Views;

public partial class Day9Page2 : ContentPage
{
	public Day9Page2(Day9ViewModel viewModel)
	{
		InitializeComponent();
        BindingContext = viewModel;
    }
}