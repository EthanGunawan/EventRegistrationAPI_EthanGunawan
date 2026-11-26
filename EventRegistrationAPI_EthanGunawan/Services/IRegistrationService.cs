using EventRegistrationAPI_EthanGunawan.DTOs;

namespace EventRegistrationAPI_EthanGunawan.Services;

/// <summary>
/// Defines the registration operations available to controllers.
/// </summary>
public interface IRegistrationService
{
    /// <summary>
    /// Registers a person for an event after checking all business rules.
    /// </summary>
    Task<RegistrationDto> RegisterAsync(int eventId, CreateRegistrationDto dto);

    /// <summary>
    /// Cancels a registration and frees one spot.
    /// Returns false if the registration does not exist.
    /// </summary>
    Task<bool> DeleteRegistrationAsync(int registrationId);
}