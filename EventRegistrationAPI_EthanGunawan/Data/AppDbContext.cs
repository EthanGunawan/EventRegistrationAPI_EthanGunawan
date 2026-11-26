using EventRegistrationAPI_EthanGunawan.Models;
using Microsoft.EntityFrameworkCore;

namespace EventRegistrationAPI_EthanGunawan.Data;

/// <summary>
/// AppDbContext is the Entity Framework Core database context for the
/// Campus Event Registration API. It provides access to the Events and
/// Registrations tables and contains the seed data.
/// </summary>
public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    /// <summary>
    /// Represents the Events table in the database.
    /// </summary>
    public DbSet<Event> Events { get; set; }

    /// <summary>
    /// Represents the Registrations table in the database.
    /// </summary>
    public DbSet<Registration> Registrations { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Seed data uses fixed dates so future events remain in the future
        // while the assignment is being marked.
        //
        // Event 1 is in the past.
        // Event 2 is a future event with available spots.
        // Event 3 has a capacity of 2 and already has 2 registrations,
        // so it is full.
        // Event 4 is another future event with available spots.

        modelBuilder.Entity<Event>().HasData(
            new Event
            {
                EventId = 1,
                Title = "Welcome Back Orientation",
                Location = "Student Centre",
                EventDate = new DateTime(2026, 9, 1, 10, 0, 0),
                Capacity = 50
            },
            new Event
            {
                EventId = 2,
                Title = "Career Networking Night",
                Location = "Conference Room A",
                EventDate = new DateTime(2027, 3, 15, 18, 0, 0),
                Capacity = 30
            },
            new Event
            {
                EventId = 3,
                Title = "Coding Workshop",
                Location = "Computer Lab 204",
                EventDate = new DateTime(2027, 4, 10, 13, 0, 0),
                Capacity = 2
            },
            new Event
            {
                EventId = 4,
                Title = "Campus Volunteer Fair",
                Location = "Athletic Centre",
                EventDate = new DateTime(2027, 5, 20, 11, 0, 0),
                Capacity = 25
            }
        );

        modelBuilder.Entity<Registration>().HasData(
            new Registration
            {
                RegistrationId = 1,
                EventId = 3,
                StudentName = "Alex Chen",
                Email = "alex.chen@student.edu",
                RegisteredOn = new DateTime(2026, 11, 1, 9, 0, 0)
            },
            new Registration
            {
                RegistrationId = 2,
                EventId = 3,
                StudentName = "Priya Patel",
                Email = "priya.patel@student.edu",
                RegisteredOn = new DateTime(2026, 11, 2, 10, 0, 0)
            }
        );

        base.OnModelCreating(modelBuilder);
    }
}