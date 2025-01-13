using MonkeyFinder.Services;

namespace MonkeyFinder.ViewModel;

// Partial class which inherits BaseViewModel class
// Part of generated mvvm code in depedencies/net8.0/analysers/observablepropertygenerator and relaycommandgenerator
// This class ties together BaseViewModel, Monkey and MonkeyService class and enables downloaded data to be displayed on app
public partial class MonkeysViewModel : BaseViewModel
{
    MonkeyService monkeyService;                                    // creates object of monkeyservice class to be called upon when required

    public ObservableCollection<Monkey> Monkeys { get; } = new();   // data structure type where dynamically can be stored objects of monkey class

    // function to assign title and check availibility of monkey service, internet connectivity, and geolocator on launch of app
    IConnectivity connectivity;
    IGeolocation geolocation;
    public MonkeysViewModel(MonkeyService monkeyService, IConnectivity connectivity, IGeolocation geolocation)
    {
        Title = "Monkey Finder";
        this.monkeyService = monkeyService;
        this.connectivity = connectivity;
        this.geolocation = geolocation;
    }

    [ObservableProperty]            // links isRefreshing to generated mvvm code
    bool isRefreshing;

    // RelayCommand is part pf mvvm generated code which converts asynchroniase function GetClosestMonkeyAsync being callable through event handler
    // Runs each time when get closest monkey  button is clicked on main page 
    [RelayCommand]
    async Task GetClosestMonkeyAsync()
    {
        // If IsBusy or count of monkeys is 0 do nothing
        if(IsBusy || Monkeys.Count == 0) return;
        
        try
        {
            // retreive last known location of the device
            var location = await geolocation.GetLastKnownLocationAsync();
            // if there is no known last location do following
            if(location == null)
            {
                // retreive current location of the device
                location = await geolocation.GetLocationAsync(
                    new GeolocationRequest
                    {
                        DesiredAccuracy = GeolocationAccuracy.Medium,
                        Timeout = TimeSpan.FromSeconds(30),
                    });
            }

            if (location == null) return;           // if there is no location do nothing

            // find closest monkey based on location of the device
            var first = Monkeys.OrderBy(m => location.CalculateDistance(location.Latitude, location.Longitude, DistanceUnits.Kilometers)).FirstOrDefault();

            if(first == null) return;               // if there is no closest monkey do nothing

            // Display error message in app with closest monkey
            await Shell.Current.DisplayAlert("Closest Monkey", $"{first.Name} in {first.Location}", "OK");

        }
        catch (Exception ex)
        {
            // If error happens write error info to console and display error message in app
            Debug.WriteLine(ex);
            await Shell.Current.DisplayAlert("Error!", $"Unable to get closest monkey:{ex.Message}", "OK");
        }
    }


    // RelayCommand is part pf mvvm generated code which converts asynchroniase function GoToDetailsAsync being callable through event handler
    // Runs each time when monkey is clicked on main page and directs to create details page for clicked monkey
    [RelayCommand]
    async Task GoToDetailsAsync(Monkey monkey)
    {
        // if there is no monkey do nothing
        if (monkey == null) return;

        // takes current monkey object and pass it to details page
        await Shell.Current.GoToAsync($"{nameof(DetailsPage)}", true, new Dictionary<string, object>
        {
            {"Monkey", monkey }
        });
    }


    // RelayCommand is part pf mvvm generated code which converts asynchroniase function GetMonkeysAsync being callable through event handler
    // Runs each time button to retreive monkeys is pressed
    [RelayCommand]
    async Task GetMonkeysAsync()
    {
        if (IsBusy) return;     
        // If IsBusy is true do nothing, otherwise try following instructions
        try
        {
            // Display error message in app if there is no internet connection
            if (connectivity.NetworkAccess != NetworkAccess.Internet)
            {
                await Shell.Current.DisplayAlert("Internet Issue", $"Check your internet connection", "OK");
                return;
            }
            
            IsBusy = true;                                  // IsBusy is true
            var monkeys = await monkeyService.GetMonkeys(); // Calls upon monkeyservice object to retreive monkey data and store it in monkeys variable

            // If observable collection of monkeys is not empty it is emptied now
            if(Monkeys.Count != 0)
            {
                Monkeys.Clear();
            }
            // Add each monkey retreived to observable collection of monkeys
            foreach(var monkey in monkeys)
            {
                Monkeys.Add(monkey);
            }
        }
        catch (Exception ex)
        {
            // If error happens write error info to console and display error message in app
            Debug.WriteLine(ex);
            await Shell.Current.DisplayAlert("Error!", $"Unable to get monkeys:{ex.Message}", "OK");
        }
        finally
        {
            IsBusy = false;                         // IsBusy is false
            IsRefreshing = false;                   // IsRefreshing is false
        }
    }
}
