using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace givPayroll.Models
{
    public class PersonnelOrderDetail
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Display(Name = "شناسه")]
        public int Id { get; set; }

        [Required]
        [Display(Name = "حکم")]
        public int PersonnelOrderId { get; set; }

        [Required]
        [Display(Name = "قلم حقوق")]
        public int SalaryItemId { get; set; }

        [Column(TypeName = "decimal(18,3)")]
        [Display(Name = "مبلغ")]
        public decimal Amount { get; set; }

        [Display(Name = "تاریخ ایجاد")]
        public DateTime DateCreated { get; set; }

        [Display(Name = "کاربر ایجادکننده")]
        public String UserCreated { get; set; }

        [Display(Name = "تاریخ تغییر")]
        public DateTime? DateChanged { get; set; }

        [Display(Name = "کاربر تغییر دهنده")]
        public String? UserChanged { get; set; }


        // Navigation

        public PersonnelOrder? PersonnelOrder { get; set; }

        public SalaryItem? SalaryItem { get; set; }
    }
}