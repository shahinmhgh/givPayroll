using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace givPayroll.Models
{
    [Table("Attendance", Schema = "Payroll")]
    public class Attendance
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Display(Name = "کد")]
        public int Id { get; set; }

        [Required]
        [MaxLength(10)]
        [Display(Name = "تاریخ")]
        public string AttendanceDate { get; set; } = string.Empty;

        [Required]
        [Display(Name = "پرسنل")]
        public int PersonnelId { get; set; }

        [Display(Name = "موظفی")]
        public int WorkingExpectedMinute { get; set; }

        [Display(Name = "کارکرد")]
        public int WorkingMinute { get; set; }

        [Display(Name = "اضافه کار")]
        public int ExtraMinute { get; set; }

        [Display(Name = "تعطیل کاری")]
        public int HolidayMinute { get; set; }

        [Display(Name = "تاخیر")]
        public int DelayMinute { get; set; }

        [Display(Name = "تعجیل")]
        public int EarlyArrivalMinute { get; set; }

        [Display(Name = "مرخصی عادی")]
        public int LeaveNormalMinute { get; set; }

        [Display(Name = "مرخصی بدون حقوق")]
        public int LeaveWithoutSalaryMinute { get; set; }

        [Display(Name = "مرخصی استعلاجی")]
        public int LeaveSickMinute { get; set; }

        [Display(Name = "غیبت")]
        public int AbsenceMinute { get; set; }

        [Display(Name = "ماموریت")]
        public int MissionMinute { get; set; }

        [Display(Name = "وضعیت")]
        public int Status { get; set; }

        [Display(Name = "کارکرده")]
        public bool HasWorked { get; set; } = true;


        // Navigation
        [ForeignKey(nameof(PersonnelId))]
        public virtual Personnel? Personnel { get; set; }
    }
}