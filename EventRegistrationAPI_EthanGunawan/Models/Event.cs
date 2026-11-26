using System.ComponentModel.DataAnnotations;

namespace EventRegistrationAPI_EthanGunawan.Models;

/// <summary>
/// Represents a campus event that students and staff can register for.
/// One event can have many registrations.
/// </summary>
public class Event
{
    /// <summary>
    /// Primary key for the event.
    /// </summary>
    public int EventId { get; set; }

    /// <summary>
    /// The name or title of the campus event.
    /// </summary>
    [Required(ErrorMessage = "Title is required.")]
    [StringLength(100, ErrorMessage = "Title cannot be longer than 100 characters.")]
    public string Title { get; set; } = string.Empty;

    /// <summary>
    /// The building, room, or location where the event takes place.
    /// </summary>
    [Required(ErrorMessage = "Location is required.")]
    public string Location { get; set; } = string.Empty;

    /// <summary>
    /// The date and time when the event occurs.
    /// </summary>
    public DateTime EventDate { get; set; }

    /// <summary>
    /// The maximum number of people who can register for the event.
    /// </summary>
    public int Capacity { get; set; }

    /// <summary>
    /// Navigation property containing all registrations for this event.
    /// </summary>
    public List<Registration> Registrations { get; set; } = new();
}