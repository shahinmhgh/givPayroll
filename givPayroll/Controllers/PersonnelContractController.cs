using DocumentFormat.OpenXml.Office.CustomUI;
using givPayroll.Data;
using givPayroll.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.Contracts;
using System.Security.Claims;

namespace givPayroll.Controllers
{
    [Authorize]
    public class PersonnelContractController : Controller
    {
        private readonly ApplicationDbContext _context;


        public PersonnelContractController(ApplicationDbContext context)
        {
            _context = context;

        }


        // =========================================================
        // Index
        // =========================================================

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            ViewBag.Personnels = await GetPersonnels();
            ViewBag.ContractTypes = await GetContractTypes();
            ViewBag.ContractStatuses = await GetContractStatuses();

            return View();
        }

        [HttpGet]
        public async Task<IActionResult> GetContractNotificationCount()
        {
            var count = await _context.Personnels
                .CountAsync(p => !_context.Contracts
                    .Any(c => c.PersonnelId == p.Id));

            return Json(new
            {
                count = count
            });
        }


        // =========================================================
        // List
        // =========================================================

        [HttpGet]
        public async Task<IActionResult> List(
            string firstName = "",
            string lastName = "",
            int personnelId = 0,
            int contractTypeId = 0,
            int statusId = 0,
            int page = 1,
            int pageSize = 10,
            string sortColumn = "Id",
            string sortDirection = "desc")
        {
            var query = _context.Contracts
                .AsNoTracking()
                .Include(x => x.Personnel)
                .Include(x => x.ContractType)
                .AsQueryable();


            // -----------------------------------------------------
            // Search
            // -----------------------------------------------------

            if (!string.IsNullOrWhiteSpace(firstName))
            {
                query = query.Where(x =>
                    x.Personnel != null &&
                    x.Personnel.FirstName.Contains(firstName));
            }


            if (!string.IsNullOrWhiteSpace(lastName))
            {
                query = query.Where(x =>
                    x.Personnel != null &&
                    x.Personnel.LastName.Contains(lastName));
            }


            if (personnelId > 0)
            {
                query = query.Where(x =>
                    x.PersonnelId == personnelId);
            }


            if (contractTypeId > 0)
            {
                query = query.Where(x =>
                    x.ContractTypeId == contractTypeId);
            }


            if (statusId > 0)
            {
                query = query.Where(x =>
                    x.ContractStatusId == statusId);
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


                ("Personnel", "asc") =>
                    query.OrderBy(x =>
                        x.Personnel!.LastName)
                        .ThenBy(x =>
                        x.Personnel!.FirstName),

                ("Personnel", "desc") =>
                    query.OrderByDescending(x =>
                        x.Personnel!.LastName)
                        .ThenByDescending(x =>
                        x.Personnel!.FirstName),


                ("ContractDate", "asc") =>
                    query.OrderBy(x => x.ContractDate),

                ("ContractDate", "desc") =>
                    query.OrderByDescending(x => x.ContractDate),


                ("StartDate", "asc") =>
                    query.OrderBy(x => x.StartDate),

                ("StartDate", "desc") =>
                    query.OrderByDescending(x => x.StartDate),


                ("EndDate", "asc") =>
                    query.OrderBy(x => x.EndDate),

                ("EndDate", "desc") =>
                    query.OrderByDescending(x => x.EndDate),





                ("ContractType", "asc") =>
                    query.OrderBy(x =>
                        x.ContractType!.ContractTypeName),

                ("ContractType", "desc") =>
                    query.OrderByDescending(x =>
                        x.ContractType!.ContractTypeName),


                _ =>
                    query.OrderByDescending(x => x.Id)
            };


            // -----------------------------------------------------
            // Paging
            // -----------------------------------------------------

            var total =
                await query.CountAsync();


            var data =
                await query
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync();


            ViewBag.SortColumn = sortColumn;
            ViewBag.SortDirection = sortDirection;


            return PartialView(
                "_ContractList",

                new PagedResult<PersonnelContract>
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
        // Create - GET
        // =========================================================

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var model = new ContractCreateViewModel
            {
                IsActive = true,

                PersonnelOrder = new PersonnelOrderViewModel
                {
                    IsActive = true
                }
            };

            await PrepareContractForm(model);

          

            model.ContractDate = DateTime.Now;
            model.ContractStartDate = DateTime.Now;
            model.ContractEndDate = DateTime.Now.AddYears(1);

            // fill amount from salayitemrule
            foreach (var item in model.PersonnelOrder.Details)
            {
                int SalaryItemId = item.SalaryItemId;
                var (lastDate, lastAmount) =
                    await GetLastSalaryItemRuleAsync(SalaryItemId);
                if (lastAmount != null)
                    item.Amount = lastAmount.GetValueOrDefault();
            }

            return PartialView(
               "_ContractForm",
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

        private async Task PrepareContractForm( ContractCreateViewModel model)
        {
            // =====================================================
            // Personnel
            // =====================================================
            model.JobList = await _context.Job
                .AsNoTracking()
                .OrderBy(x => x.Title)
                .Select(x => new SelectListItem
                {
                    Value = x.Id.ToString(),
                    Text = x.Title
                })
                .ToListAsync();

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


            // =====================================================
            // Contract Type
            // =====================================================

            model.ContractTypeList =
                await _context.ContractTypes
                    .AsNoTracking()
                    .OrderBy(x => x.Id)
                    .Select(x => new SelectListItem
                    {
                        Value = x.Id.ToString(),

                        Text = x.ContractTypeName,

                        Selected =
                            x.Id == model.ContractTypeId
                    })
                    .ToListAsync();


            // =====================================================
            // Insurance Groups
            // =====================================================

            model.PersonnelOrder.RuleInsuranceGroupList =
                await _context.RuleInsuranceGroups
                    .AsNoTracking()
                    .OrderBy(x => x.Id)
                    .Select(x => new SelectListItem
                    {
                        Value = x.Id.ToString(),

                        Text = x.GroupName,

                        Selected =
                            x.Id ==
                            model.PersonnelOrder
                                .RuleInsuranceGroupId
                    })
                    .ToListAsync();


            // =====================================================
            // PersonnelOrder Salary Items
            // =====================================================

            var salaryItems =
                await _context.SalaryItems
                    .AsNoTracking()
                    .Where(x =>
                        x.Source == "PersonnelOrder")
                    .OrderBy(x => x.Priority)
                    .ToListAsync();


            // Only populate Details if they are empty.
            // This is important during validation errors.
            if (model.PersonnelOrder.Details == null ||
                model.PersonnelOrder.Details.Count == 0)
            {
                model.PersonnelOrder.Details =
                    salaryItems.Select(x =>
                        new PersonnelOrderDetailViewModel
                        {
                            SalaryItemId = x.Id,
                            SalaryItemName = x.SalaryItemName,
                            SalaryItemLabel = x.Label,
                            Unit = x.Unit,
                            Amount = 0
                        })
                        .ToList();
            }
        }

        public enum ContractStatus
        {
            [Display(Name = "فعال")]
            Active = 1,

            [Display(Name = "خاتمه یافته")]
            Terminated = 2,

            [Display(Name = "لغو شده")]
            Cancelled = 3
        }

        // =========================================================
        // Create - POST
        // =========================================================
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ContractCreateViewModel model)
        {
            // =====================================================
            // Validate
            // =====================================================
            //ModelState.Remove(nameof(PersonnelOrder.Date));
            ModelState.Remove(nameof(PersonnelContract.Personnel));
            ModelState.Remove(nameof(PersonnelContract.ContractType));
            ModelState.Remove("PersonnelOrder.Job");
            ModelState.Remove("PersonnelOrder.Contract");
            ModelState.Remove("PersonnelOrder.Personnel");

            model.PersonnelOrder.IssueDate = model.ContractDate;
            if (!ModelState.IsValid)
            {
                await PrepareContractForm(model);

                return View(model);
            }


            // =====================================================
            // Validate Personnel
            // =====================================================

            var personnelExists =
                await _context.Personnels
                    .AnyAsync(x =>
                        x.Id == model.PersonnelId);

            if (!personnelExists)
            {
                ModelState.AddModelError(
                    nameof(model.PersonnelId),
                    "پرسنل انتخاب شده وجود ندارد.");

                await PrepareContractForm(model);

                return View(model);
            }


            // =====================================================
            // Validate Contract Type
            // =====================================================

            var contractTypeExists =
                await _context.ContractTypes
                    .AnyAsync(x =>
                        x.Id == model.ContractTypeId);

            if (!contractTypeExists)
            {
                ModelState.AddModelError(
                    nameof(model.ContractTypeId),
                    "نوع قرارداد انتخاب شده وجود ندارد.");

                await PrepareContractForm(model);

                return View(model);
            }


            // =====================================================
            // Validate Insurance Group
            // =====================================================

            var insuranceGroupExists =
                await _context.RuleInsuranceGroups
                    .AnyAsync(x =>
                        x.Id ==
                        model.PersonnelOrder
                            .RuleInsuranceGroupId);

            if (!insuranceGroupExists)
            {
                ModelState.AddModelError(
                    "PersonnelOrder.RuleInsuranceGroupId",
                    "گروه بیمه انتخاب شده وجود ندارد.");

                await PrepareContractForm(model);

                return View(model);
            }


            // =====================================================
            // Transaction
            // =====================================================

            await using var transaction =
                await _context.Database
                    .BeginTransactionAsync();

            try
            {
                await _context.Contracts
                    .Where(x => x.PersonnelId == model.PersonnelId)
                    .ExecuteUpdateAsync(setters =>
                        setters.SetProperty(x => x.IsActive, false));

                // =================================================
                // 1. CONTRACT
                // =================================================


                var contract = new PersonnelContract
                {
                    PersonnelId = model.PersonnelId,
                    ContractTypeId = model.ContractTypeId,
                    ContractDate = model.ContractDate,
                    EndDate = model.ContractEndDate,
                    StartDate = model.ContractStartDate,
                    
                    IsActive = true,
                    Description = model.Description
                };
                var maxContractId = await _context.Contracts
                    .Select(x => (int?)x.Id)
                    .MaxAsync();

                contract.ContractStatusId = (int)ContractStatus.Active;
                contract.Id = (maxContractId ?? 0) + 1;
                //contract.ContractStatusId = (int)ContractStatus.Active;
                contract.IsActive = true;

                var maxOrderId = await _context.PersonnelOrders
                .Select(x => (int?)x.Id)
                .MaxAsync();
                maxOrderId = (maxOrderId ?? 0) + 1;
                contract.PersonnelOrderId = maxOrderId.GetValueOrDefault();

                _context.Contracts.Add(contract);
              
                await _context.SaveChangesAsync();


                // =================================================
                // 2. PERSONNEL ORDER
                // =================================================

                var personnelOrder =
                    new PersonnelOrder
                    {
                        ContractId = contract.Id,
                        PersonnelId = model.PersonnelId,
                        RuleInsuranceGroupId = model.PersonnelOrder.RuleInsuranceGroupId,
                        IssueDate = contract.ContractDate.GetValueOrDefault(),
                        IsActive = true,
                        JobId = model.JobId.GetValueOrDefault(),
                        DateCreated = DateTime.Now,
                        UserCreated = GetCurrentUserId(),
                        StartDate = model.ContractStartDate,
                        EndDate = model.ContractEndDate
                    };

                var maxOrderNo = await _context.PersonnelOrders
                      .Select(x => (int?)x.No)
                      .MaxAsync();
                personnelOrder.No = (maxOrderNo ?? 0) + 1;



                personnelOrder.Id = (maxOrderId ?? 0) + 1;


                _context.PersonnelOrders.Add(
                    personnelOrder);

                var maxId = await _context.PersonnelOrderDetails
                 .Select(x => (int?)x.Id)
                 .MaxAsync();
                int maxOrderDetailId = (maxId ?? 0) + 1;

                //foreach (PersonnelOrderDetailViewModel item in model.PersonnelOrder.Details)
                //{
                //    var ordDetail = new PersonnelOrderDetail
                //    {
                //        SalaryItemId = item.SalaryItemId,
                //        Amount = item.Amount,
                //    };
                //    ordDetail.PersonnelOrderId = personnelOrder.Id;
                //    ordDetail.Id = maxOrderDetailId;
                //    ordDetail.DateCreated = DateTime.Now;
                //    ordDetail.UserCreated = GetCurrentUserId();
                //    personnelOrder.Details.Add(ordDetail);
                //    maxOrderDetailId += 1;
                //}

                //await _context.SaveChangesAsync();


                // =================================================
                // 3. PERSONNEL ORDER DETAILS
                // =================================================

                var validSalaryItemIds =
                    await _context.SalaryItems
                        .Where(x =>
                            x.Source == "PersonnelOrder")
                        .Select(x => x.Id)
                        .ToListAsync();

                foreach (var detail in model.PersonnelOrder.Details)
                {
                    // Security / integrity check
                    if (!validSalaryItemIds.Contains(
                            detail.SalaryItemId))
                    {
                        continue;
                    }

                    var orderDetail =
                        new PersonnelOrderDetail
                        {
                            PersonnelOrderId = personnelOrder.Id,
                            SalaryItemId = detail.SalaryItemId,
                            Amount = detail.Amount,
                            DateCreated = DateTime.Now,
                            UserCreated = GetCurrentUserId()
                        };

                    maxOrderDetailId += 1;
                    orderDetail.Id = maxOrderDetailId;
                    _context.PersonnelOrderDetails.Add(orderDetail);
                }


                await _context.SaveChangesAsync();


                // =================================================
                // COMMIT
                // =================================================

                await transaction.CommitAsync();

                return Json(new { success = true, Message = "ثبت انجام نشد" });
            }
            catch
            {
                await transaction.RollbackAsync();

                ModelState.AddModelError(
                    "",
                    "خطایی هنگام ذخیره قرارداد و حکم پرسنلی رخ داد.");

                await PrepareContractForm(model);

                return Json(new { success = false });
            }
        }

        private string GetCurrentUserId()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(userId))
                throw new InvalidOperationException(
                    "کاربر جاری احراز هویت نشده است.");

            return userId;
        }

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create(
        //    Contract model)
        //{
        //    // Navigation properties must not participate
        //    // in validation.

        //    ModelState.Remove(nameof(Contract.Personnel));
        //    ModelState.Remove(nameof(Contract.ContractType));


        //    if (!ModelState.IsValid)
        //    {
        //        await LoadFormData();

        //        return PartialView(
        //            "_ContractForm",
        //            model);
        //    }


        //    // Prevent duplicate contract on same personnel
        //    // with same start date.

        //    var duplicate =
        //        await _context.Contracts.AnyAsync(x =>
        //            x.PersonnelId == model.PersonnelId &&
        //            x.StartDate == model.StartDate);


        //    if (duplicate)
        //    {
        //        ModelState.AddModelError(
        //            "",
        //            "برای این پرسنل قرارداد دیگری با همین تاریخ شروع وجود دارد.");

        //        await LoadFormData();

        //        return PartialView(
        //            "_ContractForm",
        //            model);
        //    }


        //    // -----------------------------------------------------
        //    // Generate Id
        //    // -----------------------------------------------------

        //    var maxId =
        //        await _context.Contracts
        //            .Select(x => (int?)x.Id)
        //            .MaxAsync();


        //    model.Id =
        //        (maxId ?? 0) + 1;


        //    _context.Contracts.Add(model);

        //    await _context.SaveChangesAsync();


        //    return Json(new
        //    {
        //        success = true
        //    });
        //}


        // =========================================================
        // Edit - GET
        // =========================================================

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            PersonnelContract contract = await _context.Contracts.FindAsync(id);

            PersonnelOrder personnelorder =
                  await _context.PersonnelOrders
                      .Where(i => i.ContractId == id)
                      .Include(i => i.Job)
                      .FirstOrDefaultAsync();

            int PersonnelOrderId = personnelorder.Id;
            List<PersonnelOrderDetail> personnelorderDetail =
                  await _context.PersonnelOrderDetails.Include(i => i.SalaryItem)
                      .Where(i => i.PersonnelOrderId == PersonnelOrderId).ToListAsync();

            if (contract == null)
                return NotFound();

            //await LoadFormData();

            ContractCreateViewModel model = new ContractCreateViewModel();

            model.PersonnelId = contract.PersonnelId;
            model.ContractId = contract.Id;

            model.ContractTypeId = contract.ContractTypeId;
            model.ContractDate = contract.ContractDate.GetValueOrDefault();
            model.ContractEndDate = contract.EndDate.GetValueOrDefault();
            model.ContractStartDate = contract.StartDate.GetValueOrDefault();

            model.Description = contract.Description;

            model.PersonnelOrder = new PersonnelOrderViewModel();
            model.JobId = personnelorder.JobId;
            model.PersonnelOrder.Id = personnelorder.Id;
            model.PersonnelOrder.PersonnelId = personnelorder.PersonnelId;
            model.PersonnelOrder.ContractId = personnelorder.ContractId;
            model.PersonnelOrder.RuleInsuranceGroupId = personnelorder.RuleInsuranceGroupId.GetValueOrDefault();
            model.PersonnelOrder.No = personnelorder.Id;
            model.PersonnelOrder.IssueDate = personnelorder.IssueDate;
            model.PersonnelOrder.StartDate = personnelorder.StartDate;
            model.PersonnelOrder.EndDate = personnelorder.EndDate;

            model.PersonnelOrder.Description = personnelorder.Description;
            model.PersonnelOrder.IsActive = personnelorder.IsActive;
            model.PersonnelOrder.JobId = personnelorder.JobId;
            model.PersonnelOrder.Job = personnelorder.Job;
            model.PersonnelOrder.Description = personnelorder.Description;

            int i = 0;
            model.PersonnelOrder.Details = new List<PersonnelOrderDetailViewModel>();
            foreach (PersonnelOrderDetail item in personnelorderDetail)
            {
                model.PersonnelOrder.Details.Add(new PersonnelOrderDetailViewModel());
                model.PersonnelOrder.Details[i].Id = item.Id;
                model.PersonnelOrder.Details[i].Amount = item.Amount;
                model.PersonnelOrder.Details[i].PersonnelOrderId = item.PersonnelOrderId;
                model.PersonnelOrder.Details[i].SalaryItemId = item.SalaryItemId;
                model.PersonnelOrder.Details[i].SalaryItemLabel = item.SalaryItem.Label;
                model.PersonnelOrder.Details[i].SalaryItemName = item.SalaryItem.SalaryItemName;


                i++;
            }

            await PrepareContractForm(model);

            return PartialView(
                "_ContractForm",
                model);
        }


        // =========================================================
        // Edit - POST
        // =========================================================

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(
            ContractCreateViewModel model)
        {
            ModelState.Remove(nameof(PersonnelContract.Personnel));
            ModelState.Remove(nameof(PersonnelContract.ContractType));
            ModelState.Remove("PersonnelOrder.Job");
            ModelState.Remove("PersonnelOrder.Contract");
            ModelState.Remove("PersonnelOrder.Personnel");
            if (!ModelState.IsValid)
            {
                await PrepareContractForm(model);

                return PartialView(
                    "_ContractForm",
                    model);
            }

            //var duplicate =
            //    await _context.Contracts.AnyAsync(x =>
            //        x.Id != model.Id &&
            //        x.PersonnelId == model.PersonnelId &&
            //        x.StartDate == model.ContractStartDate);

            //if (duplicate)
            //{
            //    ModelState.AddModelError(
            //        "",
            //        "برای این پرسنل قرارداد دیگری با همین تاریخ شروع وجود دارد.");

            //    await PrepareContractForm(model);

            //    return PartialView(
            //        "_ContractForm",
            //        model);
            //}

            await using var transaction =
                await _context.Database
                    .BeginTransactionAsync();

            try
            {
                PersonnelContract contract =
                               await _context.Contracts
                                   .FindAsync(model.ContractId);

                if (contract == null)
                    return NotFound();

                contract.PersonnelId =   model.PersonnelId;
 
                contract.ContractTypeId = model.ContractTypeId;

                contract.ContractDate = model.ContractDate;
                contract.StartDate =   model.ContractStartDate;
                contract.EndDate =  model.ContractEndDate;

                contract.Description =   model.Description;

                contract.IsActive = true;

                await _context.SaveChangesAsync();

                PersonnelOrder personnelOrder =
                  await _context.PersonnelOrders.Where(i => i.ContractId == contract.Id).FirstOrDefaultAsync();

                if (personnelOrder == null)
                    return NotFound();

                personnelOrder.RuleInsuranceGroupId = model.PersonnelOrder.RuleInsuranceGroupId;
                personnelOrder.IsActive = true;
                await _context.SaveChangesAsync();

                foreach (PersonnelOrderDetailViewModel detail in model.PersonnelOrder.Details)
                {
                    PersonnelOrderDetail personnelOrderDetail =
                        await _context.PersonnelOrderDetails.Where(i => i.Id == detail.Id).FirstOrDefaultAsync();


                    personnelOrderDetail.Amount = detail.Amount;
                }
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

                ModelState.AddModelError(
                    "",
                    "خطایی هنگام ویرایش قرارداد و حکم پرسنلی رخ داد.");

                await PrepareContractForm(model);

                return Json(new { success = false });
            }

        }


        // =========================================================
        // Delete
        // =========================================================

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            PersonnelContract item =
                await _context.Contracts
                    .FindAsync(id);

            List<PersonnelOrder> ordList =
                await _context.PersonnelOrders.Where(i => i.ContractId == item.Id).ToListAsync();

            if (item == null)
                return NotFound();

            await using var transaction =
                await _context.Database
                    .BeginTransactionAsync();

            try
            {
                foreach (PersonnelOrder ord in ordList)
                {
                    await _context.PersonnelOrderDetails
                                        .Where(x => x.PersonnelOrderId == ord.Id)
                                        .ExecuteDeleteAsync();
                    _context.PersonnelOrders.Remove(ord);
                }

                _context.Contracts.Remove(item);

                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                return Json(new
                {
                    success = true
                });
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();

                ModelState.AddModelError(
                    "",
                    "خطایی هنگام حذف قرارداد .");

                return Json(new { success = false });
            }


        }


        // =========================================================
        // Form dropdowns
        // =========================================================

        //private async Task LoadFormData()
        //{
        //    ViewBag.Personnels =
        //        await GetPersonnels();

        //    ViewBag.ContractTypes =
        //        await GetContractTypes();

        //    ViewBag.ContractStatuses =
        //        await GetContractStatuses();

        //    ViewBag.ContractStatuses =
        //        await GetContractStatuses();

        //}


        private async Task<List<SelectListItem>>
            GetPersonnels()
        {
            return await _context.Personnels
                .OrderBy(x => x.LastName)
                .ThenBy(x => x.FirstName)
                .Select(x => new SelectListItem
                {
                    Value = x.Id.ToString(),

                    Text = x.LastName + " " + x.FirstName
                })
                .ToListAsync();
        }


        private async Task<List<SelectListItem>>
            GetContractTypes()
        {
            return await _context.ContractTypes
                .OrderBy(x => x.Id)
                .Select(x => new SelectListItem
                {
                    Value = x.Id.ToString(),

                    Text = x.ContractTypeName
                })
                .ToListAsync();
        }

        private async Task<List<SelectListItem>> GetContractStatuses()
        {
            return await _context.ContractStatuses
                .OrderBy(x => x.Id)
                .Select(x => new SelectListItem
                {
                    Value = x.Id.ToString(),
                    Text = x.ContractStatusName
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

    }
}