using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace givPayroll.Models
{
    //پروژه
    [Table("Project", Schema = "dbo")]
    public class Project
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long ProjectID { get; set; }

        [Required]
        [StringLength(1000)]
        public string ProjectName { get; set; } = string.Empty;

        [StringLength(8)]
        [Column(TypeName = "char(8)")]
        public string? StartDate { get; set; }

        [StringLength(8)]
        [Column(TypeName = "char(8)")]
        public string? EndDate { get; set; }

        public int? ManagerUserID { get; set; }

        [StringLength(1000)]
        public string? Manager { get; set; }

        [Column(TypeName = "varchar(max)")]
        public string? ProjectDesc { get; set; }

        public DateTime DateCreated { get; set; }

        public int? UserCreated { get; set; }

        public DateTime? DateChanged { get; set; }

        public int? UserChanged { get; set; }

        public DateTime? DataReceiptDate { get; set; }
    }

}