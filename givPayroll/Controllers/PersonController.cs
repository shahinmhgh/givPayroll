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
            Persons = await _context.Persons
                .AsNoTracking()
                .OrderBy(p => p.PersonID)
                .Select(p => new PersonDropdownItem
                {
                    PersonID = p.PersonID,
                    FirstName = p.PersonFirstName,
                    LastName = p.PersonLastName
                })
                .ToListAsync()
        };

        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Dropdown(
        PersonDropdownViewModel model)
    {
        if (!model.SelectedPersonID.HasValue)
        {
            ModelState.AddModelError(
                nameof(model.SelectedPersonID),
                "Please select a person.");
        }
        else
        {
            var person = await _context.Persons
                .AsNoTracking()
                .FirstOrDefaultAsync(p =>
                    p.PersonID == model.SelectedPersonID.Value);

            if (person == null)
            {
                ModelState.AddModelError(
                    nameof(model.SelectedPersonID),
                    "The selected person was not found.");
            }
            else
            {
                ViewBag.Message =
                    $"Selected person: {person.PersonFirstName} " +
                    $"{person.PersonLastName} (ID: {person.PersonID})";
            }
        }

        model.Persons = await _context.Persons
            .AsNoTracking()
            .OrderBy(p => p.PersonID)
            .Select(p => new PersonDropdownItem
            {
                PersonID = p.PersonID,
                FirstName = p.PersonFirstName,
                LastName = p.PersonLastName
            })
            .ToListAsync();

        return View(model);
    }
}