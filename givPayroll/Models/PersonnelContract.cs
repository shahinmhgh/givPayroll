using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace givPayroll.Models
{


    [Table("PersonnelContract")]
    public class PersonnelContract
    {
        [Display(Name = "فعال")]
        public bool IsActive { get; set; }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Display(Name = "کد")]
        public int Id { get; set; }


        [Required(ErrorMessage = "پرسنل الزامی است.")]
        [Display(Name = "پرسنل")]
        public int PersonnelId { get; set; }

        [Required(ErrorMessage = "پرسنل الزامی است.")]
        [Display(Name = "کد حکم پرسنلی")]
        public int PersonnelOrderId { get; set; }

        [Column(TypeName = "ntext")]
        [Display(Name = "توضیحات")]
        public string? Description { get; set; }


        [Display(Name = "تاریخ قراداد")]
        [DataType(DataType.Date)]
        public DateTime? ContractDate { get; set; }


        [Required(ErrorMessage = "نوع قرارداد الزامی است.")]
        [Display(Name = "نوع قرارداد")]
        public int ContractTypeId { get; set; }


        [Required(ErrorMessage = "تاریخ شروع الزامی است.")]
        [Display(Name = "تاریخ شروع")]
        [DataType(DataType.Date)]
        public DateTime? StartDate { get; set; }


        [Required(ErrorMessage = "تاریخ پایان الزامی است.")]
        [Display(Name = "تاریخ پایان")]
        [DataType(DataType.Date)]
        public DateTime? EndDate { get; set; }


        [Required(ErrorMessage = "وضعیت قرارداد الزامی است.")]
        [Display(Name = "وضعیت")]
        public int ContractStatusId { get; set; }


        //[MaxLength(200)]
        //[Display(Name = "عنوان شغلی")]
        //public string? JobTitle { get; set; }


        //[MaxLength(50)]
        //[Display(Name = "کد استاندارد شغل")]
        //public string? JobStandardCode { get; set; }


        //[Display(Name = "شرح شغل")]
        //public string? JobDescription { get; set; }


        // Navigation properties
        [ForeignKey(nameof(ContractStatusId))]
        public ContractStatus? ContractStatus  { get; set; }

        [ForeignKey(nameof(PersonnelOrderId))]
        public PersonnelOrder PersonnelOrder { get; set; }


        [ForeignKey(nameof(PersonnelId))]
        public Personnel? Personnel { get; set; }


        [ForeignKey(nameof(ContractTypeId))]
        public ContractType? ContractType { get; set; }

       
    }
}