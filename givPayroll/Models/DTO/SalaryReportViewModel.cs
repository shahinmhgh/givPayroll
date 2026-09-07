namespace givPayroll.Models;

public class SalaryReportViewModel
{
    public int PersonnelId { get; set; }

    public string PersonnelCode { get; set; } = "";

    public string FirstName { get; set; } = "";

    public string LastName { get; set; } = "";

    public string NationalCode { get; set; } = "";

    public string PayrollMonth { get; set; } = "";

    public decimal BasicSalary { get; set; }

    public decimal HousingAllowance { get; set; }

    public decimal FoodAllowance { get; set; }

    public decimal Overtime { get; set; }

    public decimal Deductions { get; set; }

    public decimal NetSalary =>
        BasicSalary +
        HousingAllowance +
        FoodAllowance +
        Overtime -
        Deductions;
}