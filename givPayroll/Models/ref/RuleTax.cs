using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace givPayroll.Models
{
    [Table("RuleTax", Schema = "ref")]
    public class RuleTax
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Display(Name = "شناسه")]
        public int Id { get; set; }

        [Required]
        [Display(Name = "تاریخ اجرا")]
        public DateTime EffectiveDate { get; set; }

        [Required]
        [Display(Name = "مبلغ")]
        [Column(TypeName = "decimal(18,3)")]
        public decimal Amount { get; set; }

        [Required]
        [Display(Name = "نرخ")]
        [Column(TypeName = "decimal(7,2)")]
        public decimal Rate { get; set; }

        [Display(Name = "تاریخ ایجاد")]
        public DateTime DateCreated { get; set; }

        [Display(Name = "کاربر ایجاد کننده")]
        public int UserCreated { get; set; }

        [Display(Name = "تاریخ تغییر")]
        public DateTime? DateChanged { get; set; }

        [Display(Name = "کاربر تغییر دهنده")]
        public int? UserChanged { get; set; }
    }
}