using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace givPayroll.Models
{
    [Table("PersonnelFamily", Schema = "Payroll")]
    public class PersonnelFamily
    {


        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Key]
        [Display(Name = "کد")]
        public int Id { get; set; }

        [Required]
        public int PersonnelId { get; set; }

        [Required]
        public int PersonnelRelationId { get; set; }

        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        [MaxLength(50)]
        public string LastName { get; set; } = string.Empty;

        [MaxLength(11)]
        public string? NationalCode { get; set; }

        [Display(Name = "تاریخ تولد")]
        [DataType(DataType.Date)]
        public DateTime? BirthDate { get; set; }


        public int GenderID { get; set; }

        [MaxLength(10)]
        public string? IdNo { get; set; }

        [MaxLength(50)]
        public string? IssuePlace { get; set; }

        public int MaritalStatusId { get; set; }
        [ForeignKey(nameof(MaritalStatusId))]
        public virtual MaritalStatus MaritalStatus { get; set; } = null!;

        public DateTime DateCreated { get; set; }

        public String UserCreated { get; set; }

        public DateTime? DateChanged { get; set; }

        public String? UserChanged { get; set; }


        // Navigation Properties

        [ForeignKey(nameof(PersonnelId))]
        public virtual Personnel Personnel { get; set; } = null!;

        [ForeignKey(nameof(PersonnelRelationId))]
        public virtual FamilyRelation FamilyRelation { get; set; } = null!;

        [ForeignKey(nameof(GenderID))]
        public virtual Gender Gender { get; set; } = null!;

  
    }
}