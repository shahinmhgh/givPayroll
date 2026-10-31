using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace givPayroll.Models
{
    [Keyless]
    public class VoucherModel
    {
        public int PayrollYear { get; set; }

        public int PayrollMonth { get; set; }

        public int PersonnelId { get; set; }

        public int? ProjectId { get; set; }

        [StringLength(50)]
        public string? PersonCategoryCode { get; set; }

        public int? PersonID { get; set; }

        [Required]
        [StringLength(50)]
        public string SalaryItemName { get; set; } = string.Empty;

        public int SalaryItemId { get; set; }

        public int PlusMinus { get; set; }

        [Column(TypeName = "decimal(18,3)")]
        public decimal? Credit { get; set; }

        [Column(TypeName = "decimal(18,3)")]
        public decimal? Debit { get; set; }
    }
}