using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace givPayroll.Models
{

    [Table("PersonCategoryLevel", Schema = "dbo")]
    public class PersonCategoryLevel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int CategoryLevel { get; set; }

        [Required]
        [StringLength(255)]
        [Column(TypeName = "varchar(255)")]
        public string CategoryLevelName { get; set; } = string.Empty;

        [Required]
        public int CategoryLevelLength { get; set; }

        public DateTime? DataReceiptDate { get; set; }
    }

}