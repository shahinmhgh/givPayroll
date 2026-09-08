using givPayroll.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace givPayroll.Models
{
    [Table("PayrollAdjustmentPersonnel", Schema = "Payroll")]
    public class PayrollAdjustmentPersonnel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Display(Name = "شناسه")]
        public int Id { get; set; }

        [Required]
        [Display(Name = "تعدیل حقوق")]
        public int PayrollAdjustmentId { get; set; }

        [Required]
        [Display(Name = "پرسنل")]
        public int PersonnelId { get; set; }


        // Navigation Properties

        [ForeignKey(nameof(PayrollAdjustmentId))]
        public virtual PayrollAdjustment? PayrollAdjustment { get; set; }

        [ForeignKey(nameof(PersonnelId))]
        public virtual Personnel? Personnel { get; set; }
    }
}