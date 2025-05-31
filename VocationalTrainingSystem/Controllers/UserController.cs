using Microsoft.AspNetCore.Mvc;

namespace VocationalTrainingSystem.Controllers
{
    public class UserController : Controller
    {
        public IActionResult UserDashboard()
        {
            return View();
        }
    }
}
