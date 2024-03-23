using System.Globalization;
using Airport_Ticket_Booking.Models;
using Microsoft.VisualBasic.FileIO;

namespace Airport_Ticket_Booking.Repository;

public class FlightRepository
{
    private static List<Flight> _flights;

    public FlightRepository()
    {
        _flights = new List<Flight>();
    }

    public List<Flight> GetAllFlights()
    {
        return _flights;
    }

    public Flight GetFlightById(int flightId)
    {
        return _flights.Find(flight => flight.FlightId == flightId);
    }

    public bool Upload()
    {
        const string filePath = @"Repository\Flight.csv";
        try
        {
            using (TextFieldParser parser = new TextFieldParser(filePath))
            {
                parser.TextFieldType = FieldType.Delimited;
                parser.SetDelimiters(",");
                parser.HasFieldsEnclosedInQuotes = true;

                parser.ReadLine();
                while (!parser.EndOfData)
                {
                    string[] fields = parser.ReadFields();

                    Flight flight = new Flight
                    {
                        FlightId = int.Parse(fields[0]),
                        DepartureCountry = fields[1],
                        DestinationCountry = fields[2],
                        DepartureDate = DateTime.ParseExact(fields[3], "M/d/yyyy HH:mm", CultureInfo.InvariantCulture),
                        DepartureAirport = fields[4],
                        ArrivalAirport = fields[5],
                        ClassesPrices = new Dictionary<FlightClass, double>
                        {
                            { FlightClass.Economy, double.Parse(fields[6]) },
                            { FlightClass.Business, double.Parse(fields[7]) },
                            { FlightClass.FirstClass, double.Parse(fields[8]) }
                        }
                    };

                    _flights.Add(flight);
                }
            }
        }
        catch (Exception e)
        {
            Console.WriteLine("e");
            return false;
        }

        return false;
    }
}