using EventRegistrationAPI_EthanGunawan.Data;
using EventRegistrationAPI_EthanGunawan.DTOs;
using EventRegistrationAPI_EthanGunawan.Models;
using Microsoft.EntityFrameworkCore;

namespace EventRegistrationAPI_EthanGunawan.Services;

/// <summary>
/// EventService performs all event database work, DTO mapping, and event logic.
/// Controllers call this service instead of using AppDbContext directly.
/// </summary>
public class EventService : IEventService
{
    private readonly AppDbContext _context;

    public EventService(AppDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Returns all events sorted by event date.
    /// </summary>
    public async Task<List<EventDto>> GetEventsAsync()
    {
        // Include registrations so RegisteredCount and SpotsLeft can be calculated.
        var events = await _context.Events
            .Include(e => e.Registrations)
            .OrderBy(e => e.EventDate)
            .ToListAsync();

        return events.Select(MapToDto).ToList();
    }

    /// <summary>
    /// Returns one event by ID, or null if the event does not exist.
    /// </summary>
    public async Task<EventDto?> GetEventByIdAsync(int id)
    {
        var eventEntity = await _context.Events
            .Include(e => e.Registrations)
            .FirstOrDefaultAsync(e => e.EventId == id);

        return eventEntity == null ? null : MapToDto(eventEntity);
    }

    /// <summary>
    /// Creates a new event from a validated input DTO.
    /// </summary>
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

    /// <summary>
    /// Returns all registrations for one event, or null if the event does not exist.
    /// </summary>
    public async Task<List<RegistrationDto>?> GetRegistrationsForEventAsync(int eventId)
    {
        var eventEntity = await _context.Events
            .Include(e => e.Registrations)
            .FirstOrDefaultAsync(e => e.EventId == eventId);

        if (eventEntity == null)
        {
            return null;
        }

        return eventEntity.Registrations
            .Select(r => new RegistrationDto
            {
                RegistrationId = r.RegistrationId,
                EventTitle = eventEntity.Title,
                StudentName = r.StudentName,
                Email = r.Email,
                RegisteredOn = r.RegisteredOn
            })
            .ToList();
    }

    /// <summary>
    /// Maps an Event entity to an EventDto and calculates registration values.
    /// RegisteredCount and SpotsLeft are calculated at runtime and are not stored in the database.
    /// </summary>
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