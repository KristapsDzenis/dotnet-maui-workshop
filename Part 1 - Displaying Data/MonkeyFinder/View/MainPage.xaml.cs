namespace MonkeyFinder.View;

public partial class MainPage : ContentPage
{
	// bind monkey view model to main page of app
	public MainPage(MonkeysViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}

