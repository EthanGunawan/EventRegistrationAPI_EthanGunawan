namespace EventRegistrationAPI_EthanGunawan.Exceptions;

/// <summary>
/// Thrown when an event ID does not exist.
/// The controller converts this exception into a 404 Not Found response.
/// </summary>
public class EventNotFoundException : Exception
{
    public EventNotFoundException(int eventId)
        : base($"Event with ID {eventId} was not found.")
    {
    }
}