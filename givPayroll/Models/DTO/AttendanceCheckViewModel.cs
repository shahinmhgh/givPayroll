using givPayroll;
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

    public AppUtil.enumAttendanceWorkStatus Status { get; set; }

    public string StatusText => Status switch
    {
        AppUtil.enumAttendanceWorkStatus.Incorrect => "نادرست",
        AppUtil.enumAttendanceWorkStatus.Incomplete => "کارکرد ناقص",
        AppUtil.enumAttendanceWorkStatus.PayrollReadyForInsert => "آماده ثبت حقوق",
        AppUtil.enumAttendanceWorkStatus.PayrollInserted => "ثبت حقوق انجام شده",
        AppUtil.enumAttendanceWorkStatus.PayrollConfirmed => "ثبت حقوق تایید شده",
        AppUtil.enumAttendanceWorkStatus.PayrollVouchered => "ثبت حقوق سند حسابداری دارد",
        _ => ""
    };

    public bool CanPreviewPayroll =>
        Status == AppUtil.enumAttendanceWorkStatus.PayrollReadyForInsert;

   

   
}