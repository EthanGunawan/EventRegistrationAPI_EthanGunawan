using System.ComponentModel.DataAnnotations;

namespace EventRegistrationAPI_EthanGunawan.Models;

// Represents a campus event that students and staff can register for.
public class Event
{
    public int EventId { get; set; }

    [Required(ErrorMessage = "Title is required.")]
    [StringLength(100, ErrorMessage = "Title cannot be longer than 100 characters.")]
    public string Title { get; set; } = string.Empty;

    [Required(ErrorMessage = "Location is required.")]
    public string Location { get; set; } = string.Empty;

    public DateTime EventDate { get; set; }

    public int Capacity { get; set; }

    // One event has many registrations.
    public List<Registration> Registrations { get; set; } = new();
}