namespace MonkeyFinder.ViewModel;


// Partial class which is part of generated mvvm code in depedencies/net8.0/analysers/observablepropertygenerator
// Reduces required amount of code
// This partial class is event handler for UI reload to retreive data from server 
public partial class BaseViewModel : ObservableObject
{
    public BaseViewModel()
    {

    }

    [ObservableProperty]                            // links isBusy to generated mvvm code
    [NotifyPropertyChangedFor(nameof(IsNotBusy))]   // links IsNotBusy to generated mvvm code
    bool isBusy;                                    // creates isBusy boolean to define if event is in proccess
    [ObservableProperty]                            // links title to generated mvvm code
    string title;                                   // creates empty string where to store title of application

    public bool IsNotBusy => !isBusy;               // creates IsNotBusy boolean to define if event is not in proccess
}
