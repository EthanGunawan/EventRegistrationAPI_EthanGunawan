using EventRegistrationAPI_EthanGunawan.DTOs;
using EventRegistrationAPI_EthanGunawan.Exceptions;
using EventRegistrationAPI_EthanGunawan.Services;
using Microsoft.AspNetCore.Mvc;

namespace EventRegistrationAPI_EthanGunawan.Controllers;

// EventsController handles event and event-registration endpoints.
// It calls the service layer and does not use AppDbContext directly.
[ApiController]
[Route("api/events")]
public class EventsController : ControllerBase
{
    private readonly IEventService _eventService;
    private readonly IRegistrationService _registrationService;

    public EventsController(
        IEventService eventService,
        IRegistrationService registrationService)
    {
        _eventService = eventService;
        _registrationService = registrationService;
    }

    // GET /api/events
    // Returns all events sorted by date.
    [HttpGet]
    public async Task<ActionResult<List<EventDto>>> GetEvents()
    {
        var events = await _eventService.GetEventsAsync();

        return Ok(events);
    }

    // GET /api/events/{id}
    // Returns one event, or 404 Not Found if it does not exist.
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

    // POST /api/events
    // Creates a new event and returns 201 Created.
    [HttpPost]
    public async Task<ActionResult<EventDto>> CreateEvent(CreateEventDto dto)
    {
        var createdEvent = await _eventService.CreateEventAsync(dto);

        return CreatedAtAction(
            nameof(GetEvent),
            new { id = createdEvent.EventId },
            createdEvent
        );
    }

    // GET /api/events/{id}/registrations
    // Returns all registrations for one event, or 404 if the event does not exist.
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

    // POST /api/events/{id}/registrations
    // Registers a person for an event and returns 201 Created.
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
            return NotFound($"Event with ID {id} was not found.");
        }
        catch (PastEventException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (DuplicateRegistrationException ex)
        {
            return Conflict(ex.Message);
        }
        catch (EventFullException ex)
        {
            return Conflict(ex.Message);
        }
    }
}