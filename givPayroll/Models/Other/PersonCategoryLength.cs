using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace givPayroll.Models
{

    [Table("PersonCategoryLength", Schema = "dbo")]
    public class PersonCategoryLength
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int CategoryLevel { get; set; }

        public int? SumLevelLength { get; set; }
    }

}