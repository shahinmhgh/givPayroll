using givPayroll.Models;

public class InsuTaxViewModel
{
    public int SalaryItemId { get; set; }
    public decimal Amount { get; set; }
    public SalaryItem SalaryItem { set; get; }

    public decimal AmountAfter { get; set; }
    public string Description { get; internal set; }
}