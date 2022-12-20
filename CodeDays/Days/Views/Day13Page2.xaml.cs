using Days.ViewModels;

namespace Days.Views;

public partial class Day13Page2 : ContentPage
{
	public Day13Page2(Day13ViewModel viewModel)
	{
		InitializeComponent();
        BindingContext = viewModel;
    }
}