using System.ComponentModel.DataAnnotations;
using Airport_Ticket_Booking.Models;
using Airport_Ticket_Booking.Repository;

namespace Airport_Ticket_Booking.Manager;

public class ManagerService
{
    private readonly BookingsRepository _bookingsRepository;
    private readonly FlightRepository _flightRepository;

    public ManagerService()
    {
        _bookingsRepository = new BookingsRepository();
        _flightRepository = new FlightRepository();
    }

    #region Search_For_Bookings

    public void SearchForBookings()
    {
        Console.WriteLine("******** Bookings search! ********");

        var passengerId = -1;
        var flightId = -1;
        var departureCountry = "";
        var destinationCountry = "";
        var departureDate = DateTime.MinValue;
        var departureAirport = "";
        var arrivalAirport = "";
        double price = -1;
        FlightClass? flightClass = null;

        while (true)
        {
            Console.WriteLine("Do you want add fields for the search? (Y/N)");
            var continueAdding = Console.ReadLine();
            if (continueAdding.ToLower() != "y")
                break;


            Console.WriteLine("Enter Passenger ID: (press Enter to skip)");
            var input = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(input))
            {
                if (!int.TryParse(input, out passengerId))
                {
                    Console.WriteLine("Invalid Passenger ID format!");
                    continue;
                }
            }

            Console.WriteLine("Do you want to continue adding fields? (Y/N)");
            continueAdding = Console.ReadLine();
            if (continueAdding?.ToLower() != "y")
                break;

            Console.WriteLine("Enter Flight ID: (press Enter to skip)");
            input = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(input))
            {
                if (!int.TryParse(input, out flightId))
                {
                    Console.WriteLine("Invalid Flight ID format!");
                    continue;
                }
            }

            Console.WriteLine("Do you want to continue adding fields? (Y/N)");
            continueAdding = Console.ReadLine();
            if (continueAdding?.ToLower() != "y")
                break;

            Console.WriteLine("Enter Departure Country: (press Enter to skip)");
            departureCountry = Console.ReadLine();

            Console.WriteLine("Do you want to continue adding fields? (Y/N)");
            continueAdding = Console.ReadLine();
            if (continueAdding?.ToLower() != "y")
                break;

            Console.WriteLine("Enter Destination Country: (press Enter to skip)");
            destinationCountry = Console.ReadLine();

            Console.WriteLine("Do you want to continue adding fields? (Y/N)");
            continueAdding = Console.ReadLine();
            if (continueAdding?.ToLower() != "y")
                break;

            Console.WriteLine("Enter Departure Date (MM/dd/yyyy HH:mm): (press Enter to skip)");
            input = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(input))
            {
                if (!DateTime.TryParseExact(input, "MM/dd/yyyy HH:mm", null,
                        System.Globalization.DateTimeStyles.None, out departureDate))
                {
                    Console.WriteLine("Invalid date format!");
                    continue;
                }
            }

            Console.WriteLine("Do you want to continue adding fields? (Y/N)");
            continueAdding = Console.ReadLine();
            if (continueAdding?.ToLower() != "y")
                break;

            Console.WriteLine("Enter Departure Airport: (press Enter to skip)");
            departureAirport = Console.ReadLine();

            Console.WriteLine("Do you want to continue adding fields? (Y/N)");
            continueAdding = Console.ReadLine();
            if (continueAdding?.ToLower() != "y")
                break;

            Console.WriteLine("Enter Arrival Airport: (press Enter to skip)");
            arrivalAirport = Console.ReadLine();

            Console.WriteLine("Do you want to continue adding fields? (Y/N)");
            continueAdding = Console.ReadLine();
            if (continueAdding?.ToLower() != "y")
                break;

            Console.WriteLine("Enter Price: (press Enter to skip)");
            input = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(input))
            {
                if (!double.TryParse(input, out price))
                {
                    Console.WriteLine("Invalid Price format!");
                    continue;
                }
            }

            Console.WriteLine("Do you want to continue adding fields? (Y/N)");
            continueAdding = Console.ReadLine();
            if (continueAdding?.ToLower() != "y")
                break;

            Console.WriteLine("Enter Class Type (Economy/Business/FirstClass): (press Enter to skip)");
            input = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(input))
            {
                if (!Enum.TryParse(input, true, out FlightClass addedFlightClass))
                {
                    Console.WriteLine("Invalid class type!");
                    continue;
                }

                flightClass = addedFlightClass;
            }

            break;
        }

        var bookings = _bookingsRepository.GetAllBookings();
        var matchedBookings = bookings.Where(booking =>
        {
            if (passengerId != -1 && booking.PassengerId != passengerId)
                return false;

            if (flightId != -1 && booking.Flight.FlightId != flightId)
                return false;

            if (!string.IsNullOrEmpty(departureCountry) && booking.Flight.DepartureCountry != departureCountry)
                return false;

            if (!string.IsNullOrEmpty(destinationCountry) && booking.Flight.DestinationCountry != destinationCountry)
                return false;

            if (departureDate != DateTime.MinValue && booking.Flight.DepartureDate.Date != departureDate.Date)
                return false;

            if (!string.IsNullOrEmpty(departureAirport) && booking.Flight.DepartureAirport != departureAirport)
                return false;

            if (!string.IsNullOrEmpty(arrivalAirport) && booking.Flight.ArrivalAirport != arrivalAirport)
                return false;

            if (price != -1 && booking.Price != price)
                return false;

            if (flightClass != null && booking.FlightClass != flightClass)
                return false;

            return true;
        }).ToList();

        Console.WriteLine();
        if (matchedBookings.Count == 0)

        {
            Console.WriteLine("No Bookings found matching the criteria.");
        }
        else
        {
            Console.WriteLine($"Found {matchedBookings.Count} Bookings matching the criteria:");
            foreach (var booking in matchedBookings)
            {
                Console.WriteLine(booking.ToString());
                Console.WriteLine();
            }
        }
    }

    #endregion

    #region Upload_Flights

    public void UploadFlights()
    {
        Console.WriteLine("******** Upload Flights! ********");
        if (_flightRepository.UploadFromCsvFile())
        {
            Console.WriteLine("Flights Uploaded Successfully");
        }
    }

    #endregion

    #region Validate_Flights

    public void ValidateFlights()
    {
        Console.WriteLine("******** Validate Flights! ********");
        var flights = _flightRepository.GetAllFlights();
        var flightsToRemove = new List<Flight>();
        foreach (var flight in flights)
        {
            if (Validate(flight, out var results)) continue;
            foreach (var validationResult in results)
            {
                Console.WriteLine(
                    $"Validation error in Flight Id {flight.FlightId}: {validationResult.ErrorMessage}");
            }

            flightsToRemove.Add(flight);
        }

        foreach (var flightToRemove in flightsToRemove)
        {
            _flightRepository.RemoveFlight(flightToRemove);
        }
    }

    private bool Validate(Flight flight, out ICollection<ValidationResult> results)
    {
        var context = new ValidationContext(flight, serviceProvider: null, items: null);
        results = new List<ValidationResult>();

        return Validator.TryValidateObject(flight, context, results, true);
    }

    #endregion
}