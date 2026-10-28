using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace givPayroll.Models
{
    [Table("User", Schema = "dbo")]
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int UserID { get; set; }

        [Required]
        [StringLength(50)]
        public string UserCode { get; set; } = string.Empty;

        [Required]
        [StringLength(50)]
        public string UserFirstName { get; set; } = string.Empty;

        [Required]
        [StringLength(255)]
        public string UserLastName { get; set; } = string.Empty;

        [Required]
        [StringLength(306)]
        public string UserFullName { get; set; } = string.Empty;

        [Required]
        [StringLength(255)]
        public string UserPassword { get; set; } = "123456";

        public int UserRoleID { get; set; } = 0;

        public bool UserIsActive { get; set; } = true;

        public DateTime? DataReceiptDate { get; set; }

        [StringLength(1000)]
        public string? VisibleInClientIDs { get; set; }

        public DateTime DateCreated { get; set; }

        public int? UserCreated { get; set; }

        public DateTime? DateChanged { get; set; }

        public int? UserChanged { get; set; }

        [StringLength(1000)]
        public string? AccessibleClientIDs { get; set; }

        public long? ProjectID { get; set; }

        public int? PriceListID { get; set; }

        public int DataAccessLevel { get; set; }
    }
}