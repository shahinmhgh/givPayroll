using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using givPayroll.Data;
using givPayroll.Models;

namespace givPayroll.Controllers
{
    [Authorize]
    public class RuleInsuranceController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RuleInsuranceController(ApplicationDbContext context)
        {
            _context = context;
        }


        // =========================================================
        // INDEX
        // =========================================================

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View();
        }


        // =========================================================
        // LIST
        // =========================================================

        [HttpGet]
        public async Task<IActionResult> List(
            string search = "",
            int page = 1,
            int pageSize = 10,
            string sortColumn = "Id",
            string sortDirection = "desc")
        {
            var query = _context.RuleInsurances
                .AsNoTracking()
                .Include(x => x.RuleInsuranceGroup)
                .AsQueryable();


            // ---------------------------------------------------------
            // Search
            // ---------------------------------------------------------

            if (!string.IsNullOrWhiteSpace(search))
            {
                search = search.Trim();

                query = query.Where(x =>
                    x.RuleInsuranceGroup!.GroupName.Contains(search));
            }


            // ---------------------------------------------------------
            // Sorting
            // ---------------------------------------------------------

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

                ("EmployeeRate", "asc") =>
                    query.OrderBy(x => x.EmployeeRate),

                ("EmployeeRate", "desc") =>
                    query.OrderByDescending(x => x.EmployeeRate),

                ("EmployerRate", "asc") =>
                    query.OrderBy(x => x.EmployerRate),

                ("EmployerRate", "desc") =>
                    query.OrderByDescending(x => x.EmployerRate),

                _ =>
                    query.OrderByDescending(x => x.Id)
            };


            // ---------------------------------------------------------
            // Total
            // ---------------------------------------------------------

            var total = await query.CountAsync();


            // ---------------------------------------------------------
            // Paging
            // ---------------------------------------------------------

            if (page < 1)
                page = 1;

            if (pageSize < 1)
                pageSize = 10;

            var totalPages =
                (int)Math.Ceiling(total / (double)pageSize);

            if (totalPages > 0 && page > totalPages)
                page = totalPages;


            var data = await query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Select(x => new RuleInsuranceViewModel
                {
                    Id = x.Id,
                    RuleInsuranceGroupId = x.RuleInsuranceGroupId,
                    RuleInsuranceGroupName =
                        x.RuleInsuranceGroup!.GroupName,

                    EffectiveDate = x.EffectiveDate,

                    EmployeeRate = x.EmployeeRate,
                    EmployerRate = x.EmployerRate,
                    UnemploymentRate = x.UnemploymentRate
                })
                .ToListAsync();


            ViewBag.SortColumn = sortColumn;
            ViewBag.SortDirection = sortDirection;


            return PartialView(
                "_RuleInsuranceList",
                new PagedResult<RuleInsuranceViewModel>
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
                    TotalPages = totalPages
                });
        }


        // =========================================================
        // CREATE GET
        // =========================================================

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var model = new RuleInsuranceViewModel();
            model.EffectiveDate = DateTime.Now;
            await PrepareSelectLists(model);

            return PartialView(
                "_RuleInsuranceForm",
                model);
        }


        // =========================================================
        // CREATE POST
        // =========================================================

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
            RuleInsuranceViewModel model)
        {
            if (!ModelState.IsValid)
            {
                await PrepareSelectLists(model);

                return PartialView(
                    "_RuleInsuranceForm",
                    model);
            }


            var entity = new RuleInsurance
            {
                RuleInsuranceGroupId =
                    model.RuleInsuranceGroupId,

                EffectiveDate =
                    model.EffectiveDate,

                EmployeeRate =
                    model.EmployeeRate,

                EmployerRate =
                    model.EmployerRate,

                UnemploymentRate =
                    model.UnemploymentRate,

                DateCreated =
                    DateTime.Now,

                UserCreated =
                    GetCurrentUserId()
            };

            var maxId = await _context.RuleInsurances
              .Select(x => (int?)x.Id)
              .MaxAsync();

            entity.Id = (maxId ?? 0) + 1;
            

            _context.RuleInsurances.Add(entity);

            await _context.SaveChangesAsync();


            return Json(new
            {
                success = true,
                message = "قانون بیمه با موفقیت ثبت شد."
            });
        }


        // =========================================================
        // EDIT GET
        // =========================================================

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var entity = await _context.RuleInsurances
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);

            if (entity == null)
                return NotFound();


            var model = new RuleInsuranceViewModel
            {
                Id = entity.Id,

                RuleInsuranceGroupId =
                    entity.RuleInsuranceGroupId,

                EffectiveDate =
                    entity.EffectiveDate,

                EmployeeRate =
                    entity.EmployeeRate,

                EmployerRate =
                    entity.EmployerRate,

                UnemploymentRate =
                    entity.UnemploymentRate
            };


            await PrepareSelectLists(model);


            return PartialView(
                "_RuleInsuranceForm",
                model);
        }


        // =========================================================
        // EDIT POST
        // =========================================================

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(
            RuleInsuranceViewModel model)
        {
            if (!ModelState.IsValid)
            {
                await PrepareSelectLists(model);

                return PartialView(
                    "_RuleInsuranceForm",
                    model);
            }


            var entity =
                await _context.RuleInsurances
                    .FirstOrDefaultAsync(x =>
                        x.Id == model.Id);


            if (entity == null)
                return NotFound();


            entity.RuleInsuranceGroupId =
                model.RuleInsuranceGroupId;

            entity.EffectiveDate =
                model.EffectiveDate;

            entity.EmployeeRate =
                model.EmployeeRate;

            entity.EmployerRate =
                model.EmployerRate;

            entity.UnemploymentRate =
                model.UnemploymentRate;

            entity.DateChanged =
                DateTime.Now;

            entity.UserChanged =
                GetCurrentUserId();


            await _context.SaveChangesAsync();


            return Json(new
            {
                success = true,
                message = "قانون بیمه با موفقیت ویرایش شد."
            });
        }


        // =========================================================
        // DELETE
        // =========================================================

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var entity =
                await _context.RuleInsurances
                    .FirstOrDefaultAsync(x =>
                        x.Id == id);


            if (entity == null)
            {
                return Json(new
                {
                    success = false,
                    message = "قانون بیمه پیدا نشد."
                });
            }


            _context.RuleInsurances.Remove(entity);

            await _context.SaveChangesAsync();


            return Json(new
            {
                success = true,
                message = "قانون بیمه حذف شد."
            });
        }


        // =========================================================
        // HELPERS
        // =========================================================

        private async Task PrepareSelectLists(
            RuleInsuranceViewModel model)
        {
            model.RuleInsuranceGroupList =
                await _context.RuleInsuranceGroups
                    .AsNoTracking()
                    .OrderBy(x => x.Id)
                    .Select(x => new SelectListItem
                    {
                        Value = x.Id.ToString(),

                        Text = x.GroupName,

                        Selected =
                            x.Id == model.RuleInsuranceGroupId
                    })
                    .ToListAsync();
        }


        private string GetCurrentUserId()
        {
            var value =
                User.FindFirstValue(
                    ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(value))
                throw new InvalidOperationException(
                    "کاربر جاری احراز هویت نشده است.");

            return value;
        }
    }
}