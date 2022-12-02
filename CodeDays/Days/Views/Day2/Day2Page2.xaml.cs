using Days.ViewModels;

namespace Days.Views.Day2;

public partial class Day2Page2 : ContentPage
{
	public Day2Page2(Day2ViewModel viewModel)
	{
		InitializeComponent();
        BindingContext = viewModel;
    }
}