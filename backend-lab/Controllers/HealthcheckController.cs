using Microsoft.AspNetCore.Mvc;

namespace backend_lab.Controllers;

[Route("healthcheck")]
public class HealthcheckController : ControllerBase
{
    [HttpGet]
    public IActionResult GetHealthcheck ()
    {
        return Ok("Healthcheck is working, yey!");
    }
}

