using System.Text;

namespace Airport_Ticket_Booking.Models;

public class Booking
{
    private static int _lastBookingId = 0;
    public int BookingId { get; set; }
    public Flight Flight { get; set; }
    public int PassengerId { get; set; }
    public FlightClass FlightClass { get; set; }
    public double Price { get; set; }

    public Booking(Flight flight, int passengerId, FlightClass flightClass)
    {
        BookingId = ++_lastBookingId;
        Flight = flight;
        PassengerId = passengerId;
        FlightClass = flightClass;
        Price = flight.ClassesPrices[flightClass];
    }

    public override string ToString()
    {
        StringBuilder sb = new StringBuilder();
        sb.AppendLine($"Booking ID: {BookingId}");
        sb.AppendLine($"Passenger ID: {PassengerId}");
        sb.AppendLine($"Flight ID: {Flight.FlightId}");
        sb.AppendLine($"Flight Class: {FlightClass}");
        sb.AppendLine($"Price: {Price}");
        sb.AppendLine($"Departure Country: {Flight.DepartureCountry}");
        sb.AppendLine($"Destination Country: {Flight.DestinationCountry}");
        sb.AppendLine($"Departure Date: {Flight.DepartureDate}");
        sb.AppendLine($"Departure Airport: {Flight.DepartureAirport}");
        sb.AppendLine($"Arrival Airport: {Flight.ArrivalAirport}");
        return sb.ToString();
    }
}