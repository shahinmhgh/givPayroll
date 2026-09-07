using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace givPayroll.Models
{
    [Table("RuleInsuranceGroup", Schema = "ref")]
    public class RuleInsuranceGroup
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Key]
        [Display(Name = "کد")]
        public int Id { get; set; }

        [MaxLength(50)]
        [Required(ErrorMessage = "ContractStatus is required.")]
        [Display(Name = "نام گروه بیمه")]
        public  String GroupName { get; set; }

    }
}
