namespace EventRegistrationAPI_EthanGunawan.DTOs;

/// <summary>
/// Output DTO returned when registration information is needed.
/// </summary>
public class RegistrationDto
{
    public int RegistrationId { get; set; }
    public string EventTitle { get; set; } = string.Empty;
    public string StudentName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public DateTime RegisteredOn { get; set; }
}