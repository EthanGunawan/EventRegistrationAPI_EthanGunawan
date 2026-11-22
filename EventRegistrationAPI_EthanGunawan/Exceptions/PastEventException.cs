namespace EventRegistrationAPI_EthanGunawan.Exceptions;

// Thrown when someone tries to register for an event that has already occurred.
public class PastEventException : Exception
{
    public PastEventException()
        : base("Registration is closed for past events.")
    {
    }
}