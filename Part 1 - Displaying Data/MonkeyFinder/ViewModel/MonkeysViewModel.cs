using MonkeyFinder.Services;

namespace MonkeyFinder.ViewModel;

// Partial class which inherits BaseViewModel class
// Part of generated mvvm code in depedencies/net8.0/analysers/observablepropertygenerator and relaycommandgenerator
// This class ties together BaseViewModel, Monkey and MonkeyService class and enables downloaded data to be displayed on app
public partial class MonkeysViewModel : BaseViewModel
{
    MonkeyService monkeyService;                                    // creates object of monkeyservice class to be called upon when required

    public ObservableCollection<Monkey> Monkeys { get; } = new();   // data structure type where dynamically can be stored objects of monkey class

    // function which defines app title and ?calls upon monkeyservice object?  runs on launch of app
    public MonkeysViewModel(MonkeyService monkeyService)
    {
        Title = "Monkey Finder";
        this.monkeyService = monkeyService;

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
        }
    }
}
