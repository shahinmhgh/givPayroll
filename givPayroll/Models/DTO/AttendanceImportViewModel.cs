using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace givPayroll.Models
{
    public class AttendanceImportViewModel
    {
        public int Year { get; set; }

        public int Month { get; set; }

        public IFormFile? File { get; set; }

        public List<SelectListItem> Years { get; set; }
            = new();

        public List<SelectListItem> Months { get; set; }
            = new();
    }
}