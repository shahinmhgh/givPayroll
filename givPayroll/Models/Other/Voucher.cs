using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace givPayroll.Models
{
    //سند حسابداری
    [Table("Voucher", Schema = "dbo")]
    public class Voucher
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long AutoID { get; set; }

        [Required]
        public long VoucherID { get; set; }

        [Required]
        public int ClientID { get; set; } = 99;

        [Required]
        public long VoucherNo { get; set; }

        [Required]
        [StringLength(8)]
        [Column(TypeName = "char(8)")]
        public string VoucherDate { get; set; } = string.Empty;

        [Required]
        [StringLength(50)]
        [Column(TypeName = "varchar(50)")]
        public string VoucherTypeID { get; set; } = string.Empty;

        [StringLength(4000)]
        [Column(TypeName = "varchar(4000)")]
        public string? VoucherDescription { get; set; }

        public int? InitiateUserID { get; set; }

        public DateTime? InitiateDate { get; set; }

        public int? LastModifyUserID { get; set; }

        public DateTime? LastModifyDate { get; set; }

        public bool? Finalized { get; set; } = false;

        public int? FinalizedByUserID { get; set; }

        public long? VoucherRefID { get; set; }

        public int? VoucherRefClientID { get; set; }

        [StringLength(10)]
        [Column(TypeName = "varchar(10)")]
        public string? VoucherRefType { get; set; }

        public long? OldVoucherNo { get; set; }

        public DateTime? DataReceiptDate { get; set; }

        [Required]
        public int VoucherFlag { get; set; } = 0;

        public long? DailyNo { get; set; }

        public long? WeeklyNo { get; set; }

        [StringLength(8)]
        [Column(TypeName = "varchar(8)")]
        public string? WeeklyDate { get; set; }

        public long? MonthlyNo { get; set; }

        [StringLength(8)]
        [Column(TypeName = "varchar(8)")]
        public string? MonthlyDate { get; set; }
    }

}