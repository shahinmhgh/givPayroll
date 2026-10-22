using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace givPayroll.Models
{
    //ردیفهای سند حسابداری
    [Table("VoucherRow", Schema = "dbo")]
    public class VoucherRow
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long AutoID { get; set; }

        [Required]
        public long VoucherID { get; set; }

        [Required]
        public int ClientID { get; set; } = 99;

        [Required]
        [StringLength(50)]
        [Column(TypeName = "varchar(50)")]
        public string CategoryCode { get; set; } = string.Empty;

        public long? PersonID { get; set; }

        public long? ProjectID { get; set; }

        [StringLength(4000)]
        [Column(TypeName = "varchar(4000)")]
        public string? Description { get; set; }

        [Column(TypeName = "decimal(21,3)")]
        public decimal? Debit { get; set; }

        [Column(TypeName = "decimal(21,3)")]
        public decimal? Credit { get; set; }

        public int? InitiateUserID { get; set; }

        public DateTime? InitiateDate { get; set; }

        public int? LastModifyUserID { get; set; }

        public DateTime? LastModifyDate { get; set; }

        public long? VoucherRowRefID { get; set; }

        public int? VoucherRowRefClientID { get; set; }

        [StringLength(10)]
        [Column(TypeName = "varchar(10)")]
        public string? VoucherRowRefType { get; set; }

        [StringLength(8)]
        [Column(TypeName = "varchar(8)")]
        public string? DateValue { get; set; }

        [StringLength(50)]
        [Column(TypeName = "varchar(50)")]
        public string? NumberValue { get; set; }

        [Column(TypeName = "decimal(18,3)")]
        public decimal? AmountValue { get; set; }

        public DateTime? DataReceiptDate { get; set; }
    }

}