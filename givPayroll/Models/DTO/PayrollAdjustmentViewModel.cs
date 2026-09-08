using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace givPayroll.ViewModels
{
    public class PayrollAdjustmentViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "توضیحات الزامی است")]
        [Display(Name = "توضیحات")]
        public string Description { get; set; } = string.Empty;

        [Required(ErrorMessage = "تاریخ شروع الزامی است")]
        [Display(Name = "تاریخ شروع")]
        public DateTime StartDate { get; set; }

        [Required(ErrorMessage = "تعداد اقساط الزامی است")]
        [Range(1, 120, ErrorMessage = "تعداد اقساط باید بین 1 تا 120 باشد")]
        [Display(Name = "تعداد اقساط")]
        public int AdjustmentCount { get; set; }

        [Display(Name = "تاریخ پایان")]
        public DateTime EndDate { get; set; }

        [Required(ErrorMessage = "نوع تعدیل را انتخاب کنید")]
        [Display(Name = "نوع تعدیل")]
        public int AdjustmentTypeId { get; set; }

        [Display(Name = "وام")]
        public bool Loan { get; set; }

        [Required(ErrorMessage = "مبلغ کل الزامی است")]
        [Display(Name = "مبلغ کل")]
        public decimal TotalAmount { get; set; }

        public List<SelectListItem> AdjustmentTypes { get; set; }
            = new();

        public List<int> PersonnelIds { get; set; }
            = new();

        public List<PayrollAdjustmentPersonnelViewModel> Personnels { get; set; }
            = new();
    }

    public class PayrollAdjustmentPersonnelViewModel
    {
        public int Id { get; set; }

        public int PersonnelId { get; set; }

        public string PersonnelName { get; set; } = string.Empty;
    }
}