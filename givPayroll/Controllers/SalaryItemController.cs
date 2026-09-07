using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using givPayroll.Data;
using NCalc;
using givPayroll.Models;

namespace givPayroll.Controllers
{
    [Authorize]
    public class SalaryItemController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SalaryItemController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: SalaryItem
        public async Task<IActionResult> Index(
            string? search,
            int page = 1,
            int pageSize = 20,
            string sortColumn = "Priority",
            string sortDirection = "asc")
        {
            var query = _context.SalaryItems
                .Include(i=>i.SalaryItemRules)
                .Where(i=>i.Label!= "Bonus" && i.Label != "Penalty")
                .AsNoTracking()
                .AsQueryable();

            // Search
            if (!string.IsNullOrWhiteSpace(search))
            {
                search = search.Trim();

                query = query.Where(x =>
                    x.SalaryItemName.Contains(search) ||
                    x.Label.Contains(search) ||
                    x.Unit.Contains(search) ||
                    x.Source.Contains(search));
            }

            // Sort
            query = sortColumn switch
            {
                "Id" => sortDirection == "asc"
                    ? query.OrderBy(x => x.Id)
                    : query.OrderByDescending(x => x.Id),

                "Name" => sortDirection == "asc"
                    ? query.OrderBy(x => x.SalaryItemName)
                    : query.OrderByDescending(x => x.SalaryItemName),

                "Label" => sortDirection == "asc"
                    ? query.OrderBy(x => x.Label)
                    : query.OrderByDescending(x => x.Label),

                "Unit" => sortDirection == "asc"
                    ? query.OrderBy(x => x.Unit)
                    : query.OrderByDescending(x => x.Unit),

                "Source" => sortDirection == "asc"
                    ? query.OrderBy(x => x.Source)
                    : query.OrderByDescending(x => x.Source),

                "PlusMinus" => sortDirection == "asc"
                    ? query.OrderBy(x => x.PlusMinus)
                    : query.OrderByDescending(x => x.PlusMinus),

                "CalculationMode" => sortDirection == "asc"
                    ? query.OrderBy(x => x.CalculationMode)
                    : query.OrderByDescending(x => x.CalculationMode),

                _ => sortDirection == "asc"
                    ? query.OrderBy(x => x.Priority)
                    : query.OrderByDescending(x => x.Priority)
            };

            var totalCount = await query.CountAsync();

            if (pageSize <= 0)
                pageSize = 20;

            var totalPages =
                (int)Math.Ceiling(totalCount / (double)pageSize);

            if (totalPages > 0 && page > totalPages)
                page = totalPages;

            if (page < 1)
                page = 1;

            var items = await query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            ViewBag.Search = search;
            ViewBag.Page = page;
            ViewBag.PageSize = pageSize;
            ViewBag.TotalPages = totalPages;
            ViewBag.TotalCount = totalCount;
            ViewBag.SortColumn = sortColumn;
            ViewBag.SortDirection = sortDirection;

            return View(items);
        }

        // GET: SalaryItem/Create
        [HttpGet]
        public IActionResult Create()
        {
            return PartialView("_SalaryItemForm", new SalaryItem());
        }

        // POST: SalaryItem/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SalaryItem model)
        {
            if (await _context.SalaryItems.AnyAsync(x => x.Id == model.Id))
            {
                ModelState.AddModelError(
                    nameof(model.Id),
                    "این کد قبلاً استفاده شده است.");
            }

            if (string.IsNullOrWhiteSpace(model.SalaryItemName))
            {
                ModelState.AddModelError(
                    nameof(model.SalaryItemName),
                    "نام الزامی است.");
            }

            if (!ModelState.IsValid)
            {
                return PartialView("_SalaryItemForm", model);
            }

            model.SalaryItemName = model.SalaryItemName.Trim();
            model.Label ??= "";
            model.Unit ??= "";
            model.Source ??= "";
            model.CalculationMode ??= "";
            model.FormulaValue ??= "";

            await _context.SalaryItems.AddAsync(model);
            await _context.SaveChangesAsync();

            return Json(new
            {
                success = true,
                message = "قلم حقوق با موفقیت ثبت شد."
            });
        }

        // GET: SalaryItem/Edit/5
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var model = await _context.SalaryItems
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);

            if (model == null)
                return NotFound();

            return PartialView("_SalaryItemForm", model);
        }

        // POST: SalaryItem/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(SalaryItem model)
        {
            var entity = await _context.SalaryItems
                .FirstOrDefaultAsync(x => x.Id == model.Id);

            //string formula = model.FormulaValue;
            //var expression = new Expression(formula);
            //expression.Parameters["JobAllowance"] = 10_000_000m;
            //expression.Parameters["MonthDays"] = 30m;
            //expression.Parameters["DaysWorked"] = 25m;
            //var result = expression.Evaluate();
            //decimal amount = Convert.ToDecimal(result);

            if (entity == null)
                return NotFound();

            if (!ModelState.IsValid)
            {
                return PartialView("_SalaryItemForm", model);
            }

            entity.Priority = model.Priority;
            entity.SalaryItemName = model.SalaryItemName.Trim();
            entity.Label = model.Label ?? "";
            entity.Unit = model.Unit ?? "";
            entity.Source = model.Source ?? "";
            entity.PlusMinus = model.PlusMinus;
            entity.CalculationMode = model.CalculationMode ?? "";
            entity.FormulaValue = model.FormulaValue ?? "";
            entity.AccAccountCode = model.AccAccountCode;
            entity.AccUniqueId = model.AccUniqueId;

            await _context.SaveChangesAsync();

            return Json(new
            {
                success = true,
                message = "قلم حقوق با موفقیت ویرایش شد."
            });
        }

        // POST: SalaryItem/Delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var entity = await _context.SalaryItems
                .FirstOrDefaultAsync(x => x.Id == id);

            if (entity == null)
            {
                return Json(new
                {
                    success = false,
                    message = "قلم حقوق پیدا نشد."
                });
            }

            _context.SalaryItems.Remove(entity);

            await _context.SaveChangesAsync();

            return Json(new
            {
                success = true,
                message = "قلم حقوق حذف شد."
            });
        }
    }
}