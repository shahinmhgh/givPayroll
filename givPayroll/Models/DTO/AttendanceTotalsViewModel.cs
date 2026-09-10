public class AttendanceTotalsViewModel
{
    public int WorkingMinute { get; set; }

    public int ExtraMinute { get; set; }

    public int HolidayMinute { get; set; }

    public int LeaveNormalMinute { get; set; }

    public int LeaveWithoutSalaryMinute { get; set; }

    public int LeaveSickMinute { get; set; }

    public int AbsenceMinute { get; set; }

    public int MissionMinute { get; set; }
    public int DelayMinute { get; internal set; }
}