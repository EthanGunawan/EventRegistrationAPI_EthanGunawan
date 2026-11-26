namespace EventRegistrationAPI_EthanGunawan.DTOs;

/// <summary>
/// Output DTO returned by event endpoints.
/// It prevents Entity Framework entities from being returned to API clients.
/// </summary>
public class EventDto
{
    public int EventId { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Location { get; set; } = string.Empty;
    public DateTime EventDate { get; set; }
    public int Capacity { get; set; }

    /// <summary>
    /// Calculated from the number of registrations for the event.
    /// This value is not stored in the database.
    /// </summary>
    public int RegisteredCount { get; set; }

    /// <summary>
    /// Calculated as Capacity minus RegisteredCount.
    /// This value is not stored in the database.
    /// </summary>
    public int SpotsLeft { get; set; }
}