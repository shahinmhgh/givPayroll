using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace givPayroll.Models
{
    [Table("RuleInsurance", Schema = "ref")]
    public class RuleInsurance
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        [Display(Name = "گروه بیمه")]
        [Required(ErrorMessage = "گروه بیمه الزامی است.")]
        public int RuleInsuranceGroupId { get; set; }

        [Display(Name = "تاریخ اجرا")]
        [Required(ErrorMessage = "تاریخ اجرا الزامی است.")]
        public DateTime EffectiveDate { get; set; }

        [Required]
        [Display(Name = "درصد سهم بیمه‌شده")]
        [Column(TypeName = "decimal(18,2)")]
        public decimal EmployeeRate { get; set; }

        [Required]
        [Display(Name = "درصد سهم کارفرما")]
        [Column(TypeName = "decimal(18,2)")]
        public decimal EmployerRate { get; set; }

        
        [Display(Name = "درصد بیکاری")]
        [Column(TypeName = "decimal(18,2)")]
        public decimal UnemploymentRate { get; set; }

        public DateTime DateCreated { get; set; }

        public string UserCreated { get; set; }

        public DateTime? DateChanged { get; set; }

        public string? UserChanged { get; set; }


        // Navigation

        [ForeignKey(nameof(RuleInsuranceGroupId))]
        public virtual RuleInsuranceGroup? RuleInsuranceGroup { get; set; }
    }
}