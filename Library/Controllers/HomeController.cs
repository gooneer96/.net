using Library.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;


namespace Library.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly LibraryContext _context;

        public HomeController(LibraryContext context)
        {
            _context = context;
        }
      

        public IActionResult Index()
        {
            return View();
        }
        public ActionResult Login()
        {

            return View();

        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(User user)
        {

            bool IsValidUser = _context.Users
            .Any(u => u.Login.ToLower() == user
            .Login.ToLower() && u
            .Password == user.Password);
            ;

            if (IsValidUser)
            {
                if (HttpContext.Session.IsAvailable)
                {
                    var obj = _context.Users.Where(
                        u => u.Login == user.Login && u.Password == user.Password
                        ).FirstOrDefault();

                    HttpContext.Session.SetString("Logged", "True");
                    return RedirectToAction("Index");
                }
            }
            return View();
        }

        public ActionResult Logout()
        {
            HttpContext.Session.SetString("Logged", "False");
            return RedirectToAction("Index");
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
