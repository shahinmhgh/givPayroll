using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace givPayroll.Models
{


    [Table("Payroll", Schema = "Payroll")]
    public class Payroll
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Display(Name = "شناسه")]
        public int Id { get; set; }

        [Required]
        [Display(Name = "پرسنل")]
        public int PersonnelId { get; set; }

        [Required]
        [Range(1300, 1500, ErrorMessage = "سال حقوق نامعتبر است.")]
        [Display(Name = "سال")]
        public int PayrollYear { get; set; }

        [Required]
        [Range(1, 12, ErrorMessage = "ماه حقوق باید بین 1 تا 12 باشد.")]
        [Display(Name = "ماه")]
        public int PayrollMonth { get; set; }

        [Display(Name = "وضعیت حقوق")]
        public int PayrollStatusID { get; set; }

        // Navigation properties
        [ForeignKey(nameof(PersonnelId))]
        public virtual Personnel? Personnel { get; set; }

        [ForeignKey(nameof(PayrollStatusID))]
        public virtual PayrollStatus? PayrollStatus { get; set; }

        public virtual ICollection<PayrollItem> Items { get; set; }
    = new List<PayrollItem>();

    }

}