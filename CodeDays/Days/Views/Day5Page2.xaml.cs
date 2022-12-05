using AndroidX.Lifecycle;
using Days.ViewModels;

namespace Days.Views;

public partial class Day5Page2 : ContentPage
{
	public Day5Page2(Day5ViewModel viewModel)
	{
		InitializeComponent();
        BindingContext = viewModel;
    }
}