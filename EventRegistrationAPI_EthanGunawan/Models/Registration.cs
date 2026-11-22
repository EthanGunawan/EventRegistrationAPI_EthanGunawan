using System.ComponentModel.DataAnnotations;

namespace EventRegistrationAPI_EthanGunawan.Models;

// Represents one person registered for one event.
public class Registration
{
    public int RegistrationId { get; set; }

    public int EventId { get; set; }

    [Required(ErrorMessage = "Student name is required.")]
    public string StudentName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Email is required.")]
    [EmailAddress(ErrorMessage = "Please enter a valid email address.")]
    public string Email { get; set; } = string.Empty;

    // Set automatically by the service when a registration is created.
    public DateTime RegisteredOn { get; set; }

    // Navigation property to the event this registration belongs to.
    public Event Event { get; set; } = null!;
}