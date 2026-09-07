using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace givPayroll.Models
{
    [Table("SalaryItem", Schema="ref")]
    public class SalaryItem
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Display(Name = "کد")]
        public int Id { get; set; }

        [Display(Name = "اولویت")]
        public int Priority { get; set; }

        [Required(ErrorMessage = "نام الزامی است.")]
        [MaxLength(50)]
        [Display(Name = "نام")]
        public string SalaryItemName { get; set; } = string.Empty;

        [MaxLength(100)]
        [Display(Name = "عنوان")]
        public string Label { get; set; } = string.Empty;

        [MaxLength(50)]
        [Display(Name = "واحد")]
        public string Unit { get; set; } = string.Empty;

        [MaxLength(50)]
        [Display(Name = "منبع")]
        public string Source { get; set; } = string.Empty;

        [Display(Name = "علامت")]
        public int PlusMinus { get; set; }

        [MaxLength(20)]
        [Display(Name = "روش محاسبه")]
        public string CalculationMode { get; set; } = string.Empty;

        [Display(Name = "فرمول")]
        [Column(TypeName = "ntext")]
        public string FormulaValue { get; set; } = string.Empty;

        [MaxLength(50)]
        [Display(Name = "کد حساب")]
        public string? AccAccountCode { get; set; }

        [Display(Name = "کد تفصیلی")]
        public long? AccUniqueId { get; set; }

        [Required]
        [Display(Name = "تاریخ ایجاد")]
        public DateTime DateCreated { get; set; }

        [Required]
        [Display(Name = "کاربر ایجادکننده")]
        public int UserCreated { get; set; }

        [Display(Name = "تاریخ تغییر")]
        public DateTime? DateChanged { get; set; }

        [Display(Name = "کاربر تغییر دهنده")]
        public int? UserChanged { get; set; }

        public virtual ICollection<PayrollItem> PayrollItems { get; set; }  = new List<PayrollItem>();
        public virtual ICollection<SalaryItemRule> SalaryItemRules { get; set; } = new List<SalaryItemRule>();
    }
}