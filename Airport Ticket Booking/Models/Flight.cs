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
}