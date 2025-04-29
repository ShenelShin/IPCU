using Microsoft.AspNetCore.Mvc;

namespace IPCU.Controllers
{
    public class EnvironmentalController : Controller
    {
        // GET: /Environmental/
        public IActionResult Index()
        {
            return View();
        }
    }
}
