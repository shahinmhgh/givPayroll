using givPayroll.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace givPayroll.Data
{
    
    public class ApplicationUser : IdentityUser
    {
         public string? FullName { get; set; }

        [Display(Name = "کاربر")]
        public int? UserID { get; set; }

        [ForeignKey(nameof(UserID))]
        public User? User { get; set; }// = null!;
    }

}
