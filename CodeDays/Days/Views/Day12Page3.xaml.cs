using Days.ViewModels;

namespace Days.Views;

public partial class Day12Page3 : ContentPage
{
	public Day12Page3(Day12ViewModel viewModel)
	{
		InitializeComponent();
        BindingContext = viewModel;
    }
}