namespace MonkeyFinder;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();

		// Register route of details page
		Routing.RegisterRoute(nameof(DetailsPage), typeof(DetailsPage));
	}
}