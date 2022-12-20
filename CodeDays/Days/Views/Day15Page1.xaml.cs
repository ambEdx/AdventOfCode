using Days.ViewModels;

namespace Days.Views;

public partial class Day15Page1 : ContentPage
{
	public Day15Page1(Day15ViewModel viewModel)
	{
		InitializeComponent();
        BindingContext = viewModel;
    }
}