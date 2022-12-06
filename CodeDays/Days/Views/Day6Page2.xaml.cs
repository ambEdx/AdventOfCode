using AndroidX.Lifecycle;
using Days.ViewModels;

namespace Days.Views;

public partial class Day6Page2 : ContentPage
{
	public Day6Page2(Day6ViewModel viewModel)
	{
		InitializeComponent();
        BindingContext = viewModel;
    }
}