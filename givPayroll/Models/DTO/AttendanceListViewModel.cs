namespace givPayroll.ViewModels
{
    public class AttendanceListViewModel
    {
        public int Id { get; set; }

        public int PersonnelId { get; set; }

        public string AttendanceDate { get; set; } = "";

        public int WorkingExpectedMinute { get; set; }
        public int WorkingMinute { get; set; }
        public int ExtraMinute { get; set; }
        public int HolidayMinute { get; set; }
        public int DelayMinute { get; set; }
        public int EarlyArrivalMinute { get; set; }
        public int LeaveNormalMinute { get; set; }
        public int LeaveSickMinute { get; set; }
        public int AbsenceMinute { get; set; }
        public int MissionMinute { get; set; }

        public bool HasWorked { get; set; }

        // New field
        public bool Holiday { get; set; }
        public string HolidayDescriptrion { get; set; } = "";
    }
}