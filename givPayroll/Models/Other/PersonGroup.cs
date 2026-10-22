using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace givPayroll.Models
{
    //گروه تفصیلی
    [Table("PersonGroup", Schema = "dbo")]
    public class PersonGroup
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int PersonGroupID { get; set; }

        [Required]
        [StringLength(255)]
        [Column(TypeName = "varchar(255)")]
        public string PersonGroupName { get; set; } = string.Empty;

        public DateTime? DateCreated { get; set; }

        public int? UserCreated { get; set; }

        public DateTime? DateChanged { get; set; }

        public int? UserChanged { get; set; }

        [Required]
        public bool IsCustomer { get; set; }

        [Required]
        public bool IsVendor { get; set; }

        [Required]
        public bool IsPerson { get; set; }

        [Required]
        public bool IsBank { get; set; }

        [Required]
        public bool IsMoneyBox { get; set; }

        [Required]
        public bool IsCost { get; set; }

        [Required]
        public bool IsCostCenter { get; set; }

        [Required]
        public bool IsStaff { get; set; }

        [Required]
        public bool IsShopCustomer { get; set; }

        public DateTime? DataReceiptDate { get; set; }

        public long? MinID { get; set; }

        public long? MaxID { get; set; }
    }

}