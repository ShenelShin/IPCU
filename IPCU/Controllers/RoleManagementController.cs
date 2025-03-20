using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using IPCU.Models;

namespace IPCU.Controllers
{
    [Authorize(Roles = "Admin")] // Restrict access to admins only
    public class RoleManagementController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public RoleManagementController(
            RoleManager<IdentityRole> roleManager,
            UserManager<ApplicationUser> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }

        // GET: Display list of all roles
        public IActionResult Index()
        {
            var roles = _roleManager.Roles.ToList();
            return View(roles);
        }

        // GET: Display form to create a new role
        public IActionResult CreateRole()
        {
            return View();
        }

        // POST: Create a new role
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateRole(string roleName)
        {
            if (string.IsNullOrEmpty(roleName))
            {
                ModelState.AddModelError("", "Role name cannot be empty");
                return View();
            }

            var roleExists = await _roleManager.RoleExistsAsync(roleName);
            if (!roleExists)
            {
                var result = await _roleManager.CreateAsync(new IdentityRole(roleName));
                if (result.Succeeded)
                {
                    return RedirectToAction(nameof(Index));
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            else
            {
                ModelState.AddModelError("", "Role already exists");
            }

            return View();
        }

        // GET: Display list of users with their roles
        public async Task<IActionResult> UserRoles()
        {
            var users = _userManager.Users.ToList();
            var userRolesViewModel = new List<UserRolesViewModel>();

            foreach (var user in users)
            {
                var userRoles = await _userManager.GetRolesAsync(user);
                userRolesViewModel.Add(new UserRolesViewModel
                {
                    UserId = user.Id,
                    UserName = user.UserName,
                    Email = user.Email,
                    Roles = userRoles.ToList()
                });
            }

            return View(userRolesViewModel);
        }

        // GET: Manage users in a role
        public async Task<IActionResult> ManageUsersInRole(string roleId)
        {
            var role = await _roleManager.FindByIdAsync(roleId);
            if (role == null)
            {
                return NotFound();
            }

            var model = new List<UserRolesViewModel>();
            foreach (var user in _userManager.Users)
            {
                var userRoleViewModel = new UserRolesViewModel
                {
                    UserId = user.Id,
                    UserName = user.UserName,
                    Email = user.Email,
                    IsSelected = await _userManager.IsInRoleAsync(user, role.Name)
                };

                model.Add(userRoleViewModel);
            }

            ViewBag.RoleId = roleId;
            ViewBag.RoleName = role.Name;
            return View(model);
        }

        // POST: Update users in a role
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ManageUsersInRole(List<UserRolesViewModel> model, string roleId)
        {
            var role = await _roleManager.FindByIdAsync(roleId);
            if (role == null)
            {
                return NotFound();
            }

            for (int i = 0; i < model.Count; i++)
            {
                var user = await _userManager.FindByIdAsync(model[i].UserId);

                if (user != null)
                {
                    if (model[i].IsSelected && !(await _userManager.IsInRoleAsync(user, role.Name)))
                    {
                        await _userManager.AddToRoleAsync(user, role.Name);
                    }
                    else if (!model[i].IsSelected && await _userManager.IsInRoleAsync(user, role.Name))
                    {
                        await _userManager.RemoveFromRoleAsync(user, role.Name);
                    }
                }
            }

            return RedirectToAction(nameof(Index));
        }
    }
}