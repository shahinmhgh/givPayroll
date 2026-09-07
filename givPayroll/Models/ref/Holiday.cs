

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace givPayroll.Models
{
    [Table("Holiday", Schema = "ref")]
    public class Holiday
    {
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Persian year, for example 1405
        /// </summary>
        [Required]
        [Display(Name = "سال")]
        public int Year { get; set; }

        [Required]
        [Display(Name = "تاریخ شمسی")]
        public string PersianDate { get; set; }

        /// <summary>
        /// Gregorian date used internally by the payroll system
        /// </summary>
        [Required]
        [Display(Name = "تاریخ")]
        public DateTime Date { get; set; }

        [Required]
        [MaxLength(200)]
        [Display(Name = "عنوان")]
        public string Title { get; set; } = string.Empty;

        [Display(Name = "تعطیل رسمی")]
        public bool IsOfficial { get; set; } = true;

        [MaxLength(500)]
        [Display(Name = "توضیحات")]
        public string? Description { get; set; }
    }
}
