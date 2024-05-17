using Microsoft.AspNetCore.Mvc;

namespace RecruitmentTask.Api.Controllers.Availability;

[ApiController]
[Route("Availability")]
public class AvailabilityController : ControllerBase
{
    [HttpHead]
    public IActionResult GetAvailability()
    {
        return Ok();
    }
}
