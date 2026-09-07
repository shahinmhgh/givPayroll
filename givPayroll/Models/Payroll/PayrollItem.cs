using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace givPayroll.Models
{
    [Table("PayrollItem", Schema = "Payroll")]
    public class PayrollItem
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Display(Name = "شناسه")]
        public int Id { get; set; }

        [Required]
        [Display(Name = "حقوق")]
        public int PayrollId { get; set; }

        [Required]
        [Display(Name = "قلم حقوق")]
        public int SalaryItemId { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,3)")]
        [Display(Name = "مبلغ")]
        public decimal Amount { get; set; }

        [Display(Name = "تعدیل حقوق")]
        public int? PayrollAdjustmentId { get; set; }


        // Navigation Properties

        [ForeignKey(nameof(PayrollId))]
        public virtual Payroll? Payroll { get; set; }

        [ForeignKey(nameof(SalaryItemId))]
        public virtual SalaryItem? SalaryItem { get; set; }

        [ForeignKey(nameof(PayrollAdjustmentId))]
        public virtual PayrollAdjustment? PayrollAdjustment { get; set; }
    }
}