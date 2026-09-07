using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using givPayroll.Data;
using givPayroll.Models;

namespace givPayroll.Controllers
{
    [Authorize]
    public class CompanyController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CompanyController(ApplicationDbContext context)
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
            string name = "",
            string insuranceCode = "",
            int page = 1,
            int pageSize = 10,
            string sortColumn = "Name",
            string sortDirection = "asc")
        {
            var query = _context.Companies.AsQueryable();


            // -----------------------------------------------------
            // Search
            // -----------------------------------------------------

            if (!string.IsNullOrWhiteSpace(name))
            {
                query = query.Where(x =>
                    x.CompanyName.Contains(name));
            }


            if (!string.IsNullOrWhiteSpace(insuranceCode))
            {
                query = query.Where(x =>
                    x.InsuranceCode.Contains(insuranceCode));
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

                ("Name", "asc") =>
                    query.OrderBy(x => x.CompanyName),

                ("Name", "desc") =>
                    query.OrderByDescending(x => x.CompanyName),

                ("InsuranceCode", "asc") =>
                    query.OrderBy(x => x.InsuranceCode),

                ("InsuranceCode", "desc") =>
                    query.OrderByDescending(x => x.InsuranceCode),

                ("Address", "asc") =>
                    query.OrderBy(x => x.Address),

                ("Address", "desc") =>
                    query.OrderByDescending(x => x.Address),

                _ =>
                    query.OrderBy(x => x.CompanyName)
            };


            // -----------------------------------------------------
            // Total records
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
                "_CompanyList",
                new PagedResult<Company>
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
                "_CompanyForm",
                new Company());
        }


        // =========================================================
        // CREATE - POST
        // =========================================================

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Company model)
        {
            if (!ModelState.IsValid)
            {
                return PartialView(
                    "_CompanyForm",
                    model);
            }


            // -----------------------------------------------------
            // Id is NOT Identity
            // -----------------------------------------------------

            var maxId = await _context.Companies
                .Select(x => (int?)x.Id)
                .MaxAsync();

            model.Id = (maxId ?? 0) + 1;


            _context.Companies.Add(model);

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
            var item = await _context.Companies
                .FindAsync(id);


            if (item == null)
                return NotFound();


            return PartialView(
                "_CompanyForm",
                item);
        }


        // =========================================================
        // EDIT - POST
        // =========================================================

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Company model)
        {
            if (!ModelState.IsValid)
            {
                return PartialView(
                    "_CompanyForm",
                    model);
            }


            var existing = await _context.Companies
                .FindAsync(model.Id);


            if (existing == null)
                return NotFound();


            existing.CompanyName = model.CompanyName;
            existing.InsuranceCode = model.InsuranceCode;
            existing.Address = model.Address;


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
            var item = await _context.Companies
                .FindAsync(id);


            if (item == null)
                return NotFound();


            _context.Companies.Remove(item);

            await _context.SaveChangesAsync();


            return Json(new
            {
                success = true
            });
        }
    }
}