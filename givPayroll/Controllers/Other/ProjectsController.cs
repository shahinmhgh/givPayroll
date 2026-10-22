using givPayroll;
using givPayroll.Data;
using givPayroll.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

public class ProjectsController : Controller
{
    private readonly ApplicationDbContext _context;

    public ProjectsController(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index(
        string? searchString,
        string sortOrder)
    {
        if (searchString != null) 
            searchString = AppUtil.FixYEKE(searchString);
        //ViewData["CurrentFilter"] = searchString;
        //ViewData["CurrentSort"] = sortOrder;

        //ViewData["ProjectIdSort"] =       string.IsNullOrEmpty(sortOrder) ? "id_desc" : "";

        //ViewData["ProjectNameSort"] =   sortOrder == "name" ? "name_desc" : "name";

        //ViewData["StartDateSort"] =     sortOrder == "date" ? "date_desc" : "date";
        ViewData["CurrentSort"] = sortOrder;
        ViewData["CurrentFilter"] = searchString;

        ViewData["ProjectIdSort"] =
            sortOrder == "id" ? "id_desc" : "id";

        ViewData["ProjectNameSort"] =
            sortOrder == "name" ? "name_desc" : "name";

        ViewData["StartDateSort"] =
            sortOrder == "date" ? "date_desc" : "date";

        IQueryable<Project> projects = _context.Projects.AsNoTracking();

        if (!string.IsNullOrWhiteSpace(searchString))
        {
            projects = projects.Where(p =>
                p.ProjectName.Contains(searchString));
        }

        //projects = sortOrder switch
        //{
        //    "id_desc" => projects.OrderByDescending(p => p.ProjectID),
        //    "name" => projects.OrderBy(p => p.ProjectName),
        //    "name_desc" => projects.OrderByDescending(p => p.ProjectName),
        //    "date" => projects.OrderBy(p => p.StartDate),
        //    "date_desc" => projects.OrderByDescending(p => p.StartDate),
        //    _ => projects.OrderBy(p => p.ProjectID)
        //};
        projects = sortOrder switch
        {
            "id" => projects.OrderBy(p => p.ProjectID),
            "id_desc" => projects.OrderByDescending(p => p.ProjectID),

            "name" => projects.OrderBy(p => p.ProjectName),
            "name_desc" => projects.OrderByDescending(p => p.ProjectName),

            "date" => projects.OrderBy(p => p.StartDate),
            "date_desc" => projects.OrderByDescending(p => p.StartDate),

            _ => projects.OrderBy(p => p.ProjectID)
        };
        return View(await projects
            .Select(p => new Project
            {
                ProjectID = p.ProjectID,
                ProjectName = p.ProjectName,
                StartDate = p.StartDate
            })
            .ToListAsync());
    }
}