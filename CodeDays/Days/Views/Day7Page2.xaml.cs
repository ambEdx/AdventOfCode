using Days.ViewModels;

namespace Days.Views;

public partial class Day7Page2 : ContentPage
{
	public Day7Page2(Day7ViewModel viewModel)
	{
		InitializeComponent();
        BindingContext = viewModel;
    }
}