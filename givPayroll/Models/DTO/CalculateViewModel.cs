using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

public class CalculateViewModel
{
    [Display(Name = "پرسنل")]
    public int attendancePersonnel { get; set; }

    public List<SelectListItem> Months { get; set; }
        = new();

    public List<AttendanceCheckViewModel> Check =  new();
}