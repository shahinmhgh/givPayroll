using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace givPayroll.Models
{
    [Table("PayrollAdjustment", Schema = "Payroll")]
    public class PayrollAdjustment
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Display(Name = "شناسه")]
        public int Id { get; set; }

        [Required]
        [Column(TypeName = "ntext")]
        [Display(Name = "توضیحات")]
        public string Description { get; set; } = string.Empty;

        [Required]
        [Display(Name = "تاریخ شروع")]
        public DateTime StartDate { get; set; }

        [Required]
        [Display(Name = "تاریخ پایان")]
        public DateTime EndDate { get; set; }

        [Required]
        [Display(Name = "تعداد")]
        public int AdjustmentCount { get; set; }

        [Required]
        [Display(Name = "نوع تعدیل")]
        public int AdjustmentTypeId { get; set; }

        [Required]
        [Display(Name = "وام")]
        public bool Loan { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,3)")]
        [Display(Name = "مبلغ کل")]
        public decimal TotalAmount { get; set; }

        [Required]
        public int SalaryItemId{ get; set; }

        // select * from PersonCategory 
        [MaxLength(50)]
        [Display(Name = "کد حساب")]
        public string? PersonCategoryCode { get; set; }

        // select * from person
        [Display(Name = "کد تفصیلی")]
        public int PersonID { get; set; }

        // select * from Project 
        [Display(Name = "پروژه")]
        public int ProjectId { get; set; }


        // Navigation Property

        [ForeignKey(nameof(SalaryItemId))]
        public virtual SalaryItem? SalaryItem { get; set; }

       

        public virtual ICollection<PayrollAdjustmentDetail> Details { get; set; }
                = new List<PayrollAdjustmentDetail>();

        public virtual ICollection<PayrollItem> PayrollItems { get; set; }
     = new List<PayrollItem>();

        public bool IsTaxBase { get; set; }

        public bool IsInsuranceBase { get; set; }

    }
}