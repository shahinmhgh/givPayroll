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
using NPOI.Util.Optional;

namespace givPayroll.Controllers;

public class PayrollController : Controller
{
    private readonly IPdfService _pdfService;
    private readonly ApplicationDbContext _context;
    private readonly IPayrollFormulaService _payrollFormulaService;

    public PayrollController(IPdfService pdfService,
        ApplicationDbContext context,
        IPayrollFormulaService payrollFormulaService)
    {
        _pdfService = pdfService;
        _context = context;
        _payrollFormulaService = payrollFormulaService;
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
        PayrollViewModel model = await GetPayrollData(year, month, personnelId);

        return PartialView("_PayrollPreview", model);
    }
    public async Task<IActionResult> PayrollInsuranceTaxPreview(int insuranceTax)
    {
        List<InsuTaxViewModel> model = new List<InsuTaxViewModel>();
        if (insuranceTax == 1)
        {
            ViewBag.Title = "بیمه";
            model = lstInsurance;
        }
        if (insuranceTax == 2)
        {
            ViewBag.Title = "مالیات";
            model = lstTax;
        }
        ViewBag.insuranceTax = insuranceTax;
        for (int i = 0; i < model.Count; i++)
            model[i].SalaryItem = await _context.SalaryItems.FindAsync(   model[i].SalaryItemId);
        return PartialView("_PayrollInsuranceTax", model);
    }

   
    public static List<InsuTaxViewModel> lstInsurance = new List<InsuTaxViewModel>();
    public static List<InsuTaxViewModel> lstTax = new List<InsuTaxViewModel>();

    private async Task<PayrollViewModel> GetPayrollData(int year, int month, int personnelId)
    {

        lstInsurance = new List<InsuTaxViewModel>();
        lstTax = new List<InsuTaxViewModel>();

        string prefix = $"{year:0000}/{month:00}/";

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
        decimal JobAllowance = 0;
        decimal ResponsibilityAllowance = 0;
        decimal SeniorityAllowance = 0;
        int monthDays = GetPersianMonthDays(month);

        foreach (PersonnelOrderDetail pOrderItem in activeOrderItem.Details)
        {
            PayrollItem item = new PayrollItem();
            item.SalaryItemId = pOrderItem.SalaryItemId;
            if (pOrderItem.SalaryItem.Label == "BaseSalary")
                BaseSalary = pOrderItem.Amount;
            if (pOrderItem.SalaryItem.Label == "JobAllowance")
                JobAllowance = pOrderItem.Amount;
            if (pOrderItem.SalaryItem.Label == "ResponsibilityAllowance")
                ResponsibilityAllowance = pOrderItem.Amount;
            //if (pOrderItem.SalaryItem.Label == "TechnicalAllowance")
            //    TechnicalAllowance = pOrderItem.Amount;
            if (pOrderItem.SalaryItem.Label == "SeniorityAllowance")
                SeniorityAllowance = pOrderItem.Amount;

            item.Amount = pOrderItem.Amount;
            //model.PayrollItems.Add(item);
        }
        #endregion

        var result = await _context.Attendances
            .Where(x =>
                x.AttendancePersianDate.StartsWith(prefix) &&
                x.PersonnelId == personnelId)
            .GroupBy(x => x.PersonnelId)
            .Select(g => new
            {
                Count = g.Count()
            })
            .FirstOrDefaultAsync();
        int daysWorked = result?.Count ?? 0;

        #region Attendence
        var totals = await _context.Attendances
            .Where(x => x.PersonnelId == personnelId &&
                        x.AttendancePersianDate.StartsWith(prefix))
            .GroupBy(x => x.PersonnelId)
            .Select(g => new AttendanceTotalsViewModel
            {
                DelayMinute = g.Sum(x => x.DelayMinute),
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

        //SalaryItemRule? rulesHousing = new SalaryItemRule();
        //SalaryItemRule? rulesChild = new SalaryItemRule();
        //SalaryItemRule? rulesFood = new SalaryItemRule();
        //SalaryItemRule? rulesMarital = new SalaryItemRule();
        //SalaryItemRule? rulesSeniority = new SalaryItemRule();
        //await _context.SalaryItemRules
        //          .Where(r => r.SalaryItem.Label == "MaritalAllowance").FirstOrDefaultAsync();
        decimal AmountMarital = activeOrderItem.Details.Where(i => i.SalaryItem.Label == "MaritalAllowance").FirstOrDefault().Amount;
        decimal AmountHousing = activeOrderItem.Details.Where(i => i.SalaryItem.Label == "HousingAllowance").FirstOrDefault().Amount;
        decimal AmountChild = activeOrderItem.Details.Where(i => i.SalaryItem.Label == "ChildAllowance").FirstOrDefault().Amount;
        decimal AmountFood = activeOrderItem.Details.Where(i => i.SalaryItem.Label == "FoodAllowance").FirstOrDefault().Amount;
        decimal AmountSeniority = activeOrderItem.Details.Where(i => i.SalaryItem.Label == "SeniorityAllowance").FirstOrDefault().Amount;

        Personnel Person = await _context.Personnels.FindAsync(personnelId);
        var variables = new Dictionary<string, object>
        {
            ["BaseSalary"] = BaseSalary,
            ["MonthDays"] = monthDays,
            ["DaysWorked"] = daysWorked,

            //coming from personnel
            ["ChildNo"] = Person.ChildNo,
            //coming from salaryItemRule
            ["HousingAllowance"] = AmountHousing,
            ["ChildAllowance"] = AmountChild,
            ["FoodAllowance"] = AmountFood,
            ["MaritalAllowance"] = AmountMarital,
            ["SeniorityAllowance"] = AmountSeniority,
            //coming from personnelOrder
            ["JobAllowance"] = JobAllowance,
            ["ResponsibilityAllowance"] = ResponsibilityAllowance,

            ["ExtraMinute"] = totals?.ExtraMinute ?? 0,
            ["MissionMinute"] = totals?.MissionMinute ?? 0,
            ["AbsenceMinute"] = totals?.AbsenceMinute ?? 0,
            ["DelayMinute"] = totals?.DelayMinute ?? 0,

            ["WorkingMinute"] = totals?.WorkingMinute ?? 0,
            ["HolidayMinute"] = totals?.HolidayMinute ?? 0,
            ["LeaveNormalMinute"] = totals?.LeaveNormalMinute ?? 0,
            ["LeaveSickMinute"] = totals?.LeaveSickMinute ?? 0,
            ["LeaveWithoutSalaryMinute"] = totals?.LeaveWithoutSalaryMinute ?? 0
        };
        //["TechnicalAllowance"] = TechnicalAllowance,
        //variables["TechnicalAllowance"] = TechnicalAllowance;

        List<SalaryItem> attendanceSalaryItems = null;// _context.SalaryItems.Where(i => i.FormulaValue.Trim()!="").ToList();
        try
        {
            attendanceSalaryItems = _context.SalaryItems.Where(i => i.FormulaValue.Trim() != "").ToList();

            foreach (var salaryItem in attendanceSalaryItems)
            {
                if (salaryItem.IsSystem == 0)
                {
                    //add variable to dictionary
                    string label = salaryItem.Label;
                    string source = salaryItem.Source;
                    // get it from salaryitemrules
                    decimal labelResult = 0;
                    if (source != "PersonnelOrder")
                        labelResult = await _context.SalaryItems
                              .Join(
                                  _context.SalaryItemRules,
                                  salaryItem => salaryItem.Id,
                                  salaryItemRule => salaryItemRule.SalaryItemId,
                                  (salaryItem, salaryItemRule) => new
                                  {
                                      SalaryItem = salaryItem,
                                      Amount = salaryItemRule.Amount
                                  })
                              .Where(x => x.SalaryItem.Label == label)
                              .Select(x => x.Amount)
                              .FirstOrDefaultAsync();
                    // get it from personnelorder
                    if (source == "PersonnelOrder")
                        labelResult = await _context.PersonnelOrderDetails
                            .Join(
                                _context.SalaryItems,
                                detail => detail.SalaryItemId,
                                salaryItem => salaryItem.Id,
                                (detail, salaryItem) => new { detail, salaryItem }
                            )
                            .Join(
                                _context.PersonnelOrders,
                                x => x.detail.PersonnelOrderId,
                                personnelOrder => personnelOrder.Id,
                                (x, personnelOrder) => new { x.detail, x.salaryItem, personnelOrder }
                            )
                            .Where(x =>
                                x.salaryItem.Label == label &&
                                x.personnelOrder.PersonnelId == 1
                            )
                            .Select(x => x.detail.Amount)
                            .FirstOrDefaultAsync();

                    variables[label] = labelResult;
                }
                if (string.IsNullOrWhiteSpace(salaryItem.FormulaValue))
                    continue;

                decimal amount = _payrollFormulaService.Calculate(
                    salaryItem.FormulaValue,
                    variables);

                if (amount == 0)
                    continue;

                model.PayrollItems.Add(new PayrollItem
                {
                    SalaryItemId = salaryItem.Id,
                    PlusMinus = salaryItem.PlusMinus,
                    Amount = amount
                });
            }
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.Print(ex.Message);
        }

         

        #endregion

        ////PayrollAdjustment
        List<SalaryItem> lst3 = _context.SalaryItems.Where(i => i.Source == "PayrollAdjustment").ToList();
        foreach (SalaryItem itemSalary3 in lst3)
        {
            String Label = itemSalary3.Label;

            var result3 = await _context.PayrollAdjustmentDetails
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

            if (result3 != null)
            {
                PayrollItem item = new PayrollItem();
                item.SalaryItemId = itemSalary3.Id;
                item.Amount = Convert.ToDecimal(result3.Amount);
                item.Description = result3.Description;
                item.FormulaValue = itemSalary3.FormulaValue;
                model.PayrollItems.Add(item);
            }

        }
        ////Total insurance

        var salaryItemIds = model.PayrollItems
        .Select(x => x.SalaryItemId)
        .Distinct()
        .ToList();

        var insuranceRules = await _context.SalaryItemRules
            .Where(r => salaryItemIds.Contains(r.SalaryItem.Id))
            .ToDictionaryAsync(r => r.SalaryItem.Id);

        decimal totalInsuranceAmount = 0;
        foreach (PayrollItem item in model.PayrollItems)
            item.SalaryItem = _context.SalaryItems.Where(i => i.Id == item.SalaryItemId).FirstOrDefault();

        //// empoyee rate
        decimal employeeRate = await _context.PersonnelOrders
          .Where(po => po.IsActive && po.PersonnelId == personnelId)
          .Join(
              _context.RuleInsurances,
              po => po.RuleInsuranceGroupId,
              ri => ri.RuleInsuranceGroupId,
              (po, ri) => ri.EmployeeRate
          )
          .FirstOrDefaultAsync();
        employeeRate = employeeRate / 100;

        foreach (var itemPayroll in model.PayrollItems)
            if (insuranceRules.TryGetValue(itemPayroll.SalaryItemId, out var rule) &&
                rule.IsInsuranceBase)
            {
                InsuTaxViewModel obj = new InsuTaxViewModel();
                obj.SalaryItemId = itemPayroll.SalaryItemId;
                obj.Amount = itemPayroll.Amount;
                obj.AmountAfter = itemPayroll.Amount * employeeRate;
                lstInsurance.Add(obj);

                totalInsuranceAmount += itemPayroll.Amount * itemPayroll.SalaryItem.PlusMinus;
            }
       
        decimal InsuranceAmount = totalInsuranceAmount * employeeRate;

        ////Insurance 
        SalaryItem itemSalary1 = _context.SalaryItems.Where(i => i.Label == "Insurance").FirstOrDefault();

        PayrollItem itemInsurance = new PayrollItem();
        itemInsurance.SalaryItemId = itemSalary1.Id;
        itemInsurance.Amount = Convert.ToDecimal(InsuranceAmount);
        itemInsurance.FormulaValue = itemSalary1.FormulaValue;
        model.PayrollItems.Add(itemInsurance);

        ////Total Tax amount

        var salaryItemIdsTax = model.PayrollItems
           .Select(x => x.SalaryItemId)
           .Distinct()
           .ToList();

        var taxRules = await _context.SalaryItemRules
            .Where(r => salaryItemIdsTax.Contains(r.SalaryItem.Id))
            .ToDictionaryAsync(r => r.SalaryItem.Id);

        decimal totalTaxAmount = 0;
        foreach (var itemPayroll in model.PayrollItems)
            if (taxRules.TryGetValue(itemPayroll.SalaryItemId, out var rule) &&
                rule.IsTaxBase)
            {
                InsuTaxViewModel obj = new InsuTaxViewModel();
                obj.SalaryItemId = itemPayroll.SalaryItemId;
                obj.Amount = itemPayroll.Amount;
                obj.AmountAfter = 0;
                lstTax.Add(obj);

                totalTaxAmount += itemPayroll.Amount * itemPayroll.SalaryItem.PlusMinus;
            }
        ////Tax 
        decimal TaxAmount = CalculateTax(totalTaxAmount, _context.RuleTaxes.ToList());
        lstTax[lstTax.Count - 1].AmountAfter = TaxAmount; 
        // fill last item with total, when using remember
        // to at first show the value and then make it zero

        SalaryItem itemSalary2 = _context.SalaryItems.Where(i => i.Label == "Tax").FirstOrDefault();

        PayrollItem itemTax = new PayrollItem();
        itemTax.SalaryItemId = itemSalary2.Id;
        itemTax.Amount = Convert.ToDecimal(TaxAmount);
        itemTax.FormulaValue = itemSalary2.FormulaValue;
        model.PayrollItems.Add(itemTax);

        string PersonnelName = Person.FirstName + " " + Person.LastName;
        ViewBag.PersonnelYearMonthName = AppUtil.GetPersianMonthName(month) + " " + year + " " + PersonnelName;

        foreach (PayrollItem item in model.PayrollItems)
            item.SalaryItem = _context.SalaryItems.FirstOrDefault(i => i.Id == item.SalaryItemId);

        model.PayrollItems = model.PayrollItems
            .OrderBy(item => item.SalaryItem.Priority)
            .ToList();
        return model;
    }

    decimal CalculateTax(decimal amount, List<RuleTax> rules)
    {
        decimal tax = 0;
        decimal previousLimit = 0;

        foreach (var rule in rules.OrderBy(x => x.Amount))
        {
            if (amount <= previousLimit)
                break;

            decimal taxableAmount =
                Math.Min(amount, rule.Amount) - previousLimit;

            if (taxableAmount > 0)
                tax += taxableAmount * rule.Rate / 100m;

            previousLimit = rule.Amount;
        }

        return tax;
    }

    public async Task<IActionResult> Salary(int id, string ym)
    {


        Personnel person = _context.Personnels.Where(i => i.Id == id).FirstOrDefault();
        int month = Convert.ToInt32(ym.Substring(5, 2));
        int year = Convert.ToInt32(ym.Substring(0, 4));

        PayrollViewModel modelData = await GetPayrollData(year, month, id);

        //var model = new SalaryReportViewModel
        //{
        //    PersonnelId = id,
        //    PersonnelCode = "1001",
        //    FirstName = person.FirstName,
        //    LastName = person.LastName,
        //    NationalCode = "1234567890",
        //    PayrollMonth = year + " " +  DateUtil.GetPersianMonthName(month),
        //    BasicSalary = 150000000,
        //    HousingAllowance = 9000000,
        //    FoodAllowance = 14000000,
        //    Overtime = 12000000,
        //    Deductions = 18000000
        //};
        ViewBag.YearMonthName = AppUtil.GetPersianMonthName(modelData.PayrollMonth) + " " + year;
        return View(modelData);
    }

    [HttpGet]
    public async Task<IActionResult> SalaryPdf(int id, string ym, CancellationToken cancellationToken)
    {

        var url =
            Url.Action(
                nameof(Salary),
                "Payroll",
                new { id, ym },
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