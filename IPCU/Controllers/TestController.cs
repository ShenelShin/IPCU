using IPCU.Data;
using IPCU.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace IPCU.Controllers
{
    public class TestController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TestController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult TakeTest()
        {
            return View();
        }

        public IActionResult Instructions()
        {
            return View();
        }
        public IActionResult Instructions2()
        {
            return View();
        }
        public IActionResult InstructionsPreC()
        {
            return View();
        }
        public IActionResult InstructionsPostClinical()
        {
            return View();
        }

        public IActionResult StartExam()
        {
            return View("~/Views/PreTestNonClinical/Quiz.cshtml");
        }
        public IActionResult PostExamNonClinicals()
        {
            return View("~/Views/PostTestNonClinicals/Quiz.cshtml");
        }

        public IActionResult StartExamPostC()
        {
            return View("~/Views/PostTestClinical/Index.cshtml");
        }
        public IActionResult StartExamPreC()
        {
            return View("~/Views/PreTestClinical/Quiz.cshtml");
        }
    }
}
