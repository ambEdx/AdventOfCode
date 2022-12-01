using Days.ViewModels;

namespace Days.Views;

public partial class Day1Page : ContentPage
{
	public Day1Page(Day1ViewModel viewModel)
	{
		InitializeComponent();
        BindingContext = viewModel;
    }
}