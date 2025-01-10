using System.Text.Json.Serialization;

namespace MonkeyFinder.Model;

// Class to define data for each monkey from Json file in C# and Xamarian code to be able to display in application
public class Monkey
{
    public string Name { get; set; }
    public string Location { get; set; }
    public string Details { get; set; }
    public string Image { get; set; }
    public int Population { get; set; }
    public double Latitude { get; set; }
    public double Longitude { get; set; }
}
