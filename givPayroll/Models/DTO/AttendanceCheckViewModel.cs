public class AttendanceCheckViewModel
{
    public int PersonnelId { get; set; }

    public string PersonnelName { get; set; } = "";

    public string PersonnelCode { get; set; } = "";

    public decimal Required { get; set; }

    public decimal Work { get; set; }

    public decimal Leave { get; set; }

    public decimal SickLeave { get; set; }

    public decimal Absence { get; set; }

    public decimal Mission { get; set; }

    public decimal Total =>  Work + Leave + SickLeave + Absence + Mission;

    public bool IsCorrect =>   Total >= Required;

    public int LeaveWithoutSalary { get; internal set; }
}