using givPayroll.Data;
using givPayroll.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuGet.Common;

namespace givPayroll.Controllers
{
    [Authorize]
    public class HolidayController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HolidayController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Holiday
        public async Task<IActionResult> Index(int? year)
        {
            int selectedYear = year ?? 1405;

            var holidays = await _context.Holidays
                .Where(x => x.Year == selectedYear)
                .OrderBy(x => x.Date)
                .ToListAsync();

            ViewBag.Year = selectedYear;

            return View(holidays);
        }

        // GET: Holiday/Create
        [HttpGet]
        public IActionResult Create(int year = 1405)
        {
            var model = new Holiday
            {
                Year = year,
                IsOfficial = true,
                Date = DateTime.Today
            };

            return PartialView("_HolidayForm", model);
        }

        // POST: Holiday/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Holiday model)
        {
            if (!ModelState.IsValid)
                return PartialView("_HolidayForm", model);

            bool exists = await _context.Holidays
                .AnyAsync(x =>
                    x.Year == model.Year &&
                    x.Date.Date == model.Date.Date);

            if (exists)
            {
                ModelState.AddModelError(
                    "Date",
                    "برای این تاریخ قبلاً تعطیلی ثبت شده است.");

                return PartialView("_HolidayForm", model);
            }
            model.Year = Convert.ToInt32(DateUtil.M2S(model.Date).Substring(0, 4));
            model.PersianDate = DateUtil.M2S(model.Date);

            _context.Holidays.Add(model);
            await _context.SaveChangesAsync();

            return Json(new
            {
                success = true
            });
        }

        // GET: Holiday/Edit/5
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var holiday = await _context.Holidays
                .FirstOrDefaultAsync(x => x.Id == id);

            if (holiday == null)
                return NotFound();

            return PartialView("_HolidayForm", holiday);
        }

        // POST: Holiday/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Holiday model)
        {
            if (!ModelState.IsValid)
                return PartialView("_HolidayForm", model);

            var holiday = await _context.Holidays
                .FirstOrDefaultAsync(x => x.Id == model.Id);

            model.Year =Convert.ToInt32( DateUtil.M2S(model.Date).Substring(0, 4));
            model.PersianDate = DateUtil.M2S(model.Date);

            if (holiday == null)
                return NotFound();

            bool exists = await _context.Holidays
                .AnyAsync(x =>
                    x.Id != model.Id &&
                    x.Year == model.Year &&
                    x.Date.Date == model.Date.Date);

            if (exists)
            {
                ModelState.AddModelError(
                    "Date",
                    "برای این تاریخ قبلاً تعطیلی ثبت شده است.");

                return PartialView("_HolidayForm", model);
            }

            holiday.Year = model.Year;
            holiday.Date = model.Date;
            holiday.Title = model.Title;
            holiday.IsOfficial = model.IsOfficial;
            holiday.Description = model.Description;

            await _context.SaveChangesAsync();

            return Json(new
            {
                success = true
            });
        }

        // POST: Holiday/Delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var holiday = await _context.Holidays
                .FirstOrDefaultAsync(x => x.Id == id);

            if (holiday == null)
                return Json(new
                {
                    success = false,
                    message = "تعطیلی پیدا نشد."
                });

            _context.Holidays.Remove(holiday);

            await _context.SaveChangesAsync();

            return Json(new
            {
                success = true
            });
        }

        // GET: Holiday/GetByYear
        [HttpGet]
        public async Task<IActionResult> GetByYear(int year)
        {
            var holidays = await _context.Holidays
                .Where(x => x.Year == year)
                .OrderBy(x => x.Date)
                .Select(x => new
                {
                    x.Id,
                    x.Year,
                    Date = x.Date.ToString("yyyy-MM-dd"),
                    x.Title,
                    x.IsOfficial,
                    x.Description
                })
                .ToListAsync();

            return Json(holidays);
        }
    }
}