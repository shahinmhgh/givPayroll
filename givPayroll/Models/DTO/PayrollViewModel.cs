using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace givPayroll.Models
{
    public class PayrollViewModel 
    {

        [Display(Name = "تاریخ صدور")]
        [NotMapped]
        public string IssueDatePersian
        {
            get
            {
                return AppUtil.M2S(IssueDate.GetValueOrDefault());
            }
        }

        [DataType(DataType.Date)]
        [Display(Name = "تاریخ صدور")]
        public DateTime? IssueDate { get; set; }

        [Required]
        [Display(Name = "پرسنل")]
        public int PersonnelId { get; set; }

        // Navigation properties
        [ForeignKey(nameof(PersonnelId))]
        public virtual Personnel? Personnel { get; set; }

        [Required]
        [Range(1300, 1500, ErrorMessage = "سال حقوق نامعتبر است.")]
        [Display(Name = "سال")]
        public int PayrollYear { get; set; }

        [Required]
        [Range(1, 12, ErrorMessage = "ماه حقوق باید بین 1 تا 12 باشد.")]
        [Display(Name = "ماه")]
        public int PayrollMonth { get; set; }
        

        public virtual PersonnelOrder? PersonnelOrder { get; set; }

        public List<PayrollItem> PayrollItems { get; set; } = new List<PayrollItem>();
    }
}
