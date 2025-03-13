using Microsoft.AspNetCore.Mvc;

namespace IPCU.Controllers
{
    public class TestController : Controller
    {
        public IActionResult TakeTest()
        {
            return View();
        }
        public IActionResult Instructions()
        {
            return View(); // This will look for Views/Test/Instructions.cshtml
        }

    }
}
