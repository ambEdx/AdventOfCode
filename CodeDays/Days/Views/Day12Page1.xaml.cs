using Days.ViewModels;

namespace Days.Views;

public partial class Day12Page1 : ContentPage
{
	public Day12Page1(Day12ViewModel viewModel)
	{
		InitializeComponent();
        BindingContext = viewModel;
    }
}