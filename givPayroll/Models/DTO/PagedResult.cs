using Microsoft.AspNetCore.Mvc.Rendering;

namespace givPayroll.Models
{
    public class PagedResult<T>
    {
        public IEnumerable<T> Items { get; set; } = new List<T>();

        public int PageNumber { get; set; }

        public int PageSize { get; set; }

        public List<SelectListItem> PageSizes { get; set; } = new();

        public int TotalPages { get; set; }

        public int TotalRecords { get; set; }
    }
}
