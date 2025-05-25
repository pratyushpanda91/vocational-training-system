using Microsoft.AspNetCore.Mvc;

namespace WebApplication4.Controllers
{
    public class ApproverController : Controller
    {
        public IActionResult ApproverDashboard()
        {
            return View();
        }
    }
}
