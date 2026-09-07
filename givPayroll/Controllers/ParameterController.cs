using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using givPayroll.Data;
using givPayroll.Models;

namespace givPayroll.Controllers
{
    [Authorize]
    public class ParameterController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ParameterController(ApplicationDbContext context)
        {
            _context = context;
        }


        // =========================================================
        // INDEX
        // =========================================================

        public IActionResult Index()
        {
            return View();
        }


        // =========================================================
        // LIST
        // =========================================================

        [HttpGet]
        public async Task<IActionResult> List(
            string subject = "",
            string optionKey = "",
            string optionName = "",
            int page = 1,
            int pageSize = 10,
            string sortColumn = "Subject",
            string sortDirection = "asc")
        {
            var query = _context.Parameters.AsQueryable();


            // -----------------------------------------------------
            // Search
            // -----------------------------------------------------

            if (!string.IsNullOrWhiteSpace(subject))
            {
                query = query.Where(x =>
                    x.Subject.Contains(subject));
            }


            if (!string.IsNullOrWhiteSpace(optionKey))
            {
                query = query.Where(x =>
                    x.OptionKey.Contains(optionKey));
            }


            if (!string.IsNullOrWhiteSpace(optionName))
            {
                query = query.Where(x =>
                    x.OptionName.Contains(optionName));
            }


            // -----------------------------------------------------
            // Sorting
            // -----------------------------------------------------

            query = (sortColumn, sortDirection) switch
            {
                ("Id", "asc") =>
                    query.OrderBy(x => x.Id),

                ("Id", "desc") =>
                    query.OrderByDescending(x => x.Id),

                ("Subject", "asc") =>
                    query.OrderBy(x => x.Subject),

                ("Subject", "desc") =>
                    query.OrderByDescending(x => x.Subject),

                ("OptionKey", "asc") =>
                    query.OrderBy(x => x.OptionKey),

                ("OptionKey", "desc") =>
                    query.OrderByDescending(x => x.OptionKey),

                ("OptionName", "asc") =>
                    query.OrderBy(x => x.OptionName),

                ("OptionName", "desc") =>
                    query.OrderByDescending(x => x.OptionName),

                ("OptionValue", "asc") =>
                    query.OrderBy(x => x.OptionValue),

                ("OptionValue", "desc") =>
                    query.OrderByDescending(x => x.OptionValue),

                _ =>
                    query.OrderBy(x => x.Subject)
            };


            // -----------------------------------------------------
            // Count
            // -----------------------------------------------------

            var total = await query.CountAsync();


            // -----------------------------------------------------
            // Paging
            // -----------------------------------------------------

            var data = await query
                .AsNoTracking()
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();


            ViewBag.SortColumn = sortColumn;
            ViewBag.SortDirection = sortDirection;


            return PartialView(
                "_ParameterList",
                new PagedResult<Parameter>
                {
                    Items = data,

                    PageNumber = page,

                    PageSize = pageSize,

                    PageSizes = new List<SelectListItem>
                    {
                        new SelectListItem("5", "5"),
                        new SelectListItem("10", "10"),
                        new SelectListItem("20", "20"),
                        new SelectListItem("50", "50")
                    },

                    TotalRecords = total,

                    TotalPages =
                        (int)Math.Ceiling(
                            total / (double)pageSize)
                });
        }


        // =========================================================
        // CREATE - GET
        // =========================================================

        [HttpGet]
        public IActionResult Create()
        {
            return PartialView(
                "_ParameterForm",
                new Parameter());
        }


        // =========================================================
        // CREATE - POST
        // =========================================================

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Parameter model)
        {
            if (!ModelState.IsValid)
            {
                return PartialView(
                    "_ParameterForm",
                    model);
            }


            // -----------------------------------------------------
            // Because Id is NOT Identity
            // -----------------------------------------------------

            var maxId = await _context.Parameters
                .Select(x => (int?)x.Id)
                .MaxAsync();

            model.Id = (maxId ?? 0) + 1;


            _context.Parameters.Add(model);

            await _context.SaveChangesAsync();


            return Json(new
            {
                success = true
            });
        }


        // =========================================================
        // EDIT - GET
        // =========================================================

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var item = await _context.Parameters
                .FindAsync(id);


            if (item == null)
                return NotFound();


            return PartialView(
                "_ParameterForm",
                item);
        }


        // =========================================================
        // EDIT - POST
        // =========================================================

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Parameter model)
        {
            if (!ModelState.IsValid)
            {
                return PartialView(
                    "_ParameterForm",
                    model);
            }


            var existing = await _context.Parameters
                .FindAsync(model.Id);


            if (existing == null)
                return NotFound();


            existing.Subject = model.Subject;
            existing.OptionKey = model.OptionKey;
            existing.OptionName = model.OptionName;
            existing.OptionValue = model.OptionValue;


            await _context.SaveChangesAsync();


            return Json(new
            {
                success = true
            });
        }


        // =========================================================
        // DELETE
        // =========================================================

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var item = await _context.Parameters
                .FindAsync(id);


            if (item == null)
                return NotFound();


            _context.Parameters.Remove(item);

            await _context.SaveChangesAsync();


            return Json(new
            {
                success = true
            });
        }
    }
}