
using givPayroll.Data;
using Microsoft.AspNetCore.Identity;

namespace givPayroll.ViewModels
{
    public class IdentityUserListViewModel
    {
        public List<ApplicationUser> Users { get; set; } = new();

        public string SearchString { get; set; } = "";
        public string SortColumn { get; set; } = "UserName";
        public string SortDirection { get; set; } = "asc";

        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public int TotalPages { get; set; }
        public int TotalRecords { get; set; }
    }
}
