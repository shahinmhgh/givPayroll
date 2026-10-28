using givPayroll;
using givPayroll.Data;
using givPayroll.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Configuration;

public class PersonController : Controller
{
    private readonly ApplicationDbContext _context;

    public PersonController(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> GetTafsilPersons(string categoryCode)
    {
        if (string.IsNullOrWhiteSpace(categoryCode))
            return Json(new List<TafsilPersonGroupDropdownItem>());

        var persons = await _context.Persons
            .AsNoTracking()
            .Join(
                _context.PersonCategoryGroups,
                p => p.PersonGroupID,
                pcg => pcg.PersonGroupID,
                (p, pcg) => new { p, pcg }
            )
            .Join(
                _context.PersonGroups,
                x => x.p.PersonGroupID,
                pg => pg.PersonGroupID,
                (x, pg) => new
                {
                    x.p.PersonID,
                    pg.PersonGroupName,
                    x.p.PersonFullName,
                    x.pcg.CategoryCode
                }
            )
            .Where(x => x.CategoryCode == categoryCode)
            .OrderBy(x => x.PersonFullName)
            .Select(x => new TafsilPersonGroupDropdownItem
            {
                PersonID = x.PersonID,
                PersonGroupName = x.PersonGroupName,
                PersonFullName = x.PersonFullName
            })
            .ToListAsync();

        foreach (var item in persons)
        {
            item.PersonFullName =
                AppUtil.FixYEKE2(item.PersonFullName ?? "");

            item.PersonGroupName =
                AppUtil.FixYEKE2(item.PersonGroupName ?? "");
        }

        return Json(persons);
    }

    [HttpGet]
    public async Task<IActionResult> Dropdown()
    {
        var model = new PersonDropdownViewModel
        {
            ItemsMoin = await _context.PersonCategories
                        .Join(
                            _context.PersonCategoryLengths,
                            pc => pc.CategoryCode.Length,
                            pcl => pcl.SumLevelLength,
                            (pc, pcl) => new { pc, pcl }
                        )
                        .Where(x => x.pcl.CategoryLevel == 3)
                        .OrderBy(x => x.pc.CategoryCode)
                        .Select(x => new MoinPersonGroupDropdownItem
                        {
                            CategoryCode = x.pc.CategoryCode,
                            CategoryFullName = ApplicationDbContext.PersonCategoryFullName(x.pc.CategoryCode)
                        }).ToListAsync(),
            //ItemsTafsil = await _context.Persons
            //        .AsNoTracking()
            //        .Join(
            //            _context.PersonCategoryGroups,
            //            p => p.PersonGroupID,
            //            pcg => pcg.PersonGroupID,
            //            (p, pcg) => new { p, pcg }
            //        )
            //        .Join(
            //            _context.PersonGroups,
            //            x => x.p.PersonGroupID,
            //            pg => pg.PersonGroupID,
            //            (x, pg) => new
            //            {
            //                x.p.PersonID,
            //                pg.PersonGroupName,
            //                x.p.PersonFullName,
            //                x.pcg.CategoryCode
            //            }
            //        )
            //        .Where(x => x.CategoryCode == "7508")
            //        .Select(x => new TafsilPersonGroupDropdownItem
            //        {
            //            PersonID = x.PersonID,
            //            PersonGroupName = x.PersonGroupName,
            //            PersonFullName = x.PersonFullName
            //        })
            //        .ToListAsync()
        };

        foreach (MoinPersonGroupDropdownItem item in model.ItemsMoin)
            item.CategoryFullName = AppUtil.FixYEKE(item.CategoryFullName);
        foreach (TafsilPersonGroupDropdownItem item in model.ItemsTafsil)
            item.PersonFullName = AppUtil.FixYEKE(item.PersonFullName);

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
                    $"Selected Category Code: {model.SelectedCategoryCode}" + "\n" +
                    $"Selected Person Code: {model.SelectedPersonID}";
            }
        }

        // Reload dropdown data
        model.ItemsMoin = await _context.PersonCategories
                        .Join(
                            _context.PersonCategoryLengths,
                            pc => pc.CategoryCode.Length,
                            pcl => pcl.SumLevelLength,
                            (pc, pcl) => new { pc, pcl }
                        )
                        .Where(x => x.pcl.CategoryLevel == 3)
                        .OrderBy(x => x.pc.CategoryCode)
                        .Select(x => new MoinPersonGroupDropdownItem
                        {
                            CategoryCode = x.pc.CategoryCode,
                            CategoryFullName = ApplicationDbContext.PersonCategoryFullName(x.pc.CategoryCode)
                        }).ToListAsync();

        if (model.SelectedPersonID.HasValue &&
                !string.IsNullOrWhiteSpace(model.SelectedCategoryCode))
        {
            var persons = await _context.Persons
            .AsNoTracking()
            .Join(
                _context.PersonCategoryGroups,
                p => p.PersonGroupID,
                pcg => pcg.PersonGroupID,
                (p, pcg) => new { p, pcg }
            )
            .Join(
                _context.PersonGroups,
                x => x.p.PersonGroupID,
                pg => pg.PersonGroupID,
                (x, pg) => new
                {
                    x.p.PersonID,
                    pg.PersonGroupName,
                    x.p.PersonFullName,
                    x.pcg.CategoryCode
                }
            )
            .Where(x => x.CategoryCode ==model.SelectedCategoryCode)
            .OrderBy(x => x.PersonFullName)
            .Select(x => new TafsilPersonGroupDropdownItem
            {
                PersonID = x.PersonID,
                PersonGroupName = x.PersonGroupName,
                PersonFullName = x.PersonFullName
            })
            .ToListAsync();

            foreach (var item in persons)
            {
                item.PersonFullName =
                    AppUtil.FixYEKE2(item.PersonFullName ?? "");

                item.PersonGroupName =
                    AppUtil.FixYEKE2(item.PersonGroupName ?? "");
            }

            if (persons==null)
            {
                ModelState.AddModelError(
                    nameof(model.SelectedPersonID),
                    "The selected person does not belong to this category.");
            }
        }

        return View(model);
    }
}