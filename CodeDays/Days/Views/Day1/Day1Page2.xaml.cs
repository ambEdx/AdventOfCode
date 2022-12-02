using Days.ViewModels;

namespace Days.Views.Day1;

public partial class Day1Page2 : ContentPage
{
	public Day1Page2(Day1ViewModel viewModel)
	{
		InitializeComponent();
        BindingContext = viewModel;
    }
}