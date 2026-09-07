using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using givPayroll.Models;

public class ContractCreateViewModel
{
    [Display(Name = "پرسنل")]
    public int PersonnelId { get; set; }
    public int ContractId { get; set; }
    public int PersonnelOrderId { get; set; }
    [Display(Name = "نوع قرارداد")]
    public int ContractTypeId { get; set; }
    public DateTime ContractDate { get; set; } 
    public DateTime ContractEndDate { get; set; }
    public DateTime ContractStartDate { get; set; }

    public string? Description { get; set; }

    [Display(Name = "عنوان شغل")]
    public int?  JobId { get; set; }

    //[Display(Name = "عنوان شغل")]
    //public string? JobTitle { get; set; }
    //[Display(Name = "کد شغل")]
    //public string? JobStandardCode { get; set; }
    //[Display(Name = "شرح شغل")]
    //public string? JobDescription { get; set; }

    public bool IsActive { get; set; }

    public List<SelectListItem> PersonnelList { get; set; }
        = new();

    public List<SelectListItem> ContractTypeList { get; set; }
        = new();

    public List<SelectListItem> JobList { get; set; }
        = new();

    public PersonnelOrderViewModel PersonnelOrder { get; set; }
        = new();
}