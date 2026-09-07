using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace givPayroll.Models
{
    [Table("Parameters", Schema = "ref")]
    public class Parameter
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Display(Name = "کد")]
        public int Id { get; set; }


        [Required(ErrorMessage = "موضوع الزامی است.")]
        [MaxLength(50)]
        [Display(Name = "موضوع")]
        public string Subject { get; set; } = string.Empty;


        [Required(ErrorMessage = "کلید گزینه الزامی است.")]
        [MaxLength(50)]
        [Display(Name = "کلید گزینه")]
        public string OptionKey { get; set; } = string.Empty;


        [Required(ErrorMessage = "نام گزینه الزامی است.")]
        [MaxLength(50)]
        [Display(Name = "نام گزینه")]
        public string OptionName { get; set; } = string.Empty;


        [Required(ErrorMessage = "مقدار گزینه الزامی است.")]
        [MaxLength(50)]
        [Display(Name = "مقدار گزینه")]
        public string OptionValue { get; set; } = string.Empty;
    }
}