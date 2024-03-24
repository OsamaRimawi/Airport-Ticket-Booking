using Airport_Ticket_Booking.Manager;
using Airport_Ticket_Booking.Passenger;

namespace Airport_Ticket_Booking;

class Program
{
    static readonly ManagerService _managerService = new();
    static readonly PassengerService _passengerService = new();

    static void Main(string[] args)
    {
        while (true)
        {
            Console.WriteLine("************     Airport Ticket Booking System      ************");
            Console.WriteLine("********************");
            Console.WriteLine("* Select an action *");
            Console.WriteLine("********************");
            Console.WriteLine("1: Passenger Menu");
            Console.WriteLine("2: Manager Menu");
            Console.WriteLine("3: Close application");
            Console.Write("Enter your choice: ");
            var userSelection = Console.ReadLine();

            switch (userSelection)
            {
                case "1":
                    PassengerMenu();
                    break;
                case "2":
                    ManagerMenu();
                    break;
                case "3":
                    Console.WriteLine("Goodbye!");
                    return;
                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    break;
            }
        }
    }

    static void PassengerMenu()
    {
        while (true)
        {
            Console.WriteLine("********************");
            Console.WriteLine("Passenger Menu");
            Console.WriteLine("********************");
            Console.WriteLine("1: Search Flights");
            Console.WriteLine("2: Book Flight");
            Console.WriteLine("3: Manage Bookings");
            Console.WriteLine("4. Go back to main menu");
            Console.Write("Enter your choice: ");
            var userSelection = Console.ReadLine();
            switch (userSelection)
            {
                case "1":
                    _passengerService.SearchForFlights();
                    break;
                case "2":
                    _passengerService.BookFlight();
                    break;
                case "3":
                    _passengerService.ManageBookings();
                    break;
                case "4":
                    return;
                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    break;
            }
        }
    }

    static void ManagerMenu()
    {
        while (true)
        {
            Console.WriteLine("********************");
            Console.WriteLine("Manager Menu");
            Console.WriteLine("********************");
            Console.WriteLine("1: Search For Bookings");
            Console.WriteLine("2: Upload Flight Data");
            Console.WriteLine("3: Validate Flight Data");
            Console.WriteLine("4. Go back to main menu");
            Console.Write("Enter your choice: ");
            var userSelection = Console.ReadLine();
            switch (userSelection)
            {
                case "1":
                    _managerService.SearchForBookings();
                    break;
                case "2":
                    _managerService.UploadFlights();
                    break;
                case "3":
                    _managerService.ValidateFlights();
                    break;
                case "4":
                    return;
                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    break;
            }
        }
    }
}