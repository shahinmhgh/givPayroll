using DocumentFormat.OpenXml.Bibliography;
using DocumentFormat.OpenXml.Drawing.Charts;
using givPayroll.Data;
using givPayroll.Models;
using givPayroll.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using Microsoft.Playwright;
using NCalc;

namespace givPayroll.Controllers;

public class PayrollController : Controller
{
    private readonly IPdfService _pdfService;
    private readonly ApplicationDbContext _context;

    public PayrollController(IPdfService pdfService, ApplicationDbContext context)
    {
        _pdfService = pdfService;
        _context = context;
    }

    // Personnel
    //   │
    //   ├── Contract
    //   │
    //   ├── PersonnelFamily
    //   │
    //   └── PersonnelOrder
    //             │
    //             └── PersonnelOrderItem
    //                        │
    //                        ▼
    //                    SalaryItem
    //                        │
    //                        ▼
    //                 SalaryItemRule
    //
    // PayrollPeriod
    //      │
    //      ├── PersonnelMonthly
    //      │
    //      ├── Attendance
    //      │
    //      └── Payroll
    //             │
    //             └── PayrollItem
    //                    │
    //                    └── SalaryItem

    public static int GetPersianMonthDays(int month)
    {
        if (month >= 1 && month <= 6)
            return 31;

        if (month >= 7 && month <= 11)
            return 30;

        if (month == 12)
            return 29;

        throw new ArgumentOutOfRangeException(nameof(month));
    }

    public async Task<IActionResult> PayrollPreview(int year, int month, int personnelId)
    {
        string prefix = $"{year:0000}/{month:00}/";
        // base on salaryitem 
        //  and personnelOrder
        //   and payrolladjustment

        //there are four sources: 
        // PersonnelOrder
        // Attendence
        // PayrollAdjustmentDetail
        // RuleInsurance - RuleTax

        //1 find active personnellorder - detail 
        PayrollViewModel model = new PayrollViewModel();
        model.PersonnelId = personnelId;
        model.PayrollYear = year;
        model.PayrollMonth = month;
        model.Personnel = _context.Personnels.Where(i => i.Id == personnelId).FirstOrDefault();

        #region PersonnelOrder
        //List<SalaryItem> lst1 = _context.SalaryItems.Where(i => i.Source == "PersonnelOrder").ToList();
        PersonnelOrder activeOrderItem = _context.PersonnelOrders
            .Include(x => x.Details)
            .ThenInclude(x => x.SalaryItem)
            .Where(i => i.PersonnelId == personnelId && i.IsActive).FirstOrDefault();

        // formula items
        decimal BaseSalary = 0;
        int monthDays = GetPersianMonthDays(month);

        foreach (PersonnelOrderDetail pOrderItem in activeOrderItem.Details)
        {
            PayrollItem item = new PayrollItem();
            item.SalaryItemId = pOrderItem.SalaryItemId;
            if (pOrderItem.SalaryItem.Label == "BaseSalary")
                BaseSalary = pOrderItem.Amount;

            item.Amount = pOrderItem.Amount;
            model.PayrollItems.Add(item);
        }
        #endregion

        #region Attendence
        var totals = await _context.Attendances
            .Where(x => x.PersonnelId == personnelId &&
                        x.AttendancePersianDate.StartsWith(prefix))
            .GroupBy(x => x.PersonnelId)
            .Select(g => new AttendanceTotalsViewModel
            {
                WorkingMinute = g.Sum(x => x.WorkingMinute),
                ExtraMinute = g.Sum(x => x.ExtraMinute),
                HolidayMinute = g.Sum(x => x.HolidayMinute),
                LeaveNormalMinute = g.Sum(x => x.LeaveNormalMinute),
                LeaveWithoutSalaryMinute = g.Sum(x => x.LeaveWithoutSalaryMinute),
                LeaveSickMinute = g.Sum(x => x.LeaveSickMinute),
                AbsenceMinute = g.Sum(x => x.AbsenceMinute),
                MissionMinute = g.Sum(x => x.MissionMinute)
            })
            .FirstOrDefaultAsync();

        List<SalaryItem> lst2 = _context.SalaryItems.Where(i => i.Source == "Attendence").ToList();
        foreach (SalaryItem itemSalary in lst2)
        {
            string Label = itemSalary.Label;
            string formula = itemSalary.FormulaValue;
            int total = 0;
            decimal amount = 0;
            switch (Label)
            {
                case "OverTime":
                    total = totals?.ExtraMinute ?? 0;

                    var expression1 = new Expression(formula);
                    expression1.Parameters["BaseSalary"] = BaseSalary;
                    expression1.Parameters["MonthDays"] = monthDays;
                    expression1.Parameters["ExtraMinute"] = total;
                    var result1 = expression1.Evaluate();
                    amount = Convert.ToDecimal(result1);

                    break;
                case "MissionAllowance":
                    total = totals?.MissionMinute ?? 0;

                    var expression2 = new Expression(formula);
                    expression2.Parameters["BaseSalary"] = BaseSalary;
                    expression2.Parameters["MonthDays"] = monthDays;
                    expression2.Parameters["MissionMinute"] = total;
                    var result2 = expression2.Evaluate();
                    amount = Convert.ToDecimal(result2);

                    break;
                case "AbsenceDeduction":
                    total = totals?.AbsenceMinute ?? 0;

                    var expression3 = new Expression(formula);
                    expression3.Parameters["BaseSalary"] = BaseSalary;
                    expression3.Parameters["MonthDays"] = monthDays;
                    expression3.Parameters["AbsenceMinute"] = total;
                    var result3 = expression3.Evaluate();
                    amount = Convert.ToDecimal(result3);

                    break;
                case "DelayDeduction":
                    total = totals?.HolidayMinute ?? 0;

                    var expression4 = new Expression(formula);
                    expression4.Parameters["BaseSalary"] = BaseSalary;
                    expression4.Parameters["MonthDays"] = monthDays;
                    expression4.Parameters["DelayMinute"] = total;
                    var result4 = expression4.Evaluate();
                    amount = Convert.ToDecimal(result4);

                    break;

                default:
                    break;
            }

            PayrollItem item = new PayrollItem();
            item.SalaryItemId = itemSalary.Id;
            item.PlusMinus = itemSalary.PlusMinus;
            item.Amount = amount;
            if (amount > 0)
                model.PayrollItems.Add(item);
        }
        #endregion

        ////PayrollAdjustment
        List<SalaryItem> lst3 = _context.SalaryItems.Where(i => i.Source == "PayrollAdjustment").ToList();
        foreach (SalaryItem itemSalary in lst3)
        {
            String Label = itemSalary.Label;

            var result = await _context.PayrollAdjustmentDetails
                .Where(d =>
                    d.PayrollPersianYear == year &&
                    d.PayrollPersianMonth == month &&
                    d.PayrollAdjustment.SalaryItem.Label == Label &&
                    _context.PayrollAdjustmentPersonnels.Any(ap =>
                        ap.PayrollAdjustmentId == d.PayrollAdjustmentId &&
                        ap.PersonnelId == personnelId))
                .Select(d => new
                {
                    Amount = d.Amount,
                    Description = d.PayrollAdjustment.Description
                })
                .FirstOrDefaultAsync();

            if (result != null)
            {
                PayrollItem item = new PayrollItem();
                item.SalaryItemId = itemSalary.Id;
                item.Amount = Convert.ToDecimal(result.Amount);
                item.Description = result.Description;
                model.PayrollItems.Add(item);
            }

        }
        Personnel person = _context.Personnels.Where(i => i.Id == personnelId).FirstOrDefault();
        string PersonnelName = person.FirstName + " " + person.LastName;
        ViewBag.PersonnelYearMonthName = DateUtil.GetPersianMonthName(month) + " " + year + " " + PersonnelName;

        ////RuleInsurance
        //List<SalaryItem> lst4 = _context.SalaryItems.Where(i => i.Label == "RuleInsurance").ToList();
        //foreach (SalaryItem itemSalary in lst4)
        //{
        //    PayrollItem item = new PayrollItem();
        //    item.SalaryItemId = itemSalary.Id;
        //    item.Amount = 1;
        //    model.PayrollItems.Add(item);
        //}
        ////RuleTax
        //List<SalaryItem> lst5 = _context.SalaryItems.Where(i => i.Label == "RuleTax").ToList();
        //foreach (SalaryItem itemSalary in lst5)
        //{
        //    PayrollItem item = new PayrollItem();
        //    item.SalaryItemId = itemSalary.Id;
        //    item.Amount = 1;
        //    model.PayrollItems.Add(item);
        //}

        foreach (PayrollItem item in model.PayrollItems)
            item.SalaryItem = _context.SalaryItems.Where(i => i.Id == item.SalaryItemId).FirstOrDefault();

        return PartialView("_PayrollPreview", model);
    }

    public IActionResult Salary(int id)
    {
        Personnel person = _context.Personnels.Where(i => i.Id == id).FirstOrDefault();

        var model = new SalaryReportViewModel
        {
            PersonnelId = id,

            PersonnelCode = "1001",

            FirstName = person.FirstName,

            LastName = person.LastName,

            NationalCode = "1234567890",

            PayrollMonth = "مرداد ۱۴۰۵",

            BasicSalary = 150000000,

            HousingAllowance = 9000000,

            FoodAllowance = 14000000,

            Overtime = 12000000,

            Deductions = 18000000
        };

        return View(model);
    }

    [HttpGet]
    public async Task<IActionResult> SalaryPdf(
        int id,
        CancellationToken cancellationToken)
    {

        var url =
            Url.Action(
                nameof(Salary),
                "Payroll",
                new { id },
                Request.Scheme)!;

        var pdf = await _pdfService.GeneratePdfFromUrlAsync(
            url,
            cancellationToken);

        return File(
            pdf,
            "application/pdf",
            $"Salary-{id}.pdf");
    }
}