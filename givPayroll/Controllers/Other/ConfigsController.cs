using givPayroll.Data;
using givPayroll.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

public class ConfigsController : Controller
{
    private readonly ApplicationDbContext _context;

    public ConfigsController(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        var configs = await _context.Configs
            .AsNoTracking()
            .Select(c => new Config
            {
                CompanyID = c.CompanyID,
                CompanyName = c.CompanyName
            })
            .ToListAsync();

        return View(configs);
    }
}