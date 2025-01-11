namespace MonkeyFinder.ViewModel;

// Query propert indentifier to identify linked monkey to details page
[QueryProperty("Monkey", "Monkey")]
public partial class MonkeyDetailsViewModel : BaseViewModel
{
    public MonkeyDetailsViewModel() 
    {
        
    }

    [ObservableProperty]        // links monkey to generated mvvm code
    Monkey monkey;
}
