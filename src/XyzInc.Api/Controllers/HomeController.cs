using Microsoft.AspNetCore.Mvc;

namespace XyzInc.Api.Controllers;

[Route("")]
public class HomeController : BaseController
{
    [HttpGet]
    public ActionResult Get()
        => Ok("XYZ Inc. Billing API.");
}