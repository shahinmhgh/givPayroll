using Microsoft.AspNetCore.Mvc.Rendering;

public class CalculateViewModel
{
    public List<SelectListItem> Months { get; set; }
        = new();

    public List<AttendanceCheckViewModel> Check =  new();
}