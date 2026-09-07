using Microsoft.AspNetCore.Mvc;
using Microsoft.Playwright;
using givPayroll.Data;
using givPayroll.Models;
using givPayroll.Services;

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