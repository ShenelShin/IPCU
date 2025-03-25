using Microsoft.AspNetCore.Mvc;

namespace IPCU.Controllers
{
    public class DashboardHAIController : Controller
    {
        public IActionResult Index()
        {
            return View("~/Views/HAI/Dashboard.cshtml");
        }


    }
}
