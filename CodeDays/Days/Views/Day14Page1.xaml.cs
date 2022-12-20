using Days.ViewModels;

namespace Days.Views;

public partial class Day14Page1 : ContentPage
{
	public Day14Page1(Day14ViewModel viewModel)
	{
		InitializeComponent();
        BindingContext = viewModel;
    }
}