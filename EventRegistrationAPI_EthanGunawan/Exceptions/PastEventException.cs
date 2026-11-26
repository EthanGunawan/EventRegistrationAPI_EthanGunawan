namespace EventRegistrationAPI_EthanGunawan.Exceptions;

/// <summary>
/// Thrown when someone tries to register for an event that has already occurred.
/// The controller converts this exception into a 400 Bad Request response.
/// </summary>
public class PastEventException : Exception
{
    public PastEventException()
        : base("Registration is closed for past events.")
    {
    }
}