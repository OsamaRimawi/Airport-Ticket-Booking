using System.ComponentModel.DataAnnotations;
using System.Text;
using Airport_Ticket_Booking.Validation;

namespace Airport_Ticket_Booking.Models;

public record Flight
{
    [Required(ErrorMessage = "Id is required.")]
    public int FlightId { get; init; }

    [Required(ErrorMessage = "Departure Country is required.")]
    public string DepartureCountry { get; init; }

    [Required(ErrorMessage = "Destination Country is required.")]
    public string DestinationCountry { get; init; }

    [FutureDate(ErrorMessage = "Departure date must be in the future.")]
    public DateTime DepartureDate { get; init; }

    [Required(ErrorMessage = "Departure Airport is required.")]
    public string DepartureAirport { get; init; }

    [Required(ErrorMessage = "Arrival Airport is required.")]
    public string ArrivalAirport { get; init; }

    [ValidPrice(ErrorMessage = "Invalid price for one or more flight classes.")]
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