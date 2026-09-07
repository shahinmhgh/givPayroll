using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace givPayroll.Models
{
    [Table("ContractType", Schema = "ref")]
    public class ContractType
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Key]
        [Display(Name = "کد")]
        public int Id { get; set; }

        [MaxLength(50)]
        [Required(ErrorMessage = "ContractType is required.")]
        [Display(Name = "نوع قرارداد")]
        public  String ContractTypeName { get; set; }

    }
}
