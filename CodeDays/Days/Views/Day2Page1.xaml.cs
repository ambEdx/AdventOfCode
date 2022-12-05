using Days.ViewModels;

namespace Days.Views;

public partial class Day2Page1 : ContentPage
{
	public Day2Page1(Day2ViewModel viewModel)
	{
		InitializeComponent();
        BindingContext = viewModel;
    }
}