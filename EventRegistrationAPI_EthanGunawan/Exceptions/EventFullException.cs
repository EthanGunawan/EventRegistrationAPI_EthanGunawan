namespace EventRegistrationAPI_EthanGunawan.Exceptions;

// Thrown when an event has reached its capacity.
public class EventFullException : Exception
{
    public EventFullException()
        : base("This event is full.")
    {
    }
}