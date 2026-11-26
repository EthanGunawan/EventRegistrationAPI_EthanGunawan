namespace EventRegistrationAPI_EthanGunawan.Exceptions;

/// <summary>
/// Thrown when an event has reached its capacity.
/// The controller converts this exception into a 409 Conflict response.
/// </summary>
public class EventFullException : Exception
{
    public EventFullException()
        : base("This event is full.")
    {
    }
}