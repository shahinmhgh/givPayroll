using Microsoft.AspNetCore.Mvc.Rendering;

namespace givPayroll.Models
{
    //public class PersonDropdownViewModel
    //{
    //    public int? SelectedPersonID { get; set; }

    //    public List<PersonDropdownItem> Persons { get; set; } = new();
    //}

    //public class PersonDropdownItem
    //{
    //    public Int64 PersonID { get; set; }
    //    public string? FirstName { get; set; }
    //    public string? LastName { get; set; }
    //}

    public class PersonDropdownViewModel
    {
        public string? SelectedCategoryCode { get; set; }
        public long? SelectedPersonID { get; set; }
        public List<MoinPersonGroupDropdownItem> ItemsMoin { get; set; } = new();
        public List<TafsilPersonGroupDropdownItem> ItemsTafsil { get; set; } = new();
    }

    public class MoinPersonGroupDropdownItem
    {
        //public string? CategoryName { get; set; }
        public string? CategoryFullName { get; set; }
        public string? CategoryCode { get; set; }
    }

    public class TafsilPersonGroupDropdownItem
    {
        public string? PersonFullName { get; set; }
        public string? PersonGroupName { get; set; }
        public long PersonID { get; set; }
    }

}