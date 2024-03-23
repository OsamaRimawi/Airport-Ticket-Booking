using Airport_Ticket_Booking.Models;

namespace Airport_Ticket_Booking.Repository;

public class BookingsRepository
{
    private List<Booking> _bookings;

    public List<Booking> GetAllBookings()
    {
        return _bookings;
    }

    public void AddBooking(Booking booking)
    {
        _bookings.Add(booking);
    }
}