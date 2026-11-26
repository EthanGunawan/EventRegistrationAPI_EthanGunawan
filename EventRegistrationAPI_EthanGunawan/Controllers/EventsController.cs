using EventRegistrationAPI_EthanGunawan.DTOs;
using EventRegistrationAPI_EthanGunawan.Exceptions;
using EventRegistrationAPI_EthanGunawan.Services;
using Microsoft.AspNetCore.Mvc;

namespace EventRegistrationAPI_EthanGunawan.Controllers;

/// <summary>
/// EventsController handles event and event-registration endpoints.
/// It calls the service layer and does not use AppDbContext directly.
/// </summary>
[ApiController]
[Route("api/events")]
public class EventsController : ControllerBase
{
    private readonly IEventService _eventService;
    private readonly IRegistrationService _registrationService;

    // Constructor injection provides the services needed by this controller.
    public EventsController(
        IEventService eventService,
        IRegistrationService registrationService)
    {
        _eventService = eventService;
        _registrationService = registrationService;
    }

    /// <summary>
    /// GET /api/events
    /// Returns all events sorted by date.
    /// </summary>
    [HttpGet]
    public async Task<ActionResult<List<EventDto>>> GetEvents()
    {
        var events = await _eventService.GetEventsAsync();

        return Ok(events);
    }

    /// <summary>
    /// GET /api/events/{id}
    /// Returns one event, or 404 Not Found if it does not exist.
    /// </summary>
    [HttpGet("{id}")]
    public async Task<ActionResult<EventDto>> GetEvent(int id)
    {
        var eventDto = await _eventService.GetEventByIdAsync(id);

        if (eventDto == null)
        {
            return NotFound($"Event with ID {id} was not found.");
        }

        return Ok(eventDto);
    }

    /// <summary>
    /// POST /api/events
    /// Creates a new event and returns 201 Created.
    /// </summary>
    [HttpPost]
    public async Task<ActionResult<EventDto>> CreateEvent(CreateEventDto dto)
    {
        var createdEvent = await _eventService.CreateEventAsync(dto);

        // CreatedAtAction returns 201 Created and includes a URL to the new event.
        return CreatedAtAction(
            nameof(GetEvent),
            new { id = createdEvent.EventId },
            createdEvent
        );
    }

    /// <summary>
    /// GET /api/events/{id}/registrations
    /// Returns all registrations for one event, or 404 if the event does not exist.
    /// </summary>
    [HttpGet("{id}/registrations")]
    public async Task<ActionResult<List<RegistrationDto>>> GetRegistrations(int id)
    {
        var registrations = await _eventService.GetRegistrationsForEventAsync(id);

        if (registrations == null)
        {
            return NotFound($"Event with ID {id} was not found.");
        }

        return Ok(registrations);
    }

    /// <summary>
    /// POST /api/events/{id}/registrations
    /// Registers a person for an event and returns 201 Created.
    /// </summary>
    [HttpPost("{id}/registrations")]
    public async Task<ActionResult<RegistrationDto>> Register(
        int id,
        CreateRegistrationDto dto)
    {
        try
        {
            var registration = await _registrationService.RegisterAsync(id, dto);

            return CreatedAtAction(
                nameof(GetRegistrations),
                new { id },
                registration
            );
        }
        catch (EventNotFoundException)
        {
            // The event ID does not exist.
            return NotFound($"Event with ID {id} was not found.");
        }
        catch (PastEventException ex)
        {
            // Registration is not allowed for past events.
            return BadRequest(ex.Message);
        }
        catch (DuplicateRegistrationException ex)
        {
            // The same email is already registered for this event.
            return Conflict(ex.Message);
        }
        catch (EventFullException ex)
        {
            // The event has reached its capacity.
            return Conflict(ex.Message);
        }
    }
}