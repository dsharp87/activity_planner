using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using belt1.Models;
using Microsoft.AspNetCore.Http;

namespace belt1.Controllers
{
    public class LoginRegController : Controller
    {

        private Belt1Context _context;
 
        public LoginRegController(Belt1Context context)
        {
            _context = context;
        }
        
        [HttpGet]
        [Route("")]
        public IActionResult LoginReg()
        {
            if (TempData["error"] != null) {
                ViewBag.Error = TempData["error"];
            }
            return View("LoginReg");
        }

        [HttpPost]
        [Route("register")]
        public IActionResult Register(UserViewModel FormUser) {
            if(ModelState.IsValid) {
                User NewUser = new User {
                    FirstName = FormUser.FirstName,
                    LastName = FormUser.LastName,
                    Email = FormUser.Email,
                    Password = FormUser.Password
                };
                _context.Add(NewUser);
                _context.SaveChanges();
                User results = _context.Users.SingleOrDefault(user => (user.Email == FormUser.Email && user.Password == FormUser.Password));
                int id = results.UserID;
                HttpContext.Session.SetInt32("logged_id", id);
                return RedirectToAction("ShowDashboard", "Activity");
            }
            return View("LoginReg");
        }

        [HttpPost]
        [Route("attemptlogin")]
        public IActionResult Login(string email, string password) {
            User results = _context.Users.SingleOrDefault(user => (user.Email == email && user.Password == password));
            if (results != null) {
                int id = results.UserID;
                HttpContext.Session.SetInt32("logged_id", id);
                return RedirectToAction("ShowDashboard", "Activity");
            }
            TempData["error"] = "login information inccorrect. Please try again.";
            return RedirectToAction("LoginReg");
        }

        

        public IActionResult Error()
        {
            return View();
        }
    }
}
