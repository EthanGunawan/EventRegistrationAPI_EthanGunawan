using EventRegistrationAPI_EthanGunawan.DTOs;

namespace EventRegistrationAPI_EthanGunawan.Services;

// Defines the event operations available to controllers.
public interface IEventService
{
    Task<List<EventDto>> GetEventsAsync();
    Task<EventDto?> GetEventByIdAsync(int id);
    Task<EventDto> CreateEventAsync(CreateEventDto dto);
}