using EventRegistrationAPI_EthanGunawan.Data;
using EventRegistrationAPI_EthanGunawan.DTOs;
using EventRegistrationAPI_EthanGunawan.Models;
using Microsoft.EntityFrameworkCore;

namespace EventRegistrationAPI_EthanGunawan.Services;

// EventService performs all event database work and DTO mapping.
// Controllers call this service instead of using AppDbContext directly.
public class EventService : IEventService
{
    private readonly AppDbContext _context;

    public EventService(AppDbContext context)
    {
        _context = context;
    }

    // Returns all events sorted by event date.
    public async Task<List<EventDto>> GetEventsAsync()
    {
        var events = await _context.Events
            .Include(e => e.Registrations)
            .OrderBy(e => e.EventDate)
            .ToListAsync();

        return events.Select(MapToDto).ToList();
    }

    // Returns one event, or null when the event does not exist.
    public async Task<EventDto?> GetEventByIdAsync(int id)
    {
        var eventEntity = await _context.Events
            .Include(e => e.Registrations)
            .FirstOrDefaultAsync(e => e.EventId == id);

        return eventEntity == null ? null : MapToDto(eventEntity);
    }

    // Creates a new event from a validated input DTO.
    public async Task<EventDto> CreateEventAsync(CreateEventDto dto)
    {
        var newEvent = new Event
        {
            Title = dto.Title,
            Location = dto.Location,
            EventDate = dto.EventDate,
            Capacity = dto.Capacity
        };

        _context.Events.Add(newEvent);
        await _context.SaveChangesAsync();

        return MapToDto(newEvent);
    }

    // Converts an EF entity into an EventDto and calculates registration values.
    private static EventDto MapToDto(Event eventEntity)
    {
        var registeredCount = eventEntity.Registrations.Count;

        return new EventDto
        {
            EventId = eventEntity.EventId,
            Title = eventEntity.Title,
            Location = eventEntity.Location,
            EventDate = eventEntity.EventDate,
            Capacity = eventEntity.Capacity,
            RegisteredCount = registeredCount,
            SpotsLeft = eventEntity.Capacity - registeredCount
        };
    }
}