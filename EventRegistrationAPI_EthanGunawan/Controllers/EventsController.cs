using EventRegistrationAPI_EthanGunawan.DTOs;
using EventRegistrationAPI_EthanGunawan.Services;
using Microsoft.AspNetCore.Mvc;

namespace EventRegistrationAPI_EthanGunawan.Controllers;

// EventsController handles event endpoints.
// It calls EventService and does not use AppDbContext directly.
[ApiController]
[Route("api/events")]
public class EventsController : ControllerBase
{
    private readonly IEventService _eventService;

    public EventsController(IEventService eventService)
    {
        _eventService = eventService;
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
}