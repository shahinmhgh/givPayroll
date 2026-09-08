public class PayrollAdjustmentListViewModel
{
    public int Id { get; set; }

    public string Description { get; set; } = "";

    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }

    public int AdjustmentCount { get; set; }

    public string AdjustmentTypeName { get; set; } = "";

    public bool Loan { get; set; }

    public decimal TotalAmount { get; set; }

    public int PersonnelCount { get; set; }
}


public class PayrollAdjustmentListResultViewModel
{
    public List<PayrollAdjustmentListViewModel> Items { get; set; }
        = new();

    public int Page { get; set; }

    public int PageSize { get; set; }

    public int TotalCount { get; set; }
}