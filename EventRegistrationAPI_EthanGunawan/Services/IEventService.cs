using EventRegistrationAPI_EthanGunawan.DTOs;

namespace EventRegistrationAPI_EthanGunawan.Services;

/// <summary>
/// Defines the event operations available to controllers.
/// </summary>
public interface IEventService
{
    /// <summary>
    /// Returns all events sorted by event date.
    /// </summary>
    Task<List<EventDto>> GetEventsAsync();

    /// <summary>
    /// Returns one event by ID, or null if it does not exist.
    /// </summary>
    Task<EventDto?> GetEventByIdAsync(int id);

    /// <summary>
    /// Creates a new event from a validated input DTO.
    /// </summary>
    Task<EventDto> CreateEventAsync(CreateEventDto dto);

    /// <summary>
    /// Returns all registrations for one event, or null if the event does not exist.
    /// </summary>
    Task<List<RegistrationDto>?> GetRegistrationsForEventAsync(int eventId);
}