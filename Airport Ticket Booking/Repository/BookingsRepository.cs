using Airport_Ticket_Booking.Models;

namespace Airport_Ticket_Booking.Repository;

public class BookingsRepository
{
    private static List<Booking> _bookings;

    public BookingsRepository()
    {
        _bookings = new List<Booking>();
    }

    public List<Booking> GetAllBookings()
    {
        return _bookings;
    }

    public void AddBooking(Booking booking)
    {
        _bookings.Add(booking);
    }

    public bool CancelBooking(int bookingId)
    {
        Booking bookingToRemove = _bookings.FirstOrDefault(booking => booking.BookingId == bookingId);
        if (bookingToRemove != null)
        {
            _bookings.Remove(bookingToRemove);
            return true;
        }
        else
        {
            return false;
        }
    }

    public Booking GetBookingById(int bookingId)
    {
        return _bookings.Find(booking => booking.BookingId == bookingId);
    }

    public List<Booking> GetPersonalBookings(int passengerId)
    {
        return _bookings.Where(booking => booking.PassengerId == passengerId).ToList();
    }
}