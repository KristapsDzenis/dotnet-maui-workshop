namespace MonkeyFinder.ViewModel;

// Query propert indentifier to identify linked monkey to details page
[QueryProperty("Monkey", "Monkey")]
public partial class MonkeyDetailsViewModel : BaseViewModel
{
    // function to check availibility of map
    IMap map;
    public MonkeyDetailsViewModel(IMap map) 
    {
        this.map = map;
    }

    [ObservableProperty]        // links monkey to generated mvvm code
    Monkey monkey;

    // RelayCommand is part pf mvvm generated code which converts asynchroniase function OpenMapAsync being callable through event handler
    // Runs each time when get open map  button is clicked on details page 
    [RelayCommand]
    async Task OpenMapAsync()
    {
        try
        {
            // Open external map displaying location of current monkey
            await map.OpenAsync(Monkey.Longitude, Monkey.Latitude,
                new MapLaunchOptions
                {
                    Name = Monkey.Name,
                    NavigationMode = NavigationMode.None,
                });
        }
        catch (Exception ex)
        {
            // If error happens write error info to console and display error message in app
            Debug.WriteLine(ex);
            await Shell.Current.DisplayAlert("Error!", $"Unable to open map:{ex.Message}", "OK");
        }
    }

}
