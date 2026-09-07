using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace givPayroll.Models
{
    [Table("JobGroup", Schema = "ref")]
    public class JobGroup
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Key]
        [Display(Name = "کد")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Code is required.")]
        [Display(Name = "کد")]
        public  int  Code { get; set; }

        [MaxLength(500)]
        [Required(ErrorMessage = "Name is required.")]
        [Display(Name = "نام")]
        public String Name { get; set; }

    }
}
