using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace givPayroll.Models
{
    [Table("Company", Schema = "dbo")]
    public class Company
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Display(Name = "کد")]
        public int Id { get; set; }

        [Required(ErrorMessage = "نام شرکت الزامی است.")]
        [MaxLength(50)]
        [Display(Name = "نام شرکت")]
        public string CompanyName { get; set; } = string.Empty;

        [Required(ErrorMessage = "کد بیمه الزامی است.")]
        [MaxLength(20)]
        [Display(Name = "کد بیمه")]
        public string InsuranceCode { get; set; } = string.Empty;

        [MaxLength(500)]
        [Display(Name = "آدرس")]
        public string Address { get; set; } = string.Empty;
    }
}