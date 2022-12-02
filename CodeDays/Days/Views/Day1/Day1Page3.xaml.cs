using Days.ViewModels;

namespace Days.Views.Day1;

public partial class Day1Page3 : ContentPage
{
	public Day1Page3(Day1ViewModel viewModel)
	{
		InitializeComponent();
        BindingContext = viewModel;
    }
}