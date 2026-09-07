using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace givPayroll.Models
{
    [Table("Education", Schema = "ref")]
    public class Education
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Key]
        [Display(Name = "کد")]
        public int Id { get; set; }

        [MaxLength(50)]
        [Required(ErrorMessage = "Education is required.")]
        [Display(Name = "تحصیلات")]
        public  String EducationName { get; set; }

    }
}
