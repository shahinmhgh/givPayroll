using givPayroll.Data;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace givPayroll.Models
{
   
    
    //حساب های وابسته : گروه، کل و معین
    [Table("PersonCategory", Schema = "dbo")]
    public class PersonCategory
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Required]
        [StringLength(50)]
        [Column(TypeName = "varchar(50)")]
        public string CategoryCode { get; set; } = string.Empty;

        [Required]
        [StringLength(255)]
        [Column(TypeName = "varchar(255)")]
        public string CategoryName { get; set; } = string.Empty;

        [Required]
        public bool CategoryIsActive { get; set; } = true;

        [StringLength(50)]
        [Column(TypeName = "varchar(50)")]
        public string? CategoryGroupCode { get; set; }

        public DateTime DateCreated { get; set; }

        [Required]
        public int UserCreated { get; set; } = 0;

        public DateTime? DateChanged { get; set; }

        public int? UserChanged { get; set; }

        [Required]
        [StringLength(10)]
        [Column(TypeName = "varchar(10)")]
        public string AccountNature { get; set; } = "D";

        [Required]
        [StringLength(10)]
        [Column(TypeName = "varchar(10)")]
        public string AccountType { get; set; } = "BLC";

        [Required]
        public bool HasUniqueAccount { get; set; }

        [Required]
        public bool HasSubsideryAccount { get; set; }

        public DateTime? DataReceiptDate { get; set; }

       
    }

}