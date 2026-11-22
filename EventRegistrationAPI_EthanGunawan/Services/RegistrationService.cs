using EventRegistrationAPI_EthanGunawan.Data;
using EventRegistrationAPI_EthanGunawan.DTOs;
using EventRegistrationAPI_EthanGunawan.Exceptions;
using EventRegistrationAPI_EthanGunawan.Models;
using Microsoft.EntityFrameworkCore;

namespace EventRegistrationAPI_EthanGunawan.Services;

// RegistrationService performs all registration database work, DTO mapping, and business rules.
public class RegistrationService : IRegistrationService
{
    private readonly AppDbContext _context;

    public RegistrationService(AppDbContext context)
    {
        _context = context;
    }

    // Registers a person for an event after checking all business rules.
    public async Task<RegistrationDto> RegisterAsync(int eventId, CreateRegistrationDto dto)
    {
        // Rule 1: The event must exist.
        var eventEntity = await _context.Events
            .Include(e => e.Registrations)
            .FirstOrDefaultAsync(e => e.EventId == eventId);

        if (eventEntity == null)
        {
            throw new EventNotFoundException(eventId);
        }

        // Rule 2: Registration is not allowed for past events.
        if (eventEntity.EventDate <= DateTime.Now)
        {
            throw new PastEventException();
        }

        // Rule 3: The same email cannot register twice for the same event.
        // The comparison is case-insensitive.
        bool duplicateEmail = eventEntity.Registrations.Any(r =>
            r.Email.Equals(dto.Email, StringComparison.OrdinalIgnoreCase));

        if (duplicateEmail)
        {
            throw new DuplicateRegistrationException();
        }

        // Rule 4: The event cannot accept registrations once it is full.
        if (eventEntity.Registrations.Count >= eventEntity.Capacity)
        {
            throw new EventFullException();
        }

        var registration = new Registration
        {
            EventId = eventId,
            StudentName = dto.StudentName,
            Email = dto.Email,
            RegisteredOn = DateTime.Now
        };

        _context.Registrations.Add(registration);
        await _context.SaveChangesAsync();

        return new RegistrationDto
        {
            RegistrationId = registration.RegistrationId,
            EventTitle = eventEntity.Title,
            StudentName = registration.StudentName,
            Email = registration.Email,
            RegisteredOn = registration.RegisteredOn
        };
    }

    // Cancels a registration and frees one spot. Returns false if it does not exist.
    public async Task<bool> DeleteRegistrationAsync(int registrationId)
    {
        var registration = await _context.Registrations
            .FirstOrDefaultAsync(r => r.RegistrationId == registrationId);

        if (registration == null)
        {
            return false;
        }

        _context.Registrations.Remove(registration);
        await _context.SaveChangesAsync();

        return true;
    }
}