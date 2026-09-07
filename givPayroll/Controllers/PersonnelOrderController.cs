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
    public class PersonnelOrderController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PersonnelOrderController(
            ApplicationDbContext context)
        {
            _context = context;
        }


        // =========================================================
        // INDEX
        // =========================================================

        [HttpGet]
        public async Task<IActionResult> Index(
            string? search,
            int page = 1,
            int pageSize = 10)
        {
            var query = _context.PersonnelOrders
                .AsNoTracking()
                .Include(x => x.Personnel)
                .Include(x => x.Contract)
                .OrderByDescending(x => x.No)
                .AsQueryable();


            if (!string.IsNullOrWhiteSpace(search))
            {
                search = search.Trim();

                query = query.Where(x =>
                    x.Personnel!.FirstName.Contains(search) ||
                    x.Personnel.LastName.Contains(search) ||
                    x.No.ToString().Contains(search) );
            }


            var totalCount = await query.CountAsync();

            var totalPages =
                (int)Math.Ceiling(
                    totalCount / (double)pageSize);


            if (page < 1)
                page = 1;

            if (totalPages > 0 && page > totalPages)
                page = totalPages;


            var orders = await query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();


            ViewBag.Search = search;
            ViewBag.Page = page;
            ViewBag.PageSize = pageSize;
            ViewBag.TotalPages = totalPages;
            ViewBag.TotalCount = totalCount;


            return View(orders);
        }


        // ============================================================
        // LIST
        // ============================================================

        [HttpGet]
        public async Task<IActionResult> List(
            string search = "",
            int page = 1,
            int pageSize = 10,
            string sortColumn = "Id",
            string sortDirection = "desc")
        {
            var query = _context.PersonnelOrders
                .AsNoTracking()
                .Include(x => x.Personnel)
                .Include(x => x.Contract)
                .Include(x => x.RuleInsuranceGroup)
                .AsQueryable();


            // --------------------------------------------------------
            // Search
            // --------------------------------------------------------

            if (!string.IsNullOrWhiteSpace(search))
            {
                search = search.Trim();

                if (int.TryParse(search, out int no))
                {
                    query = query.Where(x =>
                        x.No == no ||
                        x.Personnel!.FirstName.Contains(search) ||
                        x.Personnel.LastName.Contains(search));
                }
                else
                {
                    query = query.Where(x =>
                        x.Personnel!.FirstName.Contains(search) ||
                        x.Personnel.LastName.Contains(search));
                }
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

                ("No", "asc") =>
                    query.OrderBy(x => x.No),

                ("No", "desc") =>
                    query.OrderByDescending(x => x.No),

                ("Date", "asc") =>
                    query.OrderBy(x => x.IssueDate),

                ("Date", "desc") =>
                    query.OrderByDescending(x => x.IssueDate),

                ("Personnel", "asc") =>
                    query.OrderBy(x =>
                        x.Personnel!.LastName),

                ("Personnel", "desc") =>
                    query.OrderByDescending(x =>
                        x.Personnel!.LastName),

                ("IsActive", "asc") =>
                    query.OrderBy(x => x.IsActive),

                ("IsActive", "desc") =>
                    query.OrderByDescending(x => x.IsActive),

                _ =>
                    query.OrderByDescending(x => x.Id)
            };


            // --------------------------------------------------------
            // Total
            // --------------------------------------------------------

            var total = await query.CountAsync();


            // --------------------------------------------------------
            // Paging
            // --------------------------------------------------------

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
                .ToListAsync();


            ViewBag.SortColumn = sortColumn;
            ViewBag.SortDirection = sortDirection;


            return PartialView(
                "_PersonnelOrderList",
                new PagedResult<PersonnelOrder>
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
            var model = new PersonnelOrderViewModel
            {
                IsActive = true,
                Personnel = new Personnel()
            };

            await PrepareSelectLists(model);

            model.PersonnelList =
              await _context.Contracts
                  .AsNoTracking()
                  .Join(
                      _context.Personnels,
                      pc => pc.PersonnelId,
                      p => p.Id,
                      (pc, p) => p
                  )
                  .OrderBy(p => p.LastName)
                  .ThenBy(p => p.FirstName)
                  .Select(p => new SelectListItem
                  {
                      Value = p.Id.ToString(),
                      Text = p.FirstName + " " + p.LastName,
                      Selected = p.Id == model.PersonnelId
                  })
                  .ToListAsync();

            // فقط SalaryItemهای مربوط به PersonnelOrder
            model.Details = await GetSalaryItemDetails();
            model.IssueDate = DateTime.Now;
            var maxNo = await _context.PersonnelOrders
               .Select(x => (int?)x.No)
               .MaxAsync();

            model.StartDate = DateTime.Now.Date;
            model.EndDate = DateTime.Now.Date.AddYears(1);
           

            foreach (var item in model.Details)
            {
                int SalaryItemId = item.SalaryItemId;
                var (lastDate, lastAmount) =
                    await GetLastSalaryItemRuleAsync(SalaryItemId);
                if (lastAmount != null)
                    item.Amount = lastAmount.GetValueOrDefault();
            }

            model.No = (maxNo ?? 0) + 1;

            return PartialView(
                "_PersonnelOrderForm",
                model);
        }

        private async Task<(DateTime? LastDate, decimal? LastAmount)> GetLastSalaryItemRuleAsync(int salaryItemId)
        {
            var rule = await _context.SalaryItemRules
                .Where(x => x.SalaryItemId == salaryItemId)
                .OrderByDescending(x => x.EffectiveDate)
                .Select(x => new
                {
                    LastDate = x.EffectiveDate,
                    LastAmount = x.Amount
                })
                .FirstOrDefaultAsync();

            return rule == null
                ? (null, null)
                : (rule.LastDate, rule.LastAmount);
        }
        // =========================================================
        // CREATE POST
        // =========================================================

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(  PersonnelOrderViewModel model)
        {
            ModelState.Remove("Contract");
            ModelState.Remove("Job");
            ModelState.Remove("Personnel");
            if (!ModelState.IsValid)
            {
                await PrepareSelectLists(model);

                await PrepareDetailNames(model);

                return PartialView(
                    "_PersonnelOrderForm",
                    model);
            }


            // شماره حکم تکراری نباشد
            //var duplicateNo =
            //    await _context.PersonnelOrders.AnyAsync(x =>
            //        x.PersonnelId == model.PersonnelId &&
            //        x.No == model.No);


            //if (duplicateNo)
            //{
            //    ModelState.AddModelError(
            //        nameof(model.No),
            //        "این شماره حکم برای این پرسنل قبلاً ثبت شده است.");

            //    await PrepareSelectLists(model);
            //    await PrepareDetailNames(model);

            //    return PartialView(
            //        "_PersonnelOrderForm",
            //        model);
            //}


            using var transaction =
                await _context.Database.BeginTransactionAsync();

            try
            {
                await _context.PersonnelOrders
                 .Where(x => x.PersonnelId == model.PersonnelId)
                 .ExecuteUpdateAsync(setters =>
                     setters.SetProperty(x => x.IsActive, false));

                PersonnelContract con =await _context.Contracts
                 .Where(x => x.PersonnelId == model.PersonnelId && x.IsActive).FirstOrDefaultAsync();

                var order = new PersonnelOrder
                {
                    PersonnelId = model.PersonnelId,
                    ContractId = con.Id,
                    RuleInsuranceGroupId =model.RuleInsuranceGroupId,
                    JobId = model.JobId,
                    No = model.No,
                    IssueDate = model.IssueDate,
                    StartDate = model.StartDate,
                    EndDate = model.EndDate,
                    Description = model.Description,
                    IsActive = model.IsActive,
                     
                    DateCreated = DateTime.Now,
                    UserCreated = GetCurrentUserId()
                };

                var maxNo = await _context.PersonnelOrders
                     .Select(x => (int?)x.No)
                      .MaxAsync();

                order.No = (maxNo ?? 0) + 1;

                var maxId = await _context.PersonnelOrders
                   .Select(x => (int?)x.Id)
                    .MaxAsync();

                order.Id = (maxId ?? 0) + 1;

                _context.PersonnelOrders.Add(order);

                await _context.SaveChangesAsync();

                var maxOrdDetailId = await _context.PersonnelOrderDetails
                 .Select(x => (int?)x.Id)
                  .MaxAsync();

                maxOrdDetailId = (maxOrdDetailId ?? 0) + 1;
                // Details
                foreach (var detail in model.Details)
                {
                    var salaryItemExists =
                        await _context.SalaryItems.AnyAsync(x =>
                            x.Id == detail.SalaryItemId &&
                            x.Source == "PersonnelOrder");


                    if (!salaryItemExists)
                        continue;


                    var orderDetail =
                        new PersonnelOrderDetail
                        {
                            PersonnelOrderId = order.Id,
                            SalaryItemId = detail.SalaryItemId,
                            Amount = detail.Amount,

                            DateCreated = DateTime.Now,
                            UserCreated = GetCurrentUserId()
                        };

                    orderDetail.Id = maxOrdDetailId.GetValueOrDefault();
                    _context.PersonnelOrderDetails.Add(orderDetail);

                    maxOrdDetailId += 1;
                }


                await _context.SaveChangesAsync();

                await transaction.CommitAsync();


                return Json(new
                {
                    success = true,
                    message = " حکم پرسنلی با شماره " + order.No + " با موفقیت ثبت شد."
                });
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();

                throw;
            }
        }


        // =========================================================
        // EDIT GET
        // =========================================================

        [HttpGet]
        public async Task<IActionResult> Edit(long id)
        {
            var order = await _context.PersonnelOrders
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);


            if (order == null)
                return NotFound();


            var model = new PersonnelOrderViewModel
            {
                Id = order.Id,

                PersonnelId = order.PersonnelId,

                ContractId = order.ContractId,

                RuleInsuranceGroupId =
                    order.RuleInsuranceGroupId.GetValueOrDefault(),

                No = order.No,

                IssueDate = order.IssueDate ,
                StartDate = order.StartDate,
                EndDate = order.EndDate,
                JobId = order.JobId,
                Description = order.Description,

                IsActive = order.IsActive
            };

             model.Personnel =await _context.Personnels.FindAsync(model.PersonnelId);

            await PrepareSelectLists(model);


            var salaryItems =
                await _context.SalaryItems
                    .AsNoTracking()
                    .Where(x =>
                        x.Source == "PersonnelOrder")
                    .OrderBy(x => x.Priority)
                    .ToListAsync();


            var existingDetails =
                await _context.PersonnelOrderDetails
                    .AsNoTracking()
                    .Where(x =>
                        x.PersonnelOrderId == id)
                    .ToDictionaryAsync(
                        x => x.SalaryItemId,
                        x => x);


            model.Details =
                salaryItems.Select(x =>
                {
                    existingDetails.TryGetValue(
                        x.Id,
                        out var detail);

                    return new PersonnelOrderDetailViewModel
                    {
                        Id = detail?.Id ?? 0,

                        SalaryItemId = x.Id,
                       
                        SalaryItemName = x.SalaryItemName,

                        SalaryItemLabel = x.Label,

                        Unit = x.Unit,

                        Amount = detail?.Amount ?? 0
                    };

                }).ToList();


            return PartialView(
                "_PersonnelOrderForm",
                model);
        }


        // =========================================================
        // EDIT POST
        // =========================================================

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(
            PersonnelOrderViewModel model)
        {
            ModelState.Remove(nameof(model.Contract));
            ModelState.Remove(nameof(model.Job));
            ModelState.Remove("Personnel");

            if (!ModelState.IsValid)
            {
                await PrepareSelectLists(model);

                await PrepareDetailNames(model);

                return PartialView(
                    "_PersonnelOrderForm",
                    model);
            }


            var order =
                await _context.PersonnelOrders
                    .FirstOrDefaultAsync(x =>
                        x.Id == model.Id);


            if (order == null)
                return NotFound();


            using var transaction =
                await _context.Database
                    .BeginTransactionAsync();


            try
            {
                order.PersonnelId = model.PersonnelId;

                order.ContractId = model.ContractId;

                order.RuleInsuranceGroupId =
                    model.RuleInsuranceGroupId;

                order.IsActive = true;
                order.No = model.No;

                order.IssueDate = model.IssueDate;
                order.StartDate = model.StartDate;
                order.EndDate = model.EndDate;

                order.Description = model.Description;
                order.PersonnelId = model.PersonnelId;
                order.IsActive = true;

                order.DateChanged = DateTime.Now;

                order.UserChanged =
                    GetCurrentUserId();
                
                // حذف Details قبلی
                //var oldDetails =
                //    await _context.PersonnelOrderDetails
                //        .Where(x =>
                //            x.PersonnelOrderId == order.Id)
                //        .ToListAsync();


                //_context.PersonnelOrderDetails.RemoveRange(
                //    oldDetails);


                // اضافه کردن Details جدید
                foreach (var detail in model.Details)
                {
                    //var validSalaryItem =
                    //    await _context.SalaryItems.AnyAsync(x =>
                    //        x.Id == detail.SalaryItemId &&
                    //        x.Source == "PersonnelOrder");


                    //if (!validSalaryItem)
                    //    continue;
                    var orderDetail =
               await _context.PersonnelOrderDetails.FirstOrDefaultAsync(x => x.Id == detail.Id);

                    //_context.PersonnelOrderDetails.Add(
                    // new PersonnelOrderDetail
                    //{
                    //PersonnelOrderId = order.Id,

                    //SalaryItemId =
                    //   detail.SalaryItemId,

                    orderDetail.Amount = detail.Amount;

                            //DateCreated = DateTime.Now,

                            //UserCreated =
                       //         GetCurrentUserId()
                        //});
                }


                await _context.SaveChangesAsync();

                await transaction.CommitAsync();


                return Json(new
                {
                    success = true,
                    message = "حکم پرسنلی با شماره " + order.No + " موفقیت ویرایش شد."
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(long id)
        {
            var order =
                await _context.PersonnelOrders
                    .FirstOrDefaultAsync(x =>
                        x.Id == id);


            if (order == null)
            {
                return Json(new
                {
                    success = false,
                    message = "حکم پیدا نشد."
                });
            }


            _context.PersonnelOrders.Remove(order);

            await _context.SaveChangesAsync();


            return Json(new
            {
                success = true,
                message = "حکم پرسنلی حذف شد."
            });
        }


        // =========================================================
        // HELPERS
        // =========================================================

        private async Task<List<PersonnelOrderDetailViewModel>>
            GetSalaryItemDetails()
        {

            return await _context.SalaryItems
                .AsNoTracking()
                .Where(x =>
                    x.Source == "PersonnelOrder")
                .OrderBy(x => x.Priority)
                .Select(x =>
                    new PersonnelOrderDetailViewModel
                    {
                        SalaryItemId = x.Id,
                        SalaryItemName = x.SalaryItemName,
                        SalaryItemLabel = x.Label,
                        Unit = x.Unit,
                        Amount = 0
                    })
                .ToListAsync();
        }


        private async Task PrepareDetailNames(
            PersonnelOrderViewModel model)
        {
            var ids = model.Details
                .Select(x => x.SalaryItemId)
                .ToList();


            var items =
                await _context.SalaryItems
                    .AsNoTracking()
                    .Where(x =>
                        ids.Contains(x.Id) &&
                        x.Source == "PersonnelOrder")
                    .ToListAsync();


            foreach (var detail in model.Details)
            {
                var item = items.FirstOrDefault(x =>
                    x.Id == detail.SalaryItemId);


                if (item == null)
                    continue;


                detail.SalaryItemName = item.SalaryItemName;

                detail.SalaryItemLabel = item.Label;

                detail.Unit = item.Unit;
            }
        }


        private async Task PrepareSelectLists(
        PersonnelOrderViewModel model)
        {
            model.JobList = await _context.Job
                .AsNoTracking()
                .OrderBy(x => x.Title)
                .Select(x => new SelectListItem
                {
                    Value = x.Id.ToString(),
                    Text = x.Title
                })
                .ToListAsync();

            // Personnel
            model.PersonnelList =
                await _context.Personnels
                    .AsNoTracking()
                    .OrderBy(x => x.LastName)
                    .ThenBy(x => x.FirstName)
                    .Select(x => new SelectListItem
                    {
                        Value = x.Id.ToString(),
                        Text =
                            x.FirstName + " " +
                            x.LastName,

                        Selected =
                            x.Id == model.PersonnelId
                    })
                    .ToListAsync();


            // Contracts
            model.ContractList =
                await _context.Contracts
                    .AsNoTracking()
                    .Where(x =>
                        x.PersonnelId == model.PersonnelId)
                    .Select(x => new SelectListItem
                    {
                        Value = x.Id.ToString(),

                        Text = "قرارداد " + x.Id,

                        Selected =
                            x.Id == model.ContractId
                    })
                    .ToListAsync();


            // Insurance Groups
            model.RuleInsuranceGroupList =
                await _context.RuleInsuranceGroups
                    .AsNoTracking()
                    .Where(x => x.Id != 0)
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

        [HttpGet]
        public async Task<IActionResult> GetContracts(int personnelId)
        {
            var contracts = await _context.Contracts
                .AsNoTracking()
                .Where(x => x.PersonnelId == personnelId)
                .OrderByDescending(x => x.Id)
                .Select(x => new
                {
                    id = x.Id,
                    text = "قرارداد " + x.Id
                })
                .ToListAsync();

            return Json(contracts);
        }

        [HttpGet]
        public async Task<IActionResult> GetContract(int personnelId)
        {
            //var contracts1 = await _context.Contracts
            //    .AsNoTracking()
            //    .Where(x => x.PersonnelId == personnelId)
            //    .ToListAsync();

            //var activeContracts =  contracts1
            //    .Where(x => x.IsActive)
            //    .ToList();

            var contracts = await _context.Contracts
              .AsNoTracking()
              .Where(x => x.PersonnelId == personnelId && x.IsActive)
              .FirstOrDefaultAsync();

            return Json(contracts);
        }

        private string GetCurrentUserId()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(userId))
                throw new InvalidOperationException(
                    "کاربر جاری احراز هویت نشده است.");

            return userId;
        }
    }
}