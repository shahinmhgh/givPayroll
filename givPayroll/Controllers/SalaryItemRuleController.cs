using givPayroll.Data;
using givPayroll.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace WebApplicationPayroll.Controllers
{
    [Authorize]
    public class SalaryItemRuleController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SalaryItemRuleController(
            ApplicationDbContext context)
        {
            _context = context;
        }


        // =========================================================
        // GET RULE
        // =========================================================

        [HttpGet]
        public async Task<IActionResult> Edit(int salaryItemId)
        {
            var salaryItem = await _context.SalaryItems
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == salaryItemId);

            if (salaryItem == null)
                return NotFound();


            var rule = await _context.SalaryItemRules
                .FirstOrDefaultAsync(
                    x => x.SalaryItemId == salaryItemId);


            // -----------------------------------------------------
            // If rule doesn't exist, create an empty model
            // -----------------------------------------------------

            if (rule == null)
            {
                rule = new SalaryItemRule
                {
                    SalaryItemId = salaryItemId,
                    Amount =0,
                    EffectiveDate = DateTime.Now.Date ,

                    IsTaxBase = false,

                    IsInsuranceBase = false
                };
            }


            ViewBag.SalaryItemName =
                salaryItem.SalaryItemName;

            ViewBag.SalaryItemId =
                salaryItem.Id;


            return PartialView(
                "_SalaryItemRuleForm",
                rule);
        }


        // =========================================================
        // SAVE RULE
        // =========================================================

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Save(
            SalaryItemRule model)
        {
            ModelState.Remove(
                nameof(SalaryItemRule.SalaryItem));

            
            if (!ModelState.IsValid)
            {
                var salaryItem =
                    await _context.SalaryItems
                        .AsNoTracking()
                        .FirstOrDefaultAsync(
                            x => x.Id == model.SalaryItemId);


                if (salaryItem == null)
                    return NotFound();


                ViewBag.SalaryItemName =
                    salaryItem.SalaryItemName;

                ViewBag.SalaryItemId =
                    salaryItem.Id;


                return PartialView(
                    "_SalaryItemRuleForm",
                    model);
            }


            // -----------------------------------------------------
            // Find existing rule
            // -----------------------------------------------------

            var existing =
                await _context.SalaryItemRules
                    .FirstOrDefaultAsync(
                        x =>
                            x.SalaryItemId ==
                            model.SalaryItemId);


            if (existing == null)
            {
                // -------------------------------------------------
                // New rule
                // -------------------------------------------------

                var maxId =
                    await _context.SalaryItemRules
                        .Select(x => (int?)x.Id)
                        .MaxAsync();


                model.Id =
                    (maxId ?? 0) + 1;


                _context.SalaryItemRules.Add(model);
            }
            else
            {
                // -------------------------------------------------
                // Update existing rule
                // -------------------------------------------------

                existing.EffectiveDate =
                    model.EffectiveDate;

                existing.IsTaxBase =
                    model.IsTaxBase;

                existing.Amount =
               model.Amount;
                existing.IsInsuranceBase =
                    model.IsInsuranceBase;
            }


            await _context.SaveChangesAsync();


            return Json(new
            {
                success = true
            });
        }
    }
}