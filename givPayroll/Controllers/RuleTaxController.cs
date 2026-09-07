using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using givPayroll.Data;
using givPayroll.Models;

namespace givPayroll.Controllers
{
    [Authorize]
    public class RuleTaxController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RuleTaxController(ApplicationDbContext context)
        {
            _context = context;
        }


        // ============================================================
        // INDEX
        // ============================================================

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }


        // ============================================================
        // LIST
        // ============================================================

        [HttpGet]
        public async Task<IActionResult> List(
            DateTime? effectiveDate = null,
            int page = 1,
            int pageSize = 10,
            string sortColumn = "EffectiveDate",
            string sortDirection = "desc")
        {
            var query = _context.RuleTaxes
                .AsNoTracking()
                .AsQueryable();


            // --------------------------------------------------------
            // Filter
            // --------------------------------------------------------

            if (effectiveDate.HasValue)
            {
                query = query.Where(x =>
                    x.EffectiveDate.Date ==
                    effectiveDate.Value.Date);
            }


            // --------------------------------------------------------
            // Sorting
            // --------------------------------------------------------

            query = (sortColumn, sortDirection) switch
            {
                ("Id", "asc") =>
                    query.OrderBy(x => x.Id),

                ("Id", "desc") =>
                    query.OrderByDescending(x => x.Id),

                ("EffectiveDate", "asc") =>
                    query.OrderBy(x => x.EffectiveDate),

                ("EffectiveDate", "desc") =>
                    query.OrderByDescending(x => x.EffectiveDate),

                ("Amount", "asc") =>
                    query.OrderBy(x => x.Amount),

                ("Amount", "desc") =>
                    query.OrderByDescending(x => x.Amount),

                ("Rate", "asc") =>
                    query.OrderBy(x => x.Rate),

                ("Rate", "desc") =>
                    query.OrderByDescending(x => x.Rate),

                ("DateCreated", "asc") =>
                    query.OrderBy(x => x.DateCreated),

                ("DateCreated", "desc") =>
                    query.OrderByDescending(x => x.DateCreated),

                _ =>
                    query.OrderByDescending(x => x.EffectiveDate)
            };


            // --------------------------------------------------------
            // Total records
            // --------------------------------------------------------

            var total = await query.CountAsync();


            // --------------------------------------------------------
            // Paging
            // --------------------------------------------------------

            var data = await query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();


            ViewBag.SortColumn = sortColumn;
            ViewBag.SortDirection = sortDirection;


            // --------------------------------------------------------
            // Return Partial
            // --------------------------------------------------------

            return PartialView(
                "_RuleTaxList",

                new PagedResult<RuleTax>
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


        // ============================================================
        // CREATE - GET
        // ============================================================

        [HttpGet]
        public IActionResult Create()
        {
            var model = new RuleTax
            {
                EffectiveDate = DateTime.Now,
                DateCreated = DateTime.Now
            };

            return PartialView(
                "_RuleTaxForm",
                model);
        }


        // ============================================================
        // CREATE - POST
        // ============================================================

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
            RuleTax model)
        {
            if (!ModelState.IsValid)
            {
                return PartialView(
                    "_RuleTaxForm",
                    model);
            }


            // --------------------------------------------------------
            // Generate Id
            // --------------------------------------------------------

            var maxId = await _context.RuleTaxes
                .Select(x => (int?)x.Id)
                .MaxAsync();

            model.Id = (maxId ?? 0) + 1;


            // --------------------------------------------------------
            // Audit
            // --------------------------------------------------------

            model.DateCreated = DateTime.Now;

            // TODO:
            // Replace with logged-in user ID
            model.UserCreated = 0;


            _context.RuleTaxes.Add(model);

            await _context.SaveChangesAsync();


            return Json(new
            {
                success = true,
                message = "قانون مالیات با موفقیت ثبت شد"
            });
        }


        // ============================================================
        // EDIT - GET
        // ============================================================

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var item = await _context.RuleTaxes
                .FindAsync(id);


            if (item == null)
                return NotFound();


            return PartialView(
                "_RuleTaxForm",
                item);
        }


        // ============================================================
        // EDIT - POST
        // ============================================================

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(
            RuleTax model)
        {
            if (!ModelState.IsValid)
            {
                return PartialView(
                    "_RuleTaxForm",
                    model);
            }


            var item = await _context.RuleTaxes
                .FindAsync(model.Id);


            if (item == null)
                return NotFound();


            // --------------------------------------------------------
            // Update business fields only
            // --------------------------------------------------------

            item.EffectiveDate = model.EffectiveDate;

            item.Amount = model.Amount;

            item.Rate = model.Rate;


            // --------------------------------------------------------
            // Audit
            // --------------------------------------------------------

            item.DateChanged = DateTime.Now;

            // TODO:
            // Replace with logged-in user ID
            item.UserChanged = 0;


            await _context.SaveChangesAsync();


            return Json(new
            {
                success = true,
                message = "قانون مالیات با موفقیت ویرایش شد"
            });
        }


        // ============================================================
        // DELETE
        // ============================================================

        [HttpDelete]
        public async Task<IActionResult> Delete(
            int id)
        {
            var item = await _context.RuleTaxes
                .FindAsync(id);


            if (item == null)
                return NotFound();


            _context.RuleTaxes.Remove(item);

            await _context.SaveChangesAsync();


            return Json(new
            {
                success = true,
                message = "قانون مالیات با موفقیت حذف شد"
            });
        }
    }
}