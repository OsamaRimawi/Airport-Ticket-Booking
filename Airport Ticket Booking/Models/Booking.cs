namespace Airport_Ticket_Booking.Models;

public class Booking
{
    public int BookingId { get; set; }
    public Flight Flight { get; set; }
    public int PassengerId { get; set; }
    public FlightClass FlightClass { get; set; }
    public double Price { get; set; }

    public Booking(int bookingId, Flight flight, int passengerId, FlightClass flightClass, double price)
    {
        BookingId = bookingId;
        Flight = flight;
        PassengerId = passengerId;
        FlightClass = flightClass;
        Price = price;
    }
}