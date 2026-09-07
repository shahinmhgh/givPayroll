using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using givPayroll.Data;
using givPayroll.Models;

namespace givPayroll.Controllers
{
    

    public class HomeController : Controller
    {
        private UserManager<ApplicationUser> _userManager;

        public HomeController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;


        }

        public async Task<IActionResult> Index()
        {
            //var username = _userManager.GetUserName(User);
            //var user = await  _userManager.GetUserAsync(User);

            //var email = user.Email;
            //var dbUserName = user.UserName;

            return View();
        }

      

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
