namespace EventRegistrationAPI_EthanGunawan.Exceptions;

// Thrown when an event ID does not exist.
public class EventNotFoundException : Exception
{
    public EventNotFoundException(int eventId)
        : base($"Event with ID {eventId} was not found.")
    {
    }
}