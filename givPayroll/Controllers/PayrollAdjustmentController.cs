using givPayroll.Data;
using givPayroll.Models;
using givPayroll.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

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

        public async Task<IActionResult> Index()
        {
            return View();
        }


        // =========================================================
        // LIST
        // =========================================================

        [HttpGet]
        public async Task<IActionResult> List(
            string? description,
            int? adjustmentTypeId,
            int page = 1,
            int pageSize = 10)
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

            var totalCount = await query.CountAsync();

            var items = await query
                .OrderByDescending(x => x.Id)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Select(x => new PayrollAdjustmentListViewModel
                {
                    Id = x.Id,
                    Description = x.Description,
                    StartDate = x.StartDate,
                    EndDate = x.EndDate,
                    AdjustmentCount = x.AdjustmentCount,
                    AdjustmentTypeName =
                        x.SalaryItem != null
                            ? x.SalaryItem.SalaryItemName
                            : "",
                    Loan = x.Loan,
                    TotalAmount = x.TotalAmount,

                    PersonnelCount =
                        _context.PayrollAdjustmentPersonnels
                            .Count(p =>
                                p.PayrollAdjustmentId == x.Id)
                })
                .ToListAsync();

            var model = new PayrollAdjustmentListResultViewModel
            {
                Items = items,
                Page = page,
                PageSize = pageSize,
                TotalCount = totalCount
            };

            return PartialView("_PayrollAdjustmentList", model);
        }


        // =========================================================
        // CREATE
        // =========================================================

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var model = new PayrollAdjustmentViewModel
            {
                StartDate = DateTime.Today,
                AdjustmentCount = 1
            };

            await LoadAdjustmentTypes(model);

            return PartialView("_PayrollAdjustmentForm", model);
        }


        // =========================================================
        // EDIT
        // =========================================================

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var adjustment = await _context.PayrollAdjustments
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);

            if (adjustment == null)
                return NotFound();

            var model = new PayrollAdjustmentViewModel
            {
                Id = adjustment.Id,
                Description = adjustment.Description,
                StartDate = adjustment.StartDate,
                EndDate = adjustment.EndDate,
                AdjustmentCount = adjustment.AdjustmentCount,
                AdjustmentTypeId = adjustment.AdjustmentTypeId,
                Loan = adjustment.Loan,
                TotalAmount = adjustment.TotalAmount
            };

            model.Personnels = await _context.PayrollAdjustmentPersonnels
                .AsNoTracking()
                .Where(x => x.PayrollAdjustmentId == id)
                .Include(x => x.Personnel)
                .Select(x => new PayrollAdjustmentPersonnelViewModel
                {
                    Id = x.Id,
                    PersonnelId = x.PersonnelId,
                    PersonnelName =
                        x.Personnel!.FirstName + " " +
                        x.Personnel.LastName
                })
                .ToListAsync();

            model.PersonnelIds = model.Personnels
                .Select(x => x.PersonnelId)
                .ToList();

            await LoadAdjustmentTypes(model);

            return PartialView("_PayrollAdjustmentForm", model);
        }


        // =========================================================
        // SAVE
        // =========================================================

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Save(
            PayrollAdjustmentViewModel model)
        {
            if (!ModelState.IsValid)
            {
                await LoadAdjustmentTypes(model);

                return PartialView(
                    "_PayrollAdjustmentForm",
                    model);
            }

            // Calculate end date on server too
            model.EndDate = model.StartDate
                .AddMonths(model.AdjustmentCount)
                .AddDays(-1);

            using var transaction =
                await _context.Database.BeginTransactionAsync();

            try
            {
                PayrollAdjustment adjustment;

                if (model.Id == 0)
                {
                    var maxId =
                        await _context.PayrollAdjustments
                            .Select(x => (int?)x.Id)
                            .MaxAsync() ?? 0;

                    adjustment = new PayrollAdjustment
                    {
                        Id = maxId + 1
                    };
               
                    
                    _context.PayrollAdjustments.Add(adjustment);
                }
                else
                {
                    adjustment =
                        await _context.PayrollAdjustments
                            .FirstOrDefaultAsync(
                                x => x.Id == model.Id);

                    if (adjustment == null)
                        return NotFound();

                    // Delete old personnel
                    var oldPersonnel =
                        await _context.PayrollAdjustmentPersonnels
                            .Where(x =>
                                x.PayrollAdjustmentId == model.Id)
                            .ToListAsync();

                    _context.PayrollAdjustmentPersonnels
                        .RemoveRange(oldPersonnel);

                    // Delete old details
                    var oldDetails =
                        await _context.PayrollAdjustmentDetails
                            .Where(x =>
                                x.PayrollAdjustmentId == model.Id)
                            .ToListAsync();

                    _context.PayrollAdjustmentDetails
                        .RemoveRange(oldDetails);
                }

                adjustment.Description = model.Description;
                adjustment.StartDate = model.StartDate;
                adjustment.EndDate = model.EndDate;
                adjustment.AdjustmentCount = model.AdjustmentCount;
                adjustment.AdjustmentTypeId =  model.AdjustmentTypeId;
                adjustment.Loan = model.Loan;
                adjustment.TotalAmount = model.TotalAmount;
                adjustment.SalaryItemId = model.AdjustmentTypeId;

                await _context.SaveChangesAsync();


                // =================================================
                // PERSONNEL
                // =================================================

                var personnelIds =
                    model.PersonnelIds
                        .Distinct()
                        .ToList();

                var personnelMaxId =
                    await _context.PayrollAdjustmentPersonnels
                        .Select(x => (int?)x.Id)
                        .MaxAsync() ?? 0;

                foreach (var personnelId in personnelIds)
                {
                    _context.PayrollAdjustmentPersonnels.Add(
                        new PayrollAdjustmentPersonnel
                        {
                            Id = ++personnelMaxId,
                            PayrollAdjustmentId =
                                adjustment.Id,
                            PersonnelId = personnelId
                        });
                }


                // =================================================
                // DETAILS
                // =================================================

                var detailMaxId =
                    await _context.PayrollAdjustmentDetails
                        .Select(x => (int?)x.Id)
                        .MaxAsync() ?? 0;

                var amountPerInstallment =
                    model.AdjustmentCount > 0
                        ? model.TotalAmount /
                          model.AdjustmentCount
                        : 0;

                var currentDate = model.StartDate;

                for (int i = 0;
                     i < model.AdjustmentCount;
                     i++)
                {
                    foreach (var personnelId in personnelIds)
                    {
                        var detail =
                            new PayrollAdjustmentDetail
                            {
                                Id = ++detailMaxId,

                                PayrollAdjustmentId =
                                    adjustment.Id,

                                Description =
                                    model.Description,

                                PayrollYear =
                                    currentDate.Year,

                                PayrollMonth =
                                    currentDate.Month,

                                Amount =
                                    amountPerInstallment
                            };

                        _context.PayrollAdjustmentDetails
                            .Add(detail);
                    }

                    currentDate =
                        currentDate.AddMonths(1);
                }

                await _context.SaveChangesAsync();

                await transaction.CommitAsync();

                return Json(new
                {
                    success = true,
                    message = "تعدیل حقوق با موفقیت ذخیره شد"
                });
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();

                return Json(new
                {
                    success = false,
                    message = ex.Message
                });
            }
        }


        // =========================================================
        // DELETE
        // =========================================================

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var adjustment =
                await _context.PayrollAdjustments
                    .FirstOrDefaultAsync(x => x.Id == id);

            if (adjustment == null)
                return Json(new
                {
                    success = false,
                    message = "رکورد پیدا نشد"
                });

            var personnel =
                await _context.PayrollAdjustmentPersonnels
                    .Where(x =>
                        x.PayrollAdjustmentId == id)
                    .ToListAsync();

            var details =
                await _context.PayrollAdjustmentDetails
                    .Where(x =>
                        x.PayrollAdjustmentId == id)
                    .ToListAsync();

            _context.PayrollAdjustmentPersonnels
                .RemoveRange(personnel);

            _context.PayrollAdjustmentDetails
                .RemoveRange(details);

            _context.PayrollAdjustments.Remove(adjustment);

            await _context.SaveChangesAsync();

            return Json(new
            {
                success = true,
                message = "رکورد حذف شد"
            });
        }


        // =========================================================
        // PERSONNEL LIST
        // =========================================================

        [HttpGet]
        public async Task<IActionResult> PersonnelList(
            string? search)
        {
            var query = _context.Personnels
                .AsNoTracking()
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(search))
            {
                query = query.Where(x =>
                    x.FirstName.Contains(search) ||
                    x.LastName.Contains(search));
            }

            var result = await query
                .OrderBy(x => x.FirstName)
                .ThenBy(x => x.LastName)
                .Select(x => new
                {
                    id = x.Id,
                    name =
                        x.FirstName + " " +
                        x.LastName
                })
                .ToListAsync();

            return Json(result);
        }


        private async Task LoadAdjustmentTypes(
            PayrollAdjustmentViewModel model)
        {
            model.AdjustmentTypes =
                await _context.SalaryItems
                    .AsNoTracking()
                    .OrderBy(x => x.SalaryItemName)
                    .Where(i =>
                        i.Label == "Bonus" ||
                        i.Label == "Penalty")
                    .Select(x => new SelectListItem
                    {
                        Value = x.Id.ToString(),
                        Text = x.SalaryItemName
                    })
                    .ToListAsync();
        }
    }
}