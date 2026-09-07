using Microsoft.AspNetCore.Mvc.Rendering;

namespace givPayroll.Models
{
    public class AttendanceViewModel
    {
        public int Year { get; set; }

        public int Month { get; set; }

        public int PersonnelId { get; set; }


        public List<SelectListItem> Years { get; set; }
            = new();

        public List<SelectListItem> Months { get; set; }
            = new();

        public List<SelectListItem> Personnels { get; set; }
            = new();


        public List<Attendance> Attendances { get; set; }
            = new();


        public string PersonnelName { get; set; }
            = string.Empty;
    }
}