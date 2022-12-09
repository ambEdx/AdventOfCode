using AndroidX.Lifecycle;
using Days.ViewModels;

namespace Days.Views;

public partial class Day8Page2 : ContentPage
{
	public Day8Page2(Day8ViewModel viewModel)
	{
		InitializeComponent();
        BindingContext = viewModel;
    }
}