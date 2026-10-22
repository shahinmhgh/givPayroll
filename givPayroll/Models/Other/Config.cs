using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace givPayroll.Models
{
    //اطلاعات شرکت
    [Table("Config", Schema = "dbo")]
    public class Config
    {
        [Key]
        public int ActiveClientID { get; set; } = 99;

        public int ActiveStockID { get; set; }

        [StringLength(50)]
        public string? CompanyID { get; set; }

        [StringLength(255)]
        public string? CompanyName { get; set; }

        public int? GeneralCustomerID { get; set; }

        public int? ActivePriceListID { get; set; }

        public long? ActiveProjectID { get; set; }

        public DateTime? DataReceiptDate { get; set; }

        public int? ActiveWholeSalePriceListID { get; set; }
    }

}