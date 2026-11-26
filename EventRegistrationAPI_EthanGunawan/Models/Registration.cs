using System.ComponentModel.DataAnnotations;

namespace EventRegistrationAPI_EthanGunawan.Models;

/// <summary>
/// Represents one person registered for one campus event.
/// </summary>
public class Registration
{
    /// <summary>
    /// Primary key for the registration.
    /// </summary>
    public int RegistrationId { get; set; }

    /// <summary>
    /// Foreign key that links the registration to its event.
    /// </summary>
    public int EventId { get; set; }

    /// <summary>
    /// The name of the student or staff member registering.
    /// </summary>
    [Required(ErrorMessage = "Student name is required.")]
    public string StudentName { get; set; } = string.Empty;

    /// <summary>
    /// The email address used to identify the registrant.
    /// </summary>
    [Required(ErrorMessage = "Email is required.")]
    [EmailAddress(ErrorMessage = "Please enter a valid email address.")]
    public string Email { get; set; } = string.Empty;

    /// <summary>
    /// The date and time the registration was created.
    /// This is set automatically by RegistrationService.
    /// </summary>
    public DateTime RegisteredOn { get; set; }

    /// <summary>
    /// Navigation property to the event this registration belongs to.
    /// </summary>
    public Event Event { get; set; } = null!;
}