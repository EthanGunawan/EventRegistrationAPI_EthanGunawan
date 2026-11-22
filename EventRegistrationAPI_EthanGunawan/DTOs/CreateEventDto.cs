using System.ComponentModel.DataAnnotations;

namespace EventRegistrationAPI_EthanGunawan.DTOs;

// Input DTO used to create a new event.
public class CreateEventDto
{
    [Required(ErrorMessage = "Title is required.")]
    [StringLength(100, ErrorMessage = "Title cannot be longer than 100 characters.")]
    public string Title { get; set; } = string.Empty;

    [Required(ErrorMessage = "Location is required.")]
    public string Location { get; set; } = string.Empty;

    // FutureDate will be added in Milestone 3.
    public DateTime EventDate { get; set; }

    [Range(1, 500, ErrorMessage = "Capacity must be between 1 and 500.")]
    public int Capacity { get; set; }
}