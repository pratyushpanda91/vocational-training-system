using Microsoft.AspNetCore.Mvc;

namespace WebApplication4.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult AdminDashboard()
        {
            return View();
        }
    }
}
