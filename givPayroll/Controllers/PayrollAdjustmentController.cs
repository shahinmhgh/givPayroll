using givPayroll.Data;
using givPayroll.Models;
using givPersonnel.Models.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using givPayroll.Data;
using givPayroll.Models;
using Microsoft.IdentityModel.Tokens;


namespace givPayroll.Controllers
{
    [Authorize]
    public class PayrollAdjustmentController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PayrollAdjustmentController(ApplicationDbContext context)
        {
            _context = context;
        }


        // =========================================================
        // INDEX
        // =========================================================

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

         
        // =========================================================
        // LIST
        // =========================================================

        [HttpGet]  
        public async Task<IActionResult> List(
            string description = "",
            int? adjustmentTypeId = null,
            int page = 1,
            int pageSize = 10,
            string sortColumn = "StartDate",
            string sortDirection = "desc")
        {
            var query = _context.PayrollAdjustments
                .AsNoTracking()
                .Include(x => x.SalaryItem)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(description))
            {
                query = query.Where(x =>
                    x.Description.Contains(description));
            }

            if (adjustmentTypeId.HasValue)
            {
                query = query.Where(x =>
                    x.AdjustmentTypeId == adjustmentTypeId.Value);
            }

            query = (sortColumn, sortDirection) switch
            {
                ("Id", "asc") =>
                    query.OrderBy(x => x.Id),

                ("Id", "desc") =>
                    query.OrderByDescending(x => x.Id),

                ("StartDate", "asc") =>
                    query.OrderBy(x => x.StartDate),

                ("StartDate", "desc") =>
                    query.OrderByDescending(x => x.StartDate),

                ("EndDate", "asc") =>
                    query.OrderBy(x => x.EndDate),

                ("EndDate", "desc") =>
                    query.OrderByDescending(x => x.EndDate),

                ("TotalAmount", "asc") =>
                    query.OrderBy(x => x.TotalAmount),

                ("TotalAmount", "desc") =>
                    query.OrderByDescending(x => x.TotalAmount),

                _ =>
                    query.OrderByDescending(x => x.StartDate)
            };

            var total = await query.CountAsync();

            var data = await query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            ViewBag.SortColumn = sortColumn;
            ViewBag.SortDirection = sortDirection;

            return PartialView(
                "_PayrollAdjustmentList",
                new PagedResult<PayrollAdjustment>
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


        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var adjustment = await _context.PayrollAdjustments
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);

            if (adjustment == null)
                return NotFound();


            // Get selected personnel
            var personnelIds =
                await _context.PayrollAdjustmentPersonnels
                    .AsNoTracking()
                    .Where(x => x.PayrollAdjustmentId == id)
                    .Select(x => x.PersonnelId)
                    .ToListAsync();


            var model = new PayrollAdjustmentViewModel
            {
                Id = adjustment.Id,

                AdjustmentTypeId =
                    adjustment.AdjustmentTypeId,

                StartDate =
                    adjustment.StartDate,

                EndDate =
                    adjustment.EndDate,

                Description =
                    adjustment.Description,

                PersonnelIds =
                    personnelIds,

                // Amount in PayrollAdjustment is total amount,
                // so calculate the monthly/person amount.
                Amount =
                    personnelIds.Count > 0 &&
                    adjustment.AdjustmentCount > 0
                        ? adjustment.TotalAmount /
                          personnelIds.Count /
                          adjustment.AdjustmentCount
                        : 0
            };


            await LoadFormData(model);


            return PartialView(
                "_PayrollAdjustmentForm",
                model);
        }

        // =========================================================
        // CREATE MODAL
        // =========================================================

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var model = new PayrollAdjustmentViewModel
            {
                StartDate = DateTime.Now.Date,
                EndDate = DateTime.Now.Date,
                Amount = 0
            };

            await LoadFormData(model);

            return PartialView(
                "_PayrollAdjustmentForm",
                model);
        }


        // =========================================================
        // SAVE
        // =========================================================

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Save(
            PayrollAdjustmentViewModel model)
        {
            if (model.PersonnelIds == null ||
                model.PersonnelIds.Count == 0)
            {
                ModelState.AddModelError(
                    nameof(model.PersonnelIds),
                    "حداقل یک پرسنل را انتخاب کنید.");
            }

            if (!ModelState.IsValid)
            {
                await LoadFormData(model);

                return PartialView(
                    "_PayrollAdjustmentForm",
                    model);
            }

            // -----------------------------------------
            // Parse Persian dates
            // -----------------------------------------

            //if (!TryParsePersianDate(
            //        model.StartDate,
            //        out DateTime startDate))
            //{
            //    ModelState.AddModelError(
            //        nameof(model.StartDate),
            //        "تاریخ شروع نامعتبر است.");
            //}
            
            //if (!TryParsePersianDate(
            //        model.EndDate,
            //        out DateTime endDate))
            //{
            //    ModelState.AddModelError(
            //        nameof(model.EndDate),
            //        "تاریخ پایان نامعتبر است.");
            //}

            if (ModelState.ErrorCount > 0)
            {
                await LoadFormData(model);

                return PartialView(
                    "_PayrollAdjustmentForm",
                    model);
            }

            if (model.EndDate < model.StartDate)
            {
                ModelState.AddModelError(
                    nameof(model.EndDate),
                    "تاریخ پایان نمی‌تواند قبل از تاریخ شروع باشد.");

                await LoadFormData(model);

                return PartialView(
                    "_PayrollAdjustmentForm",
                    model);
            }


            // -----------------------------------------
            // Adjustment Type
            // -----------------------------------------

            var adjustmentType =
                await _context.SalaryItems
                    .FirstOrDefaultAsync(x =>
                        x.Id == model.AdjustmentTypeId);

            if (adjustmentType == null)
                return NotFound();


            // -----------------------------------------
            // Personnel
            // -----------------------------------------

            var personnelIds =
                model.PersonnelIds
                    .Distinct()
                    .ToList();

            var personnels =
                await _context.Personnels
                    .Where(x => personnelIds.Contains(x.Id))
                    .ToListAsync();

            if (personnels.Count != personnelIds.Count)
            {
                ModelState.AddModelError(
                    nameof(model.PersonnelIds),
                    "یکی از پرسنل‌های انتخاب شده معتبر نیست.");

                await LoadFormData(model);

                return PartialView(
                    "_PayrollAdjustmentForm",
                    model);
            }


            // -----------------------------------------
            // Calculate months
            // -----------------------------------------

            var months =
                GetPayrollMonths(model.StartDate, model.EndDate);

            if (months.Count == 0)
            {
                ModelState.AddModelError(
                    nameof(model.StartDate),
                    "بازه زمانی حداقل باید شامل یک ماه باشد.");

                await LoadFormData(model);

                return PartialView(
                    "_PayrollAdjustmentForm",
                    model);
            }


            // -----------------------------------------
            // Total amount
            // -----------------------------------------

            decimal totalAmount =
                model.Amount
                * personnelIds.Count
                * months.Count;


            // -----------------------------------------
            // Transaction
            // -----------------------------------------

            await using var transaction =
                await _context.Database.BeginTransactionAsync();

            try
            {
                // =====================================
                // PayrollAdjustment
                // =====================================

                var maxId =
                    await _context.PayrollAdjustments
                        .Select(x => (int?)x.Id)
                        .MaxAsync();

                var adjustmentId =
                    (maxId ?? 0) + 1;


                var adjustment =
                    new PayrollAdjustment
                    {
                        Id = adjustmentId,

                        Description =
                            model.Description ?? "",

                        StartDate =
                            model.StartDate,

                        EndDate =
                            model.EndDate,

                        AdjustmentCount =
                            months.Count,

                        AdjustmentTypeId =
                            model.AdjustmentTypeId,

                        SalaryItemId =
                            model.AdjustmentTypeId,

                        TotalAmount =
                            totalAmount
                    };


                _context.PayrollAdjustments
                    .Add(adjustment);


                // =====================================
                // PayrollAdjustmentPersonnel
                // =====================================

                var maxPersonnelId =
                    await _context.PayrollAdjustments
                        .Select(x => (int?)x.Id)
                        .MaxAsync();

                int nextPersonnelId =
                    (maxPersonnelId ?? 0) + 1;


                foreach (var personnelId in personnelIds)
                {
                    _context.PayrollAdjustmentPersonnels.Add(
                        new PayrollAdjustmentPersonnel
                        {
                            Id = nextPersonnelId++,

                            PayrollAdjustmentId =
                                adjustmentId,

                            PersonnelId =
                                personnelId
                        });
                }


                // =====================================
                // PayrollAdjustmentDetail
                // =====================================

                var maxDetailId =
                    await _context.PayrollAdjustmentDetails
                        .Select(x => (int?)x.Id)
                        .MaxAsync();

                int nextDetailId =
                    (maxDetailId ?? 0) + 1;


                foreach (var personnel in personnels)
                {
                    foreach (var month in months)
                    {
                        _context.PayrollAdjustmentDetails.Add(
                            new PayrollAdjustmentDetail
                            {
                                Id = nextDetailId++,

                                PayrollAdjustmentId =
                                    adjustmentId,

                                Description =
                                    model.Description ?? "",

                         

                                PayrollYear =
                                    month.Year,

                                PayrollMonth =
                                    month.Month,

                                Amount =
                                    model.Amount
                            });
                    }
                }


                await _context.SaveChangesAsync();

                await transaction.CommitAsync();


                return Json(new
                {
                    success = true,
                    id = adjustmentId
                });
            }
            catch
            {
                await transaction.RollbackAsync();

                throw;
            }
        }


        // =========================================================
        // DELETE
        // =========================================================

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var adjustment =
                await _context.PayrollAdjustments
                    .FirstOrDefaultAsync(x => x.Id == id);

            if (adjustment == null)
                return NotFound();


            await using var transaction =
                await _context.Database.BeginTransactionAsync();

            try
            {
                var details =
                    await _context.PayrollAdjustmentDetails
                        .Where(x =>
                            x.PayrollAdjustmentId == id)
                        .ToListAsync();

                _context.PayrollAdjustmentDetails
                    .RemoveRange(details);


                var personnel =
                    await _context.PayrollAdjustmentPersonnels
                        .Where(x =>
                            x.PayrollAdjustmentId == id)
                        .ToListAsync();

                _context.PayrollAdjustmentPersonnels
                    .RemoveRange(personnel);


                _context.PayrollAdjustments
                    .Remove(adjustment);


                await _context.SaveChangesAsync();

                await transaction.CommitAsync();


                return Json(new
                {
                    success = true
                });
            }
            catch
            {
                await transaction.RollbackAsync();

                throw;
            }
        }

        public async Task<IActionResult> ListForSelect()
        {
            var adjustmentTypes =
                await _context.SalaryItems
                    .AsNoTracking()
                    .Where(x => x.Label == "Bonus" || x.Label == "Penalty")
                    .OrderBy(x => x.SalaryItemName)
                    .Select(x => new SelectListItem
                    {
                        Value = x.Id.ToString(),
                        Text = x.SalaryItemName
                    })
                    .ToListAsync();

            return Json(adjustmentTypes);
        }

        // =========================================================
        // FORM DATA
        // =========================================================

        private async Task LoadFormData(
            PayrollAdjustmentViewModel model)
        {
            model.AdjustmentTypes =
                await _context.SalaryItems
                    .AsNoTracking()
                    .OrderBy(x => x.SalaryItemName)
                    .Where (i=>i.Label=="Bonus" || i.Label=="Penalty")
                    .Select(x => new SelectListItem
                    {
                        Value = x.Id.ToString(),
                        Text = x.SalaryItemName
                    })
                    .ToListAsync();

            model.Personnels =
                await _context.Personnels
                    .AsNoTracking()
                    .OrderBy(x => x.LastName)
                    .ThenBy(x => x.FirstName)
                    .ToListAsync();
        }


        // =========================================================
        // PERSIAN DATE
        // =========================================================

        private bool TryParsePersianDate(
            string value,
            out DateTime date)
        {
            date = default;

            if (string.IsNullOrWhiteSpace(value))
                return false;

            value = value.Trim()
                .Replace("-", "/");

            var parts =
                value.Split('/');

            if (parts.Length != 3)
                return false;

            if (!int.TryParse(parts[0], out int year))
                return false;

            if (!int.TryParse(parts[1], out int month))
                return false;

            if (!int.TryParse(parts[2], out int day))
                return false;

            try
            {
                var pc =
                    new PersianCalendar();

                date =
                    pc.ToDateTime(
                        year,
                        month,
                        day,
                        0,
                        0,
                        0,
                        0);

                return true;
            }
            catch
            {
                return false;
            }
        }


        // =========================================================
        // GET PAYROLL MONTHS
        // =========================================================

        private List<(int Year, int Month)>
            GetPayrollMonths(
                DateTime startDate,
                DateTime endDate)
        {
            var result =
                new List<(int Year, int Month)>();

            var pc =
                new PersianCalendar();

            int startYear =
                pc.GetYear(startDate);

            int startMonth =
                pc.GetMonth(startDate);

            int endYear =
                pc.GetYear(endDate);

            int endMonth =
                pc.GetMonth(endDate);


            int currentYear =
                startYear;

            int currentMonth =
                startMonth;


            while (
                currentYear < endYear ||
                (currentYear == endYear &&
                 currentMonth <= endMonth))
            {
                result.Add(
                    (currentYear, currentMonth));


                currentMonth++;

                if (currentMonth > 12)
                {
                    currentMonth = 1;
                    currentYear++;
                }
            }


            return result;
        }
    }
}