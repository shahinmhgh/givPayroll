using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace givPayroll.Models
{
    [Keyless]
    public class VoucherModel
    {
        public int VoucherID { get; set; }

        public int ClientID { get; set; }

        public string CategoryCode { get; set; } = string.Empty;

        public long PersonID { get; set; }

        public int ProjectID { get; set; }

        public string Description { get; set; } = string.Empty;

        public decimal debit { get; set; }

        public decimal credit { get; set; }

        public int InitiateUserID { get; set; }

        public DateTime InitiateDate { get; set; }
    }
}