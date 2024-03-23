using System.Globalization;
using Airport_Ticket_Booking.Models;
using Microsoft.VisualBasic.FileIO;

namespace Airport_Ticket_Booking.Repository;

public class FlightRepository
{
    public List<Flight> GetAllFlights()
    {
        var flights = new List<Flight>();
        string filePath = @"Repository\Flight.csv";
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

                    flights.Add(flight);
                }
            }
        }
        catch (FileNotFoundException e)
        {
            Console.WriteLine("File Not Found");
            throw;
        }

        return flights;
    }
}