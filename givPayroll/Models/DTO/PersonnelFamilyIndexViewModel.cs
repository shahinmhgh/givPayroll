namespace givPayroll.Models
{
    public class PersonnelFamilyIndexViewModel
    {
        public Personnel Personnel { get; set; } = null!;

        public List<PersonnelFamily> Items { get; set; }
            = new();
    }
}