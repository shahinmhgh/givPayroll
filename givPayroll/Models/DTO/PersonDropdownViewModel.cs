using Microsoft.AspNetCore.Mvc.Rendering;

namespace givPayroll.Models
{
    public class PersonDropdownViewModel
    {
        public int? SelectedPersonID { get; set; }

        public List<PersonDropdownItem> Persons { get; set; } = new();
    }

    public class PersonDropdownItem
    {
        public Int64 PersonID { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
    }
}