

using Microsoft.AspNetCore.Mvc;

namespace Cut_Roll_AdminDashboard.Api.Common.Extensions.Controllers;

public static class InternalServerErrorMethod
{
    public static IActionResult InternalServerError(this ControllerBase controller, string message)
    {
        return controller.StatusCode(500, new { message });
    }
}
