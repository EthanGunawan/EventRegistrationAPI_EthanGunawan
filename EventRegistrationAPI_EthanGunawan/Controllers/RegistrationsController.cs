using EventRegistrationAPI_EthanGunawan.Services;
using Microsoft.AspNetCore.Mvc;

namespace EventRegistrationAPI_EthanGunawan.Controllers;

/// <summary>
/// RegistrationsController handles registration cancellation.
/// It calls the service layer and does not use AppDbContext directly.
/// </summary>
[ApiController]
[Route("api/registrations")]
public class RegistrationsController : ControllerBase
{
    private readonly IRegistrationService _registrationService;

    public RegistrationsController(IRegistrationService registrationService)
    {
        _registrationService = registrationService;
    }

    /// <summary>
    /// DELETE /api/registrations/{id}
    /// Cancels a registration and frees one spot.
    /// Returns 204 No Content when successful, or 404 if not found.
    /// </summary>
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteRegistration(int id)
    {
        bool deleted = await _registrationService.DeleteRegistrationAsync(id);

        if (!deleted)
        {
            return NotFound($"Registration with ID {id} was not found.");
        }

        // 204 No Content is returned because there is no response body.
        return NoContent();
    }
}