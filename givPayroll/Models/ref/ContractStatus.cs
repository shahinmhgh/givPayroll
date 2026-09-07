using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace givPayroll.Models
{
    [Table("ContractStatus", Schema = "ref")]
    public class ContractStatus
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Key]
        [Display(Name = "کد")]
        public int Id { get; set; }

        [MaxLength(50)]
        [Required(ErrorMessage = "ContractStatus is required.")]
        [Display(Name = "وضعیت قرارداد")]
        public  String ContractStatusName { get; set; }

    }
}
