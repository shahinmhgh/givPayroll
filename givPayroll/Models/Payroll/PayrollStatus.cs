using givPayroll.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace givPayroll.Models
{
    [Table("PayrollStatus", Schema = "ref")]
    public class PayrollStatus
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Display(Name = "شناسه")]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "وضعیت")]
        public string StatusName { get; set; } = string.Empty;

        // Navigation
        public virtual ICollection<Payroll> Payrolls { get; set; }
            = new List<Payroll>();
    }
}