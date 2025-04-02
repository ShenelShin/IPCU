using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using IPCU.Models;

namespace IPCU.Controllers
{
    public class UserAreaManagementController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UserAreaManagementController(
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        // GET: Display list of users with their assigned areas
        public async Task<IActionResult> Index()
        {
            var users = await _userManager.Users
                .Select(u => new UserAreaViewModel
                {
                    UserId = u.Id,
                    UserName = u.UserName,
                    Email = u.Email,
                    FullName = u.FullName ?? $"{u.FirstName} {u.LastName}",
                    AssignedArea = u.AssignedArea
                })
                .ToListAsync();

            return View(users);
        }

        // GET: Display form to assign area to a specific user
        public async Task<IActionResult> AssignArea(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return NotFound();
            }

            var model = new AssignAreaViewModel
            {
                UserId = user.Id,
                UserName = user.UserName,
                FullName = user.FullName ?? $"{user.FirstName} {user.LastName}",
                AssignedArea = user.AssignedArea,
                // Initialize SelectedAreas from AssignedArea if it exists
                SelectedAreas = !string.IsNullOrEmpty(user.AssignedArea)
                    ? user.AssignedArea.Split(',').Select(a => a.Trim()).ToList()
                    : new List<string>()
            };

            return View(model);
        }

        // POST: Update user's assigned area
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AssignArea(AssignAreaViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByIdAsync(model.UserId);
                if (user == null)
                {
                    return NotFound();
                }

                // Use the AssignedArea from the model directly
                // This will be populated by the JavaScript in the view
                user.AssignedArea = model.AssignedArea;
                var result = await _userManager.UpdateAsync(user);

                if (result.Succeeded)
                {
                    TempData["SuccessMessage"] = "Area assigned successfully.";
                    return RedirectToAction(nameof(Index));
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }

            return View(model);
        }

        // GET: Batch assign areas to multiple users
        public async Task<IActionResult> BatchAssignAreas()
        {
            var users = await _userManager.Users
                .Select(u => new UserAreaViewModel
                {
                    UserId = u.Id,
                    UserName = u.UserName,
                    Email = u.Email,
                    FullName = u.FullName ?? $"{u.FirstName} {u.LastName}",
                    AssignedArea = u.AssignedArea
                })
                .ToListAsync();

            return View(users);
        }

        // POST: Batch update user areas
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> BatchAssignAreas(List<UserAreaViewModel> model)
        {
            if (ModelState.IsValid)
            {
                foreach (var userModel in model)
                {
                    var user = await _userManager.FindByIdAsync(userModel.UserId);
                    if (user != null)
                    {
                        user.AssignedArea = userModel.AssignedArea;
                        await _userManager.UpdateAsync(user);
                    }
                }

                TempData["SuccessMessage"] = "Areas assigned successfully.";
                return RedirectToAction(nameof(Index));
            }

            return View(model);
        }
    }
}