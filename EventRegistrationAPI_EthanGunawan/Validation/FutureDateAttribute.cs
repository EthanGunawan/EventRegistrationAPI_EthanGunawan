using System.ComponentModel.DataAnnotations;

namespace EventRegistrationAPI_EthanGunawan.Validation;

// Custom validation attribute that rejects event dates that are not in the future.
public class FutureDateAttribute : ValidationAttribute
{
    public override bool IsValid(object? value)
    {
        if (value is not DateTime eventDate)
        {
            return false;
        }

        // The event must occur after the current date and time.
        return eventDate > DateTime.Now;
    }
}