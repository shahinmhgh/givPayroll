using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace givPayroll.Models
{
    //پرسنل
    [Table("Staff", Schema = "dbo")]
    public class Staff
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int StaffID { get; set; }

        [Required]
        [StringLength(50)]
        [Column(TypeName = "varchar(50)")]
        public string StaffType { get; set; } = string.Empty;

        [Required]
        [StringLength(255)]
        [Column(TypeName = "varchar(255)")]
        public string FirstName { get; set; } = string.Empty;

        [StringLength(255)]
        [Column(TypeName = "varchar(255)")]
        public string? LastName { get; set; }

        [StringLength(1000)]
        [Column(TypeName = "varchar(1000)")]
        public string? Address { get; set; }

        [StringLength(100)]
        [Column(TypeName = "varchar(100)")]
        public string? Tel { get; set; }

        [StringLength(50)]
        [Column(TypeName = "varchar(50)")]
        public string? Mobile { get; set; }

        public bool? IsActive { get; set; } = true;

        [StringLength(2000)]
        [Column(TypeName = "varchar(2000)")]
        public string? Note { get; set; }

        public long? PersonID { get; set; }

        public int? UserID { get; set; }

        [StringLength(1000)]
        [Column(TypeName = "varchar(1000)")]
        public string? VisibleInClientIDs { get; set; }

        public int? StaffGroupID { get; set; }

        public DateTime DateCreated { get; set; }

        public int? UserCreated { get; set; }

        public DateTime? DateChanged { get; set; }

        public int? UserChanged { get; set; }

        [Required]
        [StringLength(511)]
        [Column(TypeName = "varchar(511)")]
        public string FullName { get; set; } = string.Empty;

        public DateTime? DataReceiptDate { get; set; }

        public int? OrgPostID { get; set; }

        [StringLength(50)]
        [Column(TypeName = "varchar(50)")]
        public string? PersonnelCode { get; set; }
    }

}