using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.Contracts;

namespace givPayroll.Models
{
    public class PersonnelOrderViewModel
    {
        public long Id { get; set; }

        [Required(ErrorMessage = "انتخاب پرسنل الزامی است.")]
        [Display(Name = "پرسنل")]
        public int PersonnelId { get; set; }
        public Personnel Personnel { get; set; }

        [Required(ErrorMessage = "انتخاب قرارداد الزامی است.")]
        [Display(Name = "قرارداد")]
        public int ContractId { get; set; }
        public PersonnelContract Contract  { get; set; }

        [Display(Name = "گروه بیمه")]
        public int RuleInsuranceGroupId { get; set; }
 
        [Required(ErrorMessage = "شماره حکم الزامی است.")]
        [Display(Name = "شماره حکم")]
        public int No { get; set; }
        [Required(ErrorMessage = "نوع شغل الزامی است.")]
        [Display(Name = "نوع شغل")]
        public int JobId { get; set; }
        
        public Job Job { get; set; }

        [Required(ErrorMessage = "تاریخ الزامی است.")]
        
        [Display(Name = "تاریخ")]
        public DateTime IssueDate { get; set; }
        [Display(Name = "تاریخ")]
        public DateTime? EndDate { get;  set; }
        [Display(Name = "تاریخ")]
        public DateTime StartDate { get;  set; }


        [Display(Name = "توضیحات")]
        public string? Description { get; set; }

        [Display(Name = "فعال")]
        public bool IsActive { get; set; }

        public List<SelectListItem> RuleInsuranceGroupList { get; set; }
   = new();
        public List<PersonnelOrderDetailViewModel> Details { get; set; }
            = new();


        // Select Lists

        public List<SelectListItem> PersonnelList { get; set; }    = new();

        public List<SelectListItem> ContractList { get; set; }   = new();
        public List<SelectListItem> JobList { get;   set; } = new();
    }


    public class PersonnelOrderDetailViewModel
    {
        public int Id { get; set; }

        public int SalaryItemId { get; set; }

        public string SalaryItemName { get; set; } = string.Empty;

        public string SalaryItemLabel { get; set; } = string.Empty;

        public string Unit { get; set; } = string.Empty;

        public decimal Amount { get; set; }
        public int PersonnelOrderId { get; internal set; }
    }
}