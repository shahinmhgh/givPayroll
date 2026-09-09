using Microsoft.AspNetCore.Mvc.Rendering;

public class AttendanceCheckViewModel
{
    public List<SelectListItem> Months { get; set; }
        = new();

    public int PersonnelId { get; set; }

    public string PersonnelName { get; set; } = "";

    public string PersonnelCode { get; set; } = "";

    public int  Required { get; set; }

    public int Work { get; set; }


    public int Leave { get; set; }
    public int LeaveSickMinute { get; set; }
    public int LeaveWithoutSalary { get; internal set; }
    public int LeaveNormalMinute { get; internal set; }

    public int Absence { get; set; }
    public int Mission { get; set; }

    public int Total => Work + LeaveWithoutSalary + Leave + LeaveSickMinute + LeaveNormalMinute + Absence + Mission;

   
    public string  IsCorrectErrText { get; set; }
    public string  hasWorkedErrText { get; set; }

    public AttendanceWorkStatus Status { get; set; }

    public string StatusText => Status switch
    {
        AttendanceWorkStatus.Incorrect => "نادرست",
        AttendanceWorkStatus.Incomplete => "کارکرد ناقص",
        AttendanceWorkStatus.Complete => "کارکرد کامل",
        _ => ""
    };

    public bool CanPreviewPayroll =>
        Status == AttendanceWorkStatus.Complete;

    public enum AttendanceWorkStatus
    {
        None, 
        Incorrect,
        Incomplete,
        Complete
    }

}