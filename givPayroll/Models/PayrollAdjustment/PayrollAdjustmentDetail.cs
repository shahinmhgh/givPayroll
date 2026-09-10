using givPayroll.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace givPayroll.Models
{
    [Table("PayrollAdjustmentDetail", Schema = "Payroll")]
    public class PayrollAdjustmentDetail
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Display(Name = "شناسه")]
        public int Id { get; set; }

        [Required]
        [Display(Name = "تعدیل حقوق")]
        public int PayrollAdjustmentId { get; set; }

        [Required]
        [Column(TypeName = "ntext")]
        [Display(Name = "توضیحات")]
        public string Description { get; set; } = string.Empty;

        [Required]
        public DateTime PayrollAdjustmentDate { get; internal set; }
        //[Required]
        //[Display(Name = "سال حقوق")]
        //public int PayrollYear { get; set; }

        //[Required]
        //[Display(Name = "ماه حقوق")]
        //public int PayrollMonth { get; set; }

        [Required]
        [Display(Name = "سال")]
        public int PayrollPersianYear { get; set; }

        [Required]
        [Display(Name = "ماه")]
        public int PayrollPersianMonth { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,3)")]
        [Display(Name = "مبلغ")]
        public decimal Amount { get; set; }


        // Navigation Properties

        [ForeignKey(nameof(PayrollAdjustmentId))]
        public virtual PayrollAdjustment PayrollAdjustment { get; set; }
        
    }
}