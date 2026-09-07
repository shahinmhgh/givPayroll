using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace givPayroll.Models
{
    public class RuleInsuranceViewModel
    {
        public int Id { get; set; }

        [Display(Name = "گروه بیمه")]
        [Required(ErrorMessage = "گروه بیمه را انتخاب کنید.")]
        public int RuleInsuranceGroupId { get; set; }

        [Display(Name = "تاریخ اجرا")]
        [Required(ErrorMessage = "تاریخ اجرا را وارد کنید.")]
        public DateTime  EffectiveDate { get; set; } 

        [Display(Name = "درصد سهم بیمه‌شده")]
        [Range(0, 100, ErrorMessage = "درصد باید بین 0 تا 100 باشد.")]
        public decimal EmployeeRate { get; set; }

        [Display(Name = "درصد سهم کارفرما")]
        [Range(0, 100, ErrorMessage = "درصد باید بین 0 تا 100 باشد.")]
        public decimal EmployerRate { get; set; }

        [Display(Name = "درصد بیکاری")]
        [Range(0, 100, ErrorMessage = "درصد باید بین 0 تا 100 باشد.")]
        public decimal UnemploymentRate { get; set; }


        // Display

        public string? RuleInsuranceGroupName { get; set; }


        // Dropdown

        public List<SelectListItem> RuleInsuranceGroupList { get; set; }
            = new List<SelectListItem>();
    }
}