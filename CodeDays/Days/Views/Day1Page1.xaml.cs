using Days.ViewModels;

namespace Days.Views
	;

public partial class Day1Page1 : ContentPage
{
	public Day1Page1(Day1ViewModel viewModel)
	{
		InitializeComponent();
        BindingContext = viewModel;
    }
}