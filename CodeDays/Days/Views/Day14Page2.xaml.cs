using Days.ViewModels;

namespace Days.Views;

public partial class Day14Page2 : ContentPage
{
	public Day14Page2(Day14ViewModel viewModel)
	{
		InitializeComponent();
        BindingContext = viewModel;
    }
}