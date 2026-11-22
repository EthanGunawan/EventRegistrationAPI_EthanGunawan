using EventRegistrationAPI_EthanGunawan.DTOs;

namespace EventRegistrationAPI_EthanGunawan.Services;

// Defines registration operations available to controllers.
public interface IRegistrationService
{
    Task<RegistrationDto> RegisterAsync(int eventId, CreateRegistrationDto dto);
    Task<bool> DeleteRegistrationAsync(int registrationId);
}