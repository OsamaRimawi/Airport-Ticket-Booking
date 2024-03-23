using Airport_Ticket_Booking.Models;
using Airport_Ticket_Booking.Repository;

namespace Airport_Ticket_Booking.Passenger;

public class PassengerService
{
    private BookingsRepository _bookingsRepository;
    private FlightRepository _flightRepository;

    public PassengerService()
    {
        _bookingsRepository = new BookingsRepository();
        _flightRepository = new FlightRepository();
    }

    public void BookFlight()
    {
        Console.WriteLine("******** Flight Booking! ********");
        Console.WriteLine("Enter Flight ID:");
        if (!int.TryParse(Console.ReadLine(), out var flightId))
        {
            Console.WriteLine("Invalid Flight ID format!");
            return;
        }

        var flight = _flightRepository.GetFlightById(flightId);
        if (flight == null)
        {
            Console.WriteLine("Flight not found!");
            return;
        }

        Console.WriteLine("Enter Passenger ID:");
        if (!int.TryParse(Console.ReadLine(), out var passengerId))
        {
            Console.WriteLine("Invalid Passenger ID format!");
            return;
        }

        Console.WriteLine("Enter Class Type (Economy/Business/FirstClass):");
        if (!Enum.TryParse(Console.ReadLine(), true, out FlightClass flightClass))
        {
            Console.WriteLine("Invalid class type!");
            return;
        }

        Booking newBooking = new Booking(flight, passengerId, flightClass);

        _bookingsRepository.AddBooking(newBooking);
        Console.WriteLine("Booking successful!");
    }

    public void SearchForFlights()
    {
        Console.WriteLine("******** Flight search! ********");

        var departureCountry = "";
        var destinationCountry = "";
        var departureDate = DateTime.MinValue;
        var departureAirport = "";
        var arrivalAirport = "";

        while (true)
        {
            Console.WriteLine("Do you want add fields for the search? (Y/N)");
            string continueAdding = Console.ReadLine();
            if (continueAdding.ToLower() != "y")
                break;

            Console.WriteLine("Enter Departure Country:");
            departureCountry = Console.ReadLine();

            Console.WriteLine("Do you want to continue adding fields? (Y/N)");
            continueAdding = Console.ReadLine();
            if (continueAdding.ToLower() != "y")
                break;

            Console.WriteLine("Enter Destination Country:");
            destinationCountry = Console.ReadLine();

            Console.WriteLine("Do you want to continue adding fields? (Y/N)");
            continueAdding = Console.ReadLine();
            if (continueAdding.ToLower() != "y")
                break;

            Console.WriteLine("Enter Departure Date (MM/dd/yyyy HH:mm):");
            if (!DateTime.TryParseExact(Console.ReadLine(), "MM/dd/yyyy HH:mm", null,
                    System.Globalization.DateTimeStyles.None, out departureDate))
            {
                Console.WriteLine("Invalid date format!");
                continue;
            }

            Console.WriteLine("Do you want to continue adding fields? (Y/N)");
            continueAdding = Console.ReadLine();
            if (continueAdding.ToLower() != "y")
                break;

            Console.WriteLine("Enter Departure Airport:");
            departureAirport = Console.ReadLine();

            Console.WriteLine("Do you want to continue adding fields? (Y/N)");
            continueAdding = Console.ReadLine();
            if (continueAdding.ToLower() != "y")
                break;

            Console.WriteLine("Enter Arrival Airport:");
            arrivalAirport = Console.ReadLine();
            break;
        }

        var flights = _flightRepository.GetAllFlights();
        var matchedFlights = flights.Where(flight =>
        {
            if (!string.IsNullOrEmpty(departureCountry) && flight.DepartureCountry != departureCountry)
                return false;

            if (!string.IsNullOrEmpty(destinationCountry) && flight.DestinationCountry != destinationCountry)
                return false;

            if (departureDate != DateTime.MinValue && flight.DepartureDate.Date != departureDate.Date)
                return false;

            if (!string.IsNullOrEmpty(departureAirport) && flight.DepartureAirport != departureAirport)
                return false;

            if (!string.IsNullOrEmpty(arrivalAirport) && flight.ArrivalAirport != arrivalAirport)
                return false;

            return true;
        }).ToList();

        Console.WriteLine();
        if (matchedFlights.Count == 0)

        {
            Console.WriteLine("No flights found matching the criteria.");
        }
        else
        {
            Console.WriteLine($"Found {matchedFlights.Count} flights matching the criteria:");
            foreach (var flight in matchedFlights)
            {
                Console.WriteLine(flight.ToString());
                Console.WriteLine();
            }
        }
    }

    public void ManageBookings()
    {
        Console.WriteLine("******** Manage Bookings ********");
        Console.WriteLine("Enter Passenger Id:");
        if (!int.TryParse(Console.ReadLine(), out var passengerId))
        {
            Console.WriteLine("Invalid Passenger Id!");
            return;
        }

        int choice;
        do
        {
            Console.WriteLine("1. Cancel a Booking");
            Console.WriteLine("2. Modify a Booking");
            Console.WriteLine("3. View Personal Bookings");
            Console.WriteLine("4. Go Back");
            Console.WriteLine("Enter your choice (1-4):");

            if (!int.TryParse(Console.ReadLine(), out choice))
            {
                Console.WriteLine("Invalid choice!");
                return;
            }

            switch (choice)
            {
                case 1:
                    CancelBooking(passengerId);
                    break;
                case 2:
                    ModifyBooking(passengerId);
                    break;
                case 3:
                    ViewPersonalBookings(passengerId);
                    break;
            }
        } while (choice != 4);
    }

    private void CancelBooking(int passengerId)
    {
        Console.WriteLine("******** Cancel Booking ********");
        Console.WriteLine("Enter Booking ID to cancel:");

        if (!int.TryParse(Console.ReadLine(), out int bookingId))
        {
            Console.WriteLine("Invalid Booking ID format!");
            return;
        }

        Booking booking = _bookingsRepository.GetBookingById(bookingId);
        if (booking == null)
        {
            Console.WriteLine("Booking not found!");
            return;
        }

        if (booking.PassengerId != passengerId)
        {
            Console.WriteLine("You are not authorized to cancel this booking.");
            return;
        }

        Console.WriteLine(_bookingsRepository.CancelBooking(bookingId)
            ? "Booking canceled successfully!"
            : "Booking not found!");
    }

    private void ModifyBooking(int passengerId)
    {
        Console.WriteLine("******** Modify Booking ********");
        Console.WriteLine("Enter Booking ID to modify:");

        if (!int.TryParse(Console.ReadLine(), out int bookingId))
        {
            Console.WriteLine("Invalid Booking ID format!");
            return;
        }

        Booking booking = _bookingsRepository.GetBookingById(bookingId);
        if (booking == null)
        {
            Console.WriteLine("Booking not found!");
            return;
        }

        if (booking.PassengerId != passengerId)
        {
            Console.WriteLine("You are not authorized to modify this booking.");
            return;
        }

        Console.WriteLine($"Current Flight ID: {booking.Flight.FlightId}");
        Console.WriteLine("Enter new Flight ID (press Enter to keep current):");
        string newFlightIdInput = Console.ReadLine();
        if (!string.IsNullOrWhiteSpace(newFlightIdInput))
        {
            if (int.TryParse(newFlightIdInput, out int newFlightId))
            {
                var newFlight = _flightRepository.GetFlightById(newFlightId);
                if (newFlight == null)
                {
                    Console.WriteLine("Flight not found!");
                    return;
                }
                else
                {
                    booking.Flight = newFlight;
                    Console.WriteLine("Flight Updated!");
                }
            }
            else
            {
                Console.WriteLine("Invalid Flight ID format!");
                return;
            }
        }

        Console.WriteLine($"Current Class Type: {booking.FlightClass}");
        Console.WriteLine("Enter new Class Type (Economy/Business/FirstClass) (press Enter to keep current):");
        var classTypeInput = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(classTypeInput)) return;
        if (Enum.TryParse(classTypeInput, true, out FlightClass newClassType))
        {
            booking.FlightClass = newClassType;
            booking.Price = booking.Flight.ClassesPrices[newClassType];
            Console.WriteLine("Flight Class and Price Updated!");
        }
        else
        {
            Console.WriteLine("Invalid class type!");
        }
    }

    private void ViewPersonalBookings(int passengerId)
    {
        Console.WriteLine("******** View Personal Bookings ********");
        List<Booking> personalBookings = _bookingsRepository.GetPersonalBookings(passengerId);
        if (personalBookings.Any())
        {
            Console.WriteLine("Personal Bookings:");
            foreach (var booking in personalBookings)
            {
                Console.WriteLine(booking.ToString());
                Console.WriteLine();
            }
        }
        else
        {
            Console.WriteLine("No personal bookings found!");
        }
    }
}