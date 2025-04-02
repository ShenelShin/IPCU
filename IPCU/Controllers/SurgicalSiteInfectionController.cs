using IPCU.Data;
using IPCU.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace IPCU.Controllers
{
    public class SurgicalSiteInfectionController : Controller
    {

        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public SurgicalSiteInfectionController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;

        }
        public IActionResult Index()
        {
            var model = new SurgicalSiteInfectionChecklist();
            return View(model);
        }
    }
}
