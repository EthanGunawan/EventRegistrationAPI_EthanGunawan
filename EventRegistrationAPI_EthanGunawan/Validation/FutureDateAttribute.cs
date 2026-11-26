using System.ComponentModel.DataAnnotations;

namespace EventRegistrationAPI_EthanGunawan.Validation;

/// <summary>
/// Custom validation attribute that rejects event dates that are not in the future.
/// </summary>
public class FutureDateAttribute : ValidationAttribute
{
    /// <summary>
    /// Checks whether the supplied event date is after the current date and time.
    /// </summary>
    /// <param name="value">The event date being validated.</param>
    /// <returns>True if the date is in the future; otherwise, false.</returns>
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