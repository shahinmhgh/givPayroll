using Azure;
using SQLitePCL;
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
        public String FirstName { get; set; }

        [MaxLength(11)]
        [Required(ErrorMessage = "Mobiler Number is required.")]
        [Display(Name = "موبایل")]
        public String Mobile { get; set; } = string.Empty;

        [MaxLength(50)]
        [Required(ErrorMessage = "Last Name is required.")]
        [Display(Name = "نام خانوادگی")]
        public String LastName { get; set; }

        [Required(ErrorMessage = "MaritalStatus is required")]
        [Display(Name = "وضعیت تاهل")]
        public int MaritalStatusId { get; set; }

        

        [MaxLength(50)]
        [Required(ErrorMessage = "شماره بیمه is required.")]
        [Display(Name = "شماره بیمه")]
        public String InsuranceNo { get; set; } = string.Empty;

        [MaxLength(50)]
        [Required(ErrorMessage = "کد کارمندی is required.")]
        [Display(Name = "کد کارمندی")]
        public String PersonnelNo { get; set; } = string.Empty;

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

        

        [Display(Name = "سن")]
        [NotMapped]
        public int Age
        {
            get
            {
                int ageYears = 0;
                AppUtil.CompleteAge(BirthDate.GetValueOrDefault(), ref ageYears);
                return ageYears;
            }
        }


        [Display(Name = "تعداد بچه حائز شرایط")]
        [NotMapped]
        public int ChildNo
        {
            get
            {
                if (PersonnelFamilies == null) return 0;
                if (PersonnelFamilies.Count() == 0)
                    return 0;

                List<PersonnelFamily> lst = PersonnelFamilies.ToList();
                int childNo = 0;
                foreach (PersonnelFamily item in lst)
                {
                    if (item.CanReceive)
                        childNo += 1;
                }
                return childNo;
            }
        }


        public virtual ICollection<PayrollAdjustmentPersonnel> PayrollAdjustmentPersonnels { get; set; }
    = new List<PayrollAdjustmentPersonnel>();

    }
}
