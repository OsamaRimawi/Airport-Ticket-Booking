using System.Text;

namespace Airport_Ticket_Booking.Models;

public record Flight
{
    public int FlightId { get; init; }
    public string DepartureCountry { get; init; }
    public string DestinationCountry { get; init; }
    public DateTime DepartureDate { get; init; }
    public string DepartureAirport { get; init; }
    public string ArrivalAirport { get; init; }
    public Dictionary<FlightClass, double> ClassesPrices { get; init; }

    public override string ToString()
    {
        StringBuilder sb = new StringBuilder();
        sb.AppendLine($"Flight ID: {FlightId}");
        sb.AppendLine($"Departure Country: {DepartureCountry}");
        sb.AppendLine($"Destination Country: {DestinationCountry}");
        sb.AppendLine($"Departure Date: {DepartureDate}");
        sb.AppendLine($"Departure Airport: {DepartureAirport}");
        sb.AppendLine($"Arrival Airport: {ArrivalAirport}");

        foreach (var kvp in ClassesPrices)
        {
            sb.AppendLine($"Price for {kvp.Key}: {kvp.Value}");
        }

        return sb.ToString();
    }
}