using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using givPayroll.Models;
using Microsoft.AspNetCore.Identity;

namespace givPayroll.Data
{
    
    public class ApplicationUser : IdentityUser
    {
         public string? FullName { get; set; }
    }

}
