using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace givPayroll.Models
{
    public class PersonnelOrder
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Display(Name = "شناسه")]
        public int Id { get; set; }

        [Required]
        [Display(Name = "قرارداد")]
        public int ContractId { get; set; }

        [Required(ErrorMessage = "انتخاب گروه بیمه الزامی است.")]
        [Display(Name = "گروه بیمه")]
        public int?  RuleInsuranceGroupId { get; set; }
        
       
        [Required]
        [Display(Name = "پرسنل")]
        public int PersonnelId { get; set; }

        [Required]
        [Display(Name = "شماره حکم")]
        public int No { get; set; }

        [Required]
        [Display(Name = "تاریخ صدور حکم")]
        public DateTime IssueDate { get; set; }

        [Required]
        [Display(Name = "تاریخ شروع")]
        public DateTime StartDate { get; set; }

        [Display(Name = "تاریخ پایان")]
        public DateTime? EndDate { get; set; }

        [Column(TypeName = "ntext")]
        [Display(Name = "توضیحات")]
        public string? Description { get; set; }

        [Display(Name = "فعال")]
        public bool IsActive { get; set; }

        [Display(Name = "تاریخ ایجاد")]
        public DateTime DateCreated { get; set; }

        [Display(Name = "کاربر ایجادکننده")]
        public String UserCreated { get; set; }

        [Display(Name = "تاریخ تغییر")]
        public DateTime? DateChanged { get; set; }

        [Display(Name = "کاربر تغییر دهنده")]
        public String? UserChanged { get; set; }


        // Navigation Properties

        public Personnel? Personnel { get; set; }

        public PersonnelContract? Contract { get; set; }

        [ForeignKey(nameof(RuleInsuranceGroupId))]
        public virtual RuleInsuranceGroup RuleInsuranceGroup { get; set; } = null!;

        [Display(Name = "شغل")]
        public int JobId { get; set; }
        public Job Job { get; set; }

        public ICollection<PersonnelOrderDetail> Details { get; set; }
            = new List<PersonnelOrderDetail>();
    }
}