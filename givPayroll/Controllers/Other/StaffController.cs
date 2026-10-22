using givPayroll.Data;
using givPayroll.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

public class StaffController : Controller
{
    private readonly ApplicationDbContext _context;

    public StaffController(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index(
        string? searchString,
        string? sortOrder)
    {
        ViewData["CurrentFilter"] = searchString;
        ViewData["CurrentSort"] = sortOrder;

        ViewData["StaffIDSort"] =
            sortOrder == "id" ? "id_desc" : "id";

        ViewData["FirstNameSort"] =
            sortOrder == "firstName" ? "firstName_desc" : "firstName";

        ViewData["LastNameSort"] =
            sortOrder == "lastName" ? "lastName_desc" : "lastName";

        IQueryable<Staff> staff = _context.staff.AsNoTracking();

        // Search by first name or last name
        if (!string.IsNullOrWhiteSpace(searchString))
        {
            staff = staff.Where(s =>
                s.FirstName.Contains(searchString) ||
                s.LastName.Contains(searchString));
        }

        // Sorting
        staff = sortOrder switch
        {
            "id" => staff.OrderBy(s => s.StaffID),
            "id_desc" => staff.OrderByDescending(s => s.StaffID),

            "firstName" => staff.OrderBy(s => s.FirstName),
            "firstName_desc" => staff.OrderByDescending(s => s.FirstName),

            "lastName" => staff.OrderBy(s => s.LastName),
            "lastName_desc" => staff.OrderByDescending(s => s.LastName),

            _ => staff.OrderBy(s => s.StaffID)
        };

        var result = await staff.Where(i=>i.IsActive==true)
            .Select(s => new Staff
            {
                StaffID = s.StaffID,
                FirstName = s.FirstName,
                LastName = s.LastName
            })
            .ToListAsync();

        return View(result);
    }
}