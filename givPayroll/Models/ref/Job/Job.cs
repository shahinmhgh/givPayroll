using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace givPayroll.Models
{
    [Table("Job", Schema = "ref")]
    public class Job
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
        public String Title { get; set; }

        [MaxLength(500)]
        [Display(Name = "شرح")]
        public String? Description { get; set; } = string.Empty; 

 
        public int JobGroupId { get; set; }
        [ForeignKey("JobGroupId")]
        public JobGroup JobGroup { get; set; }

   public int JobGradeId { get; set; }
        [ForeignKey("JobGradeId")]
        public JobGrade JobGrade  { get; set; }

 public bool Active { get; set; }

        public ICollection<PersonnelOrder> PersonnelOrders { get; set; }
        = new List<PersonnelOrder>();

    }
}
