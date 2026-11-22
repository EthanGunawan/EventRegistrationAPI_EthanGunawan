using System.ComponentModel.DataAnnotations;

namespace EventRegistrationAPI_EthanGunawan.DTOs;

// Input DTO used to register a person for an event.
// RegisteredOn is excluded because the service sets it automatically.
public class CreateRegistrationDto
{
    [Required(ErrorMessage = "Student name is required.")]
    public string StudentName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Email is required.")]
    [EmailAddress(ErrorMessage = "Please enter a valid email address.")]
    public string Email { get; set; } = string.Empty;
}