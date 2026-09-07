using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace givPayroll.Models
{
    [Table("MaritalStatus", Schema = "ref")]
    public class MaritalStatus
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Key]
        [Display(Name = "کد")]
        public int Id { get; set; }

        [MaxLength(50)]
        [Required(ErrorMessage = "Name is required.")]
        [Display(Name = "وضعیت تاهل")]
        public  String MaritalStatusName { get; set; }

    }
}
