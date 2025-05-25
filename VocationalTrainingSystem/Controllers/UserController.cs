using Microsoft.AspNetCore.Mvc;

namespace WebApplication4.Controllers
{
    public class UserController : Controller
    {
        public IActionResult UserDashboard()
        {
            return View();
        }
    }
}
