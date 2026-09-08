using givPayroll.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace givPersonnel.Models.DTO
{
    public class PayrollAdjustmentViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "انتخاب نوع تعدیل الزامی است.")]
        [Display(Name = "نوع تعدیل")]
        public int AdjustmentTypeId { get; set; }

        [Required(ErrorMessage = "تاریخ شروع الزامی است.")]
        [Display(Name = "تاریخ شروع")]
        public DateTime StartDate { get; set; }  

        [Required(ErrorMessage = "تاریخ پایان الزامی است.")]
        [Display(Name = "تاریخ پایان")]
        public DateTime EndDate { get; set; }  

        [Required(ErrorMessage = "مبلغ الزامی است.")]
        [Range(typeof(decimal), "1", "999999999999999",
            ErrorMessage = "مبلغ باید بزرگتر از صفر باشد.")]
        [Display(Name = "مبلغ ماهانه هر نفر")]
        public decimal Amount { get; set; }

        [Display(Name = "توضیحات")]
        public string Description { get; set; } = string.Empty;

        public List<int> PersonnelIds { get; set; }
            = new List<int>();

        public List<SelectListItem> AdjustmentTypes { get; set; }
            = new List<SelectListItem>();

        public List<Personnel> Personnels { get; set; }
            = new List<Personnel>();
    }
}