using System.ComponentModel.DataAnnotations;
using EventRegistrationAPI_EthanGunawan.Validation;

namespace EventRegistrationAPI_EthanGunawan.DTOs;

/// <summary>
/// Input DTO used to create a new campus event.
/// </summary>
public class CreateEventDto
{
    [Required(ErrorMessage = "Title is required.")]
    [StringLength(100, ErrorMessage = "Title cannot be longer than 100 characters.")]
    public string Title { get; set; } = string.Empty;

    [Required(ErrorMessage = "Location is required.")]
    public string Location { get; set; } = string.Empty;

    /// <summary>
    /// The event must occur in the future.
    /// </summary>
    [FutureDate(ErrorMessage = "Event date must be in the future.")]
    public DateTime EventDate { get; set; }

    [Range(1, 500, ErrorMessage = "Capacity must be between 1 and 500.")]
    public int Capacity { get; set; }
}