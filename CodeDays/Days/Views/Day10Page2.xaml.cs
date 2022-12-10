using AndroidX.Lifecycle;
using Days.ViewModels;

namespace Days.Views;

public partial class Day10Page2 : ContentPage
{
	public Day10Page2(Day10ViewModel viewModel)
	{
		InitializeComponent();
        BindingContext = viewModel;
    }
}