using Days.ViewModels;

namespace Days.Views;

public partial class Day12Page2 : ContentPage
{
	public Day12Page2(Day12ViewModel viewModel)
	{
		InitializeComponent();
        BindingContext = viewModel;
    }
}