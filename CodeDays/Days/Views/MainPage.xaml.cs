using Days.ViewModels;

namespace Days.Views;

public partial class MainPage : ContentPage
{

	public MainPage(MainPageViewModel viewModel)
	{
		InitializeComponent();
        BindingContext = viewModel;
    }

}

