using Days.ViewModels;

namespace Days.Views;

public partial class Day3Page2 : ContentPage
{
	public Day3Page2(Day3ViewModel viewModel)
	{
		InitializeComponent();
        BindingContext = viewModel;
    }
}