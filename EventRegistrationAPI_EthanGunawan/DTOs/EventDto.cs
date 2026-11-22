namespace EventRegistrationAPI_EthanGunawan.DTOs;

// Output DTO returned by event endpoints.
// It prevents EF entities from being returned to API clients.
public class EventDto
{
    public int EventId { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Location { get; set; } = string.Empty;
    public DateTime EventDate { get; set; }
    public int Capacity { get; set; }

    // Calculated from the number of registrations; not stored in the database.
    public int RegisteredCount { get; set; }

    // Calculated as Capacity minus RegisteredCount; not stored in the database.
    public int SpotsLeft { get; set; }
}