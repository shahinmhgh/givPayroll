using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using givPayroll.Models;

namespace givPayroll.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.EntityFrameworkCore;
    using givPayroll.Data;

    [Authorize]
    public class PersonnelController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PersonnelController(ApplicationDbContext context)
        {
            _context = context;
        }


        public IActionResult Index()
        {
            return View();
        }


   
        [HttpGet]
        public async Task<IActionResult> List(
            string firstName = "",
            string lastName = "",
            int page = 1,
            int pageSize = 10,
            string sortColumn = "LastName",
            string sortDirection = "asc")
        {
            var query = _context.Personnels.AsQueryable();


            if (!string.IsNullOrEmpty(firstName))
                query = query.Where(x => x.FirstName.Contains(firstName));


            if (!string.IsNullOrEmpty(lastName))
                query = query.Where(x => x.LastName.Contains(lastName));


            query = (sortColumn, sortDirection) switch
            {
                ("FirstName", "asc") =>
                    query.OrderBy(x => x.FirstName),

                ("FirstName", "desc") =>
                    query.OrderByDescending(x => x.FirstName),

                ("LastName", "asc") =>
                    query.OrderBy(x => x.LastName),

                ("LastName", "desc") =>
                    query.OrderByDescending(x => x.LastName),

                ("Id", "asc") =>
                    query.OrderBy(x => x.Id),

                ("Id", "desc") =>
                    query.OrderByDescending(x => x.Id),


                ("BirthDate", "asc") =>
                    query.OrderBy(x => x.BirthDate),

                ("BirthDate", "desc") =>
                    query.OrderByDescending(x => x.BirthDate),

                //("MaritalStatus", "asc") =>
                //    query.OrderBy(x => x.MaritalStatus.MaritalStatusName),

                //("MaritalStatus", "desc") =>
                //    query.OrderByDescending(x => x.MaritalStatus.MaritalStatusName),

                //("ContractFinished", "asc") =>
                //    query.OrderBy(x => x.ContractFinished),

                //("ContractFinished", "desc") =>
                //    query.OrderByDescending(x => x.ContractFinished),

                //("BirthDate", "asc") =>
                //    query.OrderBy(x => x.BirthDate),

                //("BirthDate", "desc") =>
                //    query.OrderByDescending(x => x.BirthDate),

                _ => query.OrderBy(x => x.Id)
            };


            var total = await query.CountAsync();


            var data = await query
                .Include(i => i.MaritalStatus)
                .Include(i => i.Education)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            ViewBag.SortColumn = sortColumn;
            ViewBag.SortDirection = sortDirection;

            return PartialView("_PersonnelList",
                new PagedResult<Personnel>
                {
                    Items = data,
                    PageNumber = page,
                    PageSize = pageSize,
                    PageSizes = new List<SelectListItem>
                        {
                            new SelectListItem("5","5"),
                            new SelectListItem("10","10"),
                            new SelectListItem("20","20"),
                            new SelectListItem("50","50")
                        },
                    TotalRecords = total,
                    TotalPages = (int)Math.Ceiling(total / (double)pageSize)
                });
        }



        [HttpGet]
        public async Task<IActionResult> Create()
        {
            ViewBag.MaritalStatus = await GetMaritalStatus();
            ViewBag.Education = await GetEducation();
            return PartialView("_PersonnelForm",
                new Personnel());
        }

        //Personnel
        //│
        //├── PersonnelOrder
        //│       └── PersonnelOrderItem
        //│                └── SalaryItem
        //│
        //└── MonthlyWorkRecord
        //          │
        //          ▼
        //      Payroll
        //          └── PayrollItem
        //                   └── SalaryItem

        [HttpPost]
        public async Task<IActionResult> Create(Personnel model)
        {
            ModelState.Remove(nameof(Personnel.MaritalStatus));
            ModelState.Remove(nameof(Personnel.Education));
            ModelState.Remove(nameof(Personnel.Gender));
            ModelState.Remove(nameof(Personnel.PersonnelFamilies));

            if (!ModelState.IsValid)
            {
                ViewBag.MaritalStatus = await GetMaritalStatus();
                ViewBag.Education = await GetEducation();
                return PartialView("_PersonnelForm", model);
            }
            var maxId = await _context.Personnels
               .Select(x => (int?)x.Id)
               .MaxAsync();

            model.Id = (maxId ?? 0) + 1;

            _context.Personnels.Add(model);

            await _context.SaveChangesAsync();

            return Json(new { success = true, message="اطلاعات پرسنل با موفقیت ثبت شد" });
        }


        private async Task<List<SelectListItem>> GetMaritalStatus()
        {
            return await _context.MaritalStatus
                .OrderBy(x => x.Id)
                .Select(x => new SelectListItem
                {
                    Value = x.Id.ToString(),
                    Text = x.MaritalStatusName
                })
                .ToListAsync();
        }

        private async Task<List<SelectListItem>> GetEducation()
        {
            return await _context.Educations
                .OrderBy(x => x.Id)
                .Select(x => new SelectListItem
                {
                    Value = x.Id.ToString(),
                    Text = x.EducationName
                })
                .ToListAsync();
        }


        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            ViewBag.MaritalStatus = await GetMaritalStatus();
            ViewBag.Education = await GetEducation();
            var item = await _context.Personnels
                .FindAsync(id);

            if (item == null) return NotFound();

            // ViewBag.MaritalStatus = await GetMaritalStatus();
            return PartialView("_PersonnelForm", item);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Personnel model)
        {
            ModelState.Remove(nameof(Personnel.MaritalStatus));
            ModelState.Remove(nameof(Personnel.Education));
            ModelState.Remove(nameof(Personnel.Gender));
            ModelState.Remove(nameof(Personnel.PersonnelFamilies));

            if (!ModelState.IsValid)
            {
                var errors = ModelState
               .Where(x => x.Value?.Errors.Count > 0)
               .SelectMany(x => x.Value!.Errors.Select(e => new
               {
                   Field = x.Key,
                   Error = e.ErrorMessage
               }))
               .ToList();

                //  ViewBag.MaritalStatus = await GetMaritalStatus();
                return PartialView("_PersonnelForm", model);
            }

            _context.Personnels.Update(model);

            await _context.SaveChangesAsync();

            return Json(new { success = true });
        }


        [HttpGet]
        public async Task<IActionResult> PersonnelFamily(int id)
        {
            ViewBag.MaritalStatus = await GetMaritalStatus();
            ViewBag.Education = await GetEducation();
            var item = await _context.Personnels
                .FindAsync(id);

            if (item == null) return NotFound();

            // ViewBag.MaritalStatus = await GetMaritalStatus();
            return PartialView("_PersonnelFamily", item);
        }

        [HttpPost]
        public async Task<IActionResult> PersonnelFamily(Personnel model)
        {
            ModelState.Remove(nameof(Personnel.MaritalStatus));
            ModelState.Remove(nameof(Personnel.Education));
            if (!ModelState.IsValid)
            {
                var errors = ModelState
               .Where(x => x.Value?.Errors.Count > 0)
               .SelectMany(x => x.Value!.Errors.Select(e => new
               {
                   Field = x.Key,
                   Error = e.ErrorMessage
               }))
               .ToList();

                //  ViewBag.MaritalStatus = await GetMaritalStatus();
                return PartialView("_PersonnelFamily", model);
            }

            _context.Personnels.Update(model);

            await _context.SaveChangesAsync();

            return Json(new { success = true });
        }
        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var item = await _context.Personnels
                .FindAsync(id);

            if (item == null)
                return NotFound();


            _context.Personnels.Remove(item);

            await _context.SaveChangesAsync();


            return Json(new { success = true });
        }
    }
}
