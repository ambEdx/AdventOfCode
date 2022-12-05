using Days.ViewModels;

namespace Days.Views;

public partial class Day4Page2 : ContentPage
{
	public Day4Page2(Day4ViewModel viewModel)
	{
		InitializeComponent();
        BindingContext = viewModel;
    }
}