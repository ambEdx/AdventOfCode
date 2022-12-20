using Days.ViewModels;

namespace Days.Views;

public partial class Day15Page2 : ContentPage
{
	public Day15Page2(Day15ViewModel viewModel)
	{
		InitializeComponent();
        BindingContext = viewModel;
    }
}