namespace EventRegistrationAPI_EthanGunawan.Exceptions;

// Thrown when the same email tries to register for the same event more than once.
public class DuplicateRegistrationException : Exception
{
    public DuplicateRegistrationException()
        : base("This email is already registered for this event.")
    {
    }
}