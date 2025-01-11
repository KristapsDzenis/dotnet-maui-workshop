namespace MonkeyFinder;

public partial class DetailsPage : ContentPage
{
	// bind monkeys details view model to details page
	public DetailsPage(MonkeyDetailsViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}

	// set page code
    protected override void OnNavigatedTo(NavigatedToEventArgs args)
    {
        base.OnNavigatedTo(args);
    }
}