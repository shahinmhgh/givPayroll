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
    public class PersonnelFamilyController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PersonnelFamilyController(ApplicationDbContext context)
        {
            _context = context;
        }

        // ============================================================
        // INDEX
        // ============================================================

        [HttpGet]
        public async Task<IActionResult> Index(int personnelId)
        {
            var personnel = await _context.Personnels
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == personnelId);

            if (personnel == null)
                return NotFound();

            ViewBag.Personnel = personnel;

            return View(personnelId);
        }


        // ============================================================
        // LIST
        // ============================================================

        [HttpGet]
        public async Task<IActionResult> List(
            int personnelId,
            string firstName = "",
            string lastName = "",
            int page = 1,
            int pageSize = 10,
            string sortColumn = "LastName",
            string sortDirection = "asc")
        {
            var personnel = await _context.Personnels
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == personnelId);

            if (personnel == null)
                return NotFound();

            var query = _context.PersonnelFamilies
                .AsNoTracking()
                .Where(x => x.PersonnelId == personnelId);


            // Search
            if (!string.IsNullOrWhiteSpace(firstName))
            {
                query = query.Where(x =>
                    x.FirstName.Contains(firstName));
            }

            if (!string.IsNullOrWhiteSpace(lastName))
            {
                query = query.Where(x =>
                    x.LastName.Contains(lastName));
            }


            // Sorting
            query = (sortColumn, sortDirection) switch
            {
                ("Id", "asc") =>
                    query.OrderBy(x => x.Id),

                ("Id", "desc") =>
                    query.OrderByDescending(x => x.Id),

                ("FirstName", "asc") =>
                    query.OrderBy(x => x.FirstName),

                ("FirstName", "desc") =>
                    query.OrderByDescending(x => x.FirstName),

                ("LastName", "asc") =>
                    query.OrderBy(x => x.LastName),

                ("LastName", "desc") =>
                    query.OrderByDescending(x => x.LastName),

                ("FamilyRelation", "asc") =>
                    query.OrderBy(x =>
                        x.FamilyRelation.FamilyRelationName),

                ("FamilyRelation", "desc") =>
                    query.OrderByDescending(x =>
                        x.FamilyRelation.FamilyRelationName),

                _ => query.OrderBy(x => x.Id)
            };


            var total = await query.CountAsync();


            var data = await query
                .Include(x => x.FamilyRelation)
                .Include(x => x.Gender)
                .Include(x => x.MaritalStatus)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();


            ViewBag.SortColumn = sortColumn;
            ViewBag.SortDirection = sortDirection;
            ViewBag.Personnel = personnel;


            return PartialView(
                "_PersonnelFamilyList",
                new PagedResult<PersonnelFamily>
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
        public async Task<IActionResult> Create(int personnelId)
        {
            var personnel = await _context.Personnels
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == personnelId);

            if (personnel == null)
                return NotFound();


            await LoadFormData();


            var model = new PersonnelFamily
            {
                PersonnelId = personnelId,
                DateCreated = DateTime.Now
            };
            model.UserCreated = GetCurrentUserId();

            ViewBag.Personnel = personnel;

            return PartialView(
                "_PersonnelFamilyForm",
                model);
        }

        private string GetCurrentUserId()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(userId))
                throw new InvalidOperationException(
                    "کاربر جاری احراز هویت نشده است.");

            return userId;
        }
        // ============================================================
        // CREATE - POST
        // ============================================================

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PersonnelFamily model)
        {
            // Navigation properties should not participate
            // in model validation.
            ModelState.Remove(nameof(PersonnelFamily.Personnel));
            ModelState.Remove(nameof(PersonnelFamily.FamilyRelation));
            ModelState.Remove(nameof(PersonnelFamily.Gender));
            ModelState.Remove(nameof(PersonnelFamily.MaritalStatus));


            var personnelExists =
                await _context.Personnels
                    .AnyAsync(x => x.Id == model.PersonnelId);

            if (!personnelExists)
                return NotFound();

            
            if (!ModelState.IsValid)
            {
                await LoadFormData();

                var personnel =
                    await _context.Personnels
                        .AsNoTracking()
                        .FirstOrDefaultAsync(
                            x => x.Id == model.PersonnelId);

                ViewBag.Personnel = personnel;

                return PartialView(
                    "_PersonnelFamilyForm",
                    model);
            }


            model.DateCreated = DateTime.Now;

            var maxId = await _context.PersonnelFamilies
              .Select(x => (int?)x.Id)
              .MaxAsync();

            model.Id = (maxId ?? 0) + 1;

            _context.PersonnelFamilies.Add(model);

            await _context.SaveChangesAsync();


            return Json(new
            {
                success = true
            });
        }


        // ============================================================
        // EDIT - GET
        // ============================================================

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var model =
                await _context.PersonnelFamilies
                    .AsNoTracking()
                    .FirstOrDefaultAsync(x => x.Id == id);

            if (model == null)
                return NotFound();


            await LoadFormData();


            var personnel =
                await _context.Personnels
                    .AsNoTracking()
                    .FirstOrDefaultAsync(
                        x => x.Id == model.PersonnelId);


            ViewBag.Personnel = personnel;


            return PartialView(
                "_PersonnelFamilyForm",
                model);
        }


        // ============================================================
        // EDIT - POST
        // ============================================================

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(PersonnelFamily model)
        {
            ModelState.Remove(nameof(PersonnelFamily.Personnel));
            ModelState.Remove(nameof(PersonnelFamily.FamilyRelation));
            ModelState.Remove(nameof(PersonnelFamily.Gender));
            ModelState.Remove(nameof(PersonnelFamily.MaritalStatus));


            if (!ModelState.IsValid)
            {
                await LoadFormData();

                var personnel =
                    await _context.Personnels
                        .AsNoTracking()
                        .FirstOrDefaultAsync(
                            x => x.Id == model.PersonnelId);

                ViewBag.Personnel = personnel;

                return PartialView(
                    "_PersonnelFamilyForm",
                    model);
            }


            var existing =
                await _context.PersonnelFamilies
                    .FirstOrDefaultAsync(x => x.Id == model.Id);

            if (existing == null)
                return NotFound();


            // Update only editable properties
            existing.PersonnelRelationId =
                model.PersonnelRelationId;

            existing.FirstName =
                model.FirstName;

            existing.LastName =
                model.LastName;

            existing.NationalCode =
                model.NationalCode;

            existing.BirthDate =
                model.BirthDate;

            existing.GenderID =
                model.GenderID;

            existing.IdNo =
                model.IdNo;

            existing.IssuePlace =
                model.IssuePlace;

            existing.MaritalStatusId =
                model.MaritalStatusId;

            existing.DateChanged =
                DateTime.Now;


            await _context.SaveChangesAsync();


            return Json(new
            {
                success = true
            });
        }


        // ============================================================
        // DELETE
        // ============================================================

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var item =
                await _context.PersonnelFamilies
                    .FirstOrDefaultAsync(x => x.Id == id);

            if (item == null)
                return NotFound();


            _context.PersonnelFamilies.Remove(item);

            await _context.SaveChangesAsync();


            return Json(new
            {
                success = true
            });
        }


        // ============================================================
        // LOOKUP DATA
        // ============================================================

        private async Task LoadFormData()
        {
            ViewBag.FamilyRelation =
                await _context.FamilyRelations
                    .OrderBy(x => x.Id)
                    .Select(x => new SelectListItem
                    {
                        Value = x.Id.ToString(),
                        Text = x.FamilyRelationName
                    })
                    .ToListAsync();


            ViewBag.Gender =
                await _context.Genders
                    .OrderBy(x => x.Id)
                    .Select(x => new SelectListItem
                    {
                        Value = x.Id.ToString(),
                        Text = x.GenderName
                    })
                    .ToListAsync();


            ViewBag.MarriageStatus =
                await _context.MarriageStatus
                    .OrderBy(x => x.Id)
                    .Select(x => new SelectListItem
                    {
                        Value = x.Id.ToString(),
                        Text = x.MaritalStatusName
                    })
                    .ToListAsync();
        }
    }
}