namespace EventRegistrationAPI_EthanGunawan.Exceptions;

/// <summary>
/// Thrown when the same email tries to register for the same event more than once.
/// The controller converts this exception into a 409 Conflict response.
/// </summary>
public class DuplicateRegistrationException : Exception
{
    public DuplicateRegistrationException()
        : base("This email is already registered for this event.")
    {
    }
}