using Microsoft.AspNetCore.Mvc;

namespace VocationalTrainingSystem.Controllers
{
    public class ApproverController : Controller
    {
        public IActionResult ApproverDashboard()
        {
            return View();
        }
    }
}
