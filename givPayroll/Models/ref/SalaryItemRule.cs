
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace givPayroll.Models
{
    [Table("SalaryItemRule", Schema = "ref")]
    public class SalaryItemRule
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        public int SalaryItemId { get; set; }

        public DateTime EffectiveDate { get; set; }  

        public bool IsTaxBase { get; set; }

        public bool IsInsuranceBase { get; set; }

        public decimal Amount { get; set; }

        [ForeignKey("SalaryItemId")]
        public SalaryItem? SalaryItem { get; set; }
    }
}