using System.Net.Http.Json;

namespace MonkeyFinder.Services;


// MonkeyService class to connect to server and retreive monkey data
public class MonkeyService
{
    HttpClient client;      // http client
    
    // function to define http client as new http client
    public MonkeyService()
    {
        client = new HttpClient();
    }

    List<Monkey> monkeyList = new();        //  new list of monkeys
    // asynchrnous function to retreive monkeys from server, Runs each time button to retreive monkeys is pressed
    public async Task<List<Monkey>> GetMonkeys()
    {   
        // connect to server and wait for respnsonse
        var url = "https://montemagno.com/monkeys.json";
        var response = await client.GetAsync(url);

        // if response successful read data from json file stored on server into monkey list
        if (response.IsSuccessStatusCode)
        {
            monkeyList = await response.Content.ReadFromJsonAsync<List<Monkey>>();
        }
        
        return monkeyList;      // return monkey list
    }
}
