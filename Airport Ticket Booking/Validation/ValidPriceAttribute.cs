using System.ComponentModel.DataAnnotations;
using Airport_Ticket_Booking.Models;

namespace Airport_Ticket_Booking.Validation;

public class ValidPriceAttribute : ValidationAttribute
{
    public override bool IsValid(object value)
    {
        if (value is Dictionary<FlightClass, double> prices)
        {
            return prices.Values.All(price => price >= 0);
        }

        return false;
    }
}