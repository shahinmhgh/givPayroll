
using givPayroll.Data;
using givPayroll.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace givPayroll.Controllers
{
    [Authorize]
    public class IdentityUsersController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _context;

        public IdentityUsersController(
            UserManager<ApplicationUser> userManager, ApplicationDbContext context  )
        {
            _userManager = userManager;
            _context = context;
        }

        public async Task<IActionResult> Index(
            string searchString = "",
            string sortColumn = "UserName",
            string sortDirection = "asc",
            int pageNumber = 1,
            int pageSize = 10)
        {
            var query = _userManager.Users.AsNoTracking();

            if (!string.IsNullOrWhiteSpace(searchString))
            {
                searchString = searchString.Trim();

                query = query.Where(u =>
                    (u.UserName != null &&
                     u.UserName.Contains(searchString)) ||
                    (u.Email != null &&
                     u.Email.Contains(searchString)) ||
                    (u.PhoneNumber != null &&
                     u.PhoneNumber.Contains(searchString)));
            }

            sortColumn = new[]
                { "UserName", "Email", "PhoneNumber", "Id", "Lockout" }
                .Contains(sortColumn) ? sortColumn : "UserName";

            bool desc = sortDirection.Equals(
                "desc", StringComparison.OrdinalIgnoreCase);

            sortDirection = desc ? "desc" : "asc";

            query = sortColumn switch
            {
                "Email" => desc
                    ? query.OrderByDescending(u => u.Email)
                    : query.OrderBy(u => u.Email),

                "PhoneNumber" => desc
                    ? query.OrderByDescending(u => u.PhoneNumber)
                    : query.OrderBy(u => u.PhoneNumber),

                "Id" => desc
                    ? query.OrderByDescending(u => u.Id)
                    : query.OrderBy(u => u.Id),

                "Lockout" => desc
                    ? query.OrderByDescending(u => u.LockoutEnd)
                    : query.OrderBy(u => u.LockoutEnd),

                _ => desc
                    ? query.OrderByDescending(u => u.UserName)
                    : query.OrderBy(u => u.UserName)
            };

            int totalRecords = await query.CountAsync();

            pageSize = new[] { 10, 20, 50, 100 }
                .Contains(pageSize) ? pageSize : 10;

            int totalPages = (int)Math.Ceiling(
                totalRecords / (double)pageSize);

            pageNumber = Math.Max(1, pageNumber);
            pageNumber = totalPages == 0
                ? 1 : Math.Min(pageNumber, totalPages);

            var model = new IdentityUserListViewModel
            {
                Users = await query
                    .Skip((pageNumber - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync(),

                SearchString = searchString,
                SortColumn = sortColumn,
                SortDirection = sortDirection,
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalPages = totalPages,
                TotalRecords = totalRecords
            };

            return View(model);
        }

        // AJAX: load Create modal
        [HttpGet]
        public async Task<IActionResult> CreateModal()
        {
            ViewBag.IsEdit = false;
            ViewBag.UserID = await GetUser();
            return PartialView(
                "_UserModal", new ApplicationUser());
        }

        // AJAX: load Edit modal with existing user
        [HttpGet]
        public async Task<IActionResult> EditModal(string id)
        {
            var user = await _userManager.FindByIdAsync(id);

            if (user == null)
                return NotFound();

            ViewBag.IsEdit = true;
            ViewBag.UserID = await GetUser();

            return PartialView("_UserModal", user);
        }
        private async Task<List<SelectListItem>> GetUser()
        {
            var persons= await _context.User
                .OrderBy(x => x.UserID)
                .Select(x => new SelectListItem
                {
                    Value = x.UserID.ToString(),
                    Text = x.UserFullName 

                })
                .ToListAsync();

            foreach (var item in persons)
                item.Text = AppUtil.FixYEKE2(item.Text ?? "");

            return persons;
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
            string userName,
            string? email,
            string? phoneNumber,
            string password , string fullName, string UserID)
        {
            if (string.IsNullOrWhiteSpace(userName) ||
                string.IsNullOrWhiteSpace(password))
            {
                TempData["Error"] =
                    "Username and password are required.";

                return RedirectToAction(nameof(Index));
            }

            var user = new ApplicationUser
            {
                UserName = userName.Trim(),
                Email = string.IsNullOrWhiteSpace(email)
                    ? null : email.Trim(),
                PhoneNumber = string.IsNullOrWhiteSpace(phoneNumber)
                    ? null : phoneNumber.Trim(),
                UserID = Convert.ToInt32(UserID),
                FullName = string.IsNullOrWhiteSpace(fullName)
                    ? null : fullName.Trim()
            };

            var result = await _userManager.CreateAsync(user, password);

            TempData[result.Succeeded ? "Success" : "Error"] =
                result.Succeeded
                    ? "User created successfully."
                    : string.Join(" ",
                        result.Errors.Select(e => e.Description));

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(
            string id,
            string userName,
            string? email,
            string? phoneNumber, string fullName, string UserID)
        {
            var user = await _userManager.FindByIdAsync(id);

            if (user == null)
                return NotFound();

            if (string.IsNullOrWhiteSpace(userName))
            {
                TempData["Error"] = "Username is required.";
                return RedirectToAction(nameof(Index));
            }
            user.UserName = userName.Trim();
            user.Email = string.IsNullOrWhiteSpace(email)
                ? null : email.Trim();
            user.PhoneNumber = string.IsNullOrWhiteSpace(phoneNumber)
                ? null : phoneNumber.Trim();
            user.UserID =Convert.ToInt32( UserID);
            user.FullName = string.IsNullOrWhiteSpace(fullName)
                ? null : fullName.Trim();

            var result = await _userManager.UpdateAsync(user);

            TempData[result.Succeeded ? "Success" : "Error"] =
                result.Succeeded
                    ? "User updated successfully."
                    : string.Join(" ",
                        result.Errors.Select(e => e.Description));

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ToggleLockout(string id)
        {
            var user = await _userManager.FindByIdAsync(id);

            if (user == null)
                return NotFound();

            if (!await _userManager.GetLockoutEnabledAsync(user))
            {
                var enableResult =
                    await _userManager.SetLockoutEnabledAsync(user, true);

                if (!enableResult.Succeeded)
                {
                    TempData["Error"] = "Could not enable lockout.";
                    return RedirectToAction(nameof(Index));
                }
            }

            bool locked = user.LockoutEnd.HasValue &&
                user.LockoutEnd.Value > DateTimeOffset.UtcNow;

            var result = await _userManager.SetLockoutEndDateAsync(
                user,
                locked ? null : DateTimeOffset.MaxValue);

            TempData[result.Succeeded ? "Success" : "Error"] =
                result.Succeeded
                    ? (locked ? "User unlocked." : "User locked.")
                    : "Could not change lockout status.";

            return RedirectToAction(nameof(Index));
        }
    }
}
