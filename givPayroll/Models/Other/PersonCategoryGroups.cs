using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace givPayroll.Models
{
    //ارتباط مجاز معین با گروه تفصیلی
    [Table("PersonCategoryGroups", Schema = "dbo")]
    public class PersonCategoryGroups
    {
        [Key]
        [Required]
        [StringLength(50)]
        [Column(TypeName = "varchar(50)")]
        public string CategoryCode { get; set; } = string.Empty;

        [Required]
        public int PersonGroupID { get; set; }

        public DateTime? DateCreated { get; set; }

        public int? UserCreated { get; set; }

        public DateTime? DateChanged { get; set; }

        public int? UserChanged { get; set; }

        public DateTime? DataReceiptDate { get; set; }
    }

}