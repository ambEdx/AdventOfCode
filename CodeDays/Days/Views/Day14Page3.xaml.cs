using Days.ViewModels;

namespace Days.Views;

public partial class Day14Page3 : ContentPage
{
	public Day14Page3(Day14ViewModel viewModel)
	{
		InitializeComponent();
        BindingContext = viewModel;
    }
}