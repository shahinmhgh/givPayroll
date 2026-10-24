using givPayroll.Data;
using givPayroll.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
 
public class PersonController : Controller
{
    private readonly ApplicationDbContext _context;

    public PersonController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> Dropdown()
    {
        var model = new PersonDropdownViewModel
        {
            Items = await _context.PersonCategories
                .Join(
                    _context.PersonCategoryLengths,
                    pc => pc.CategoryCode.Length,
                    pcl => pcl.SumLevelLength,
                    (pc, pcl) => new { pc, pcl }
                )
                .Where(x => x.pcl.CategoryLevel == 3)
                .Join(
                    _context.PersonCategoryGroups,
                    x => x.pc.CategoryCode,
                    pcg => pcg.CategoryCode,
                    (x, pcg) => new { x.pc, pcg }
                )
                .Join(
                    _context.PersonGroups,
                    x => x.pcg.PersonGroupID,
                    pg => pg.PersonGroupID,
                    (x, pg) => new MoinPersonGroupDropdownItem
                    {
                        CategoryName = x.pc.CategoryName,
                        PersonGroupName = pg.PersonGroupName,
                        CategoryCode = x.pc.CategoryCode
                    }
                )
                .OrderBy(x => x.CategoryName)
                .ThenBy(x => x.PersonGroupName)
                .ToListAsync()
        };

        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Dropdown(
    PersonDropdownViewModel model)
    {
        if (string.IsNullOrWhiteSpace(model.SelectedCategoryCode))
        {
            ModelState.AddModelError(
                nameof(model.SelectedCategoryCode),
                "Please select a category.");
        }
        else
        {
            var exists = await _context.PersonCategories
                .AsNoTracking()
                .AnyAsync(pc =>
                    pc.CategoryCode == model.SelectedCategoryCode &&
                    _context.PersonCategoryLengths.Any(pcl =>
                        pcl.CategoryLevel == 3 &&
                        pcl.SumLevelLength == pc.CategoryCode.Length) &&
                    _context.PersonCategoryGroups.Any(pcg =>
                        pcg.CategoryCode == pc.CategoryCode));

            if (!exists)
            {
                ModelState.AddModelError(
                    nameof(model.SelectedCategoryCode),
                    "The selected category was not found.");
            }
            else
            {
                ViewBag.Message =
                    $"Selected Category Code: {model.SelectedCategoryCode}";
            }
        }

        // Reload dropdown data
        model.Items = await _context.PersonCategories
            .Join(
                _context.PersonCategoryLengths,
                pc => pc.CategoryCode.Length,
                pcl => pcl.SumLevelLength,
                (pc, pcl) => new { pc, pcl }
            )
            .Where(x => x.pcl.CategoryLevel == 3)
            .Join(
                _context.PersonCategoryGroups,
                x => x.pc.CategoryCode,
                pcg => pcg.CategoryCode,
                (x, pcg) => new { x.pc, pcg }
            )
            .Join(
                _context.PersonGroups,
                x => x.pcg.PersonGroupID,
                pg => pg.PersonGroupID,
                (x, pg) => new MoinPersonGroupDropdownItem
                {
                    CategoryName = x.pc.CategoryName,
                    PersonGroupName = pg.PersonGroupName,
                    CategoryCode = x.pc.CategoryCode
                }
            )
            .OrderBy(x => x.CategoryName)
            .ThenBy(x => x.PersonGroupName)
            .ToListAsync();

        return View(model);
    }
}