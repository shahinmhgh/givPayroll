using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace givPayroll.Models
{
    //تفصیلی
    [Table("Person", Schema = "dbo")]
    public class Person
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long PersonID { get; set; }

        [Required]
        public int PersonTypeID { get; set; }

        [Required]
        [StringLength(50)]
        [Column(TypeName = "varchar(50)")]
        public string PersonCategoryCode { get; set; } = string.Empty;

        [StringLength(255)]
        [Column(TypeName = "varchar(255)")]
        public string? PersonFirstName { get; set; }

        [StringLength(255)]
        [Column(TypeName = "varchar(255)")]
        public string? PersonLastName { get; set; }

        [StringLength(1000)]
        [Column(TypeName = "varchar(1000)")]
        public string? PersonAddress { get; set; }

        [StringLength(100)]
        [Column(TypeName = "varchar(100)")]
        public string? PersonTel { get; set; }

        [StringLength(50)]
        [Column(TypeName = "varchar(50)")]
        public string? PersonMobile { get; set; }

        [StringLength(50)]
        [Column(TypeName = "varchar(50)")]
        public string? PersonFax { get; set; }

        [Required]
        public bool PersonIsActive { get; set; } = true;

        [StringLength(2000)]
        [Column(TypeName = "varchar(2000)")]
        public string? PersonNote { get; set; }

        public int? PriceListID { get; set; }

        [StringLength(50)]
        [Column(TypeName = "varchar(50)")]
        public string? CompanyID { get; set; }

        public int? DiscountListID { get; set; }

        public DateTime DateCreated { get; set; }

        [Required]
        public int UserCreated { get; set; } = 0;

        public DateTime? DateChanged { get; set; }

        public int? UserChanged { get; set; }

        [Required]
        [StringLength(511)]
        [Column(TypeName = "varchar(511)")]
        public string PersonFullName { get; set; } = string.Empty;

        [Required]
        [StringLength(50)]
        [Column(TypeName = "varchar(50)")]
        public string DiscountListUsage { get; set; } = string.Empty;

        [Required]
        public int PersonGroupID { get; set; } = 1;

        public DateTime? DataReceiptDate { get; set; }

        public long? OldPersonID { get; set; }

        public long? NewPersonID { get; set; }

        public long? OriginPersonID { get; set; }

        public long? ParentPersonID { get; set; }
    }

}