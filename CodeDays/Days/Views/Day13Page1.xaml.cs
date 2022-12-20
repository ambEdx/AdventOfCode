using Days.ViewModels;

namespace Days.Views;

public partial class Day13Page1 : ContentPage
{
	public Day13Page1(Day13ViewModel viewModel)
	{
		InitializeComponent();
        BindingContext = viewModel;
    }
}