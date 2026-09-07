using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace givPayroll.Models
{
    [Table("Personnel", Schema = "Payroll")]
    public class Personnel
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Key]
        [Display(Name = "کد")]
        public int Id { get; set; }

        [MaxLength(50)]
        [Required(ErrorMessage = "First Name is required.")]
        [Display(Name = "نام")]
        public  String FirstName { get; set; }

        //[Display(Name = "ترک کار")]
        //public Boolean ContractFinished { get; set; }

        [MaxLength(50)]
        [Required(ErrorMessage = "Last Name is required.")]
        [Display(Name = "نام خانوادگی")]
        public  String LastName { get; set; }

        [Required(ErrorMessage = "MaritalStatus is required")]
        [Display(Name = "وضعیت تاهل")]
        public int MaritalStatusId { get; set; }

        [ForeignKey(nameof(MaritalStatusId))]
        public MaritalStatus MaritalStatus { get; set; } = null!;

        [Required(ErrorMessage = "Education is required")]
        [Display(Name = "تحصیلات")]
        public int EducationID { get; set; }

        [ForeignKey(nameof(EducationID))]
        public Education Education { get; set; } = null!;

        [Display(Name = "تاریخ تولد")]
        [DataType(DataType.Date)]
        public DateTime? BirthDate { get; set; }

        public List<PersonnelFamily> PersonnelFamilies { get; internal set; }

        public int GenderID { get; set; }
        [ForeignKey(nameof(GenderID))]
        public virtual Gender Gender { get; set; } = null!;

       

        public virtual ICollection<PayrollAdjustmentPersonnel> PayrollAdjustmentPersonnels { get; set; }
    = new List<PayrollAdjustmentPersonnel>();

    }
}
