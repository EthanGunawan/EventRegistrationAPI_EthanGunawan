using EventRegistrationAPI_EthanGunawan.Services;
using Microsoft.AspNetCore.Mvc;

namespace EventRegistrationAPI_EthanGunawan.Controllers;

// RegistrationsController handles registration cancellation.
[ApiController]
[Route("api/registrations")]
public class RegistrationsController : ControllerBase
{
    private readonly IRegistrationService _registrationService;

    public RegistrationsController(IRegistrationService registrationService)
    {
        _registrationService = registrationService;
    }

    // DELETE /api/registrations/{id}
    // Cancels a registration and frees one spot.
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteRegistration(int id)
    {
        bool deleted = await _registrationService.DeleteRegistrationAsync(id);

        if (!deleted)
        {
            return NotFound($"Registration with ID {id} was not found.");
        }

        // 204 No Content is returned when deletion succeeds.
        return NoContent();
    }
}