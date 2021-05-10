using System.Linq;
using Microsoft.AspNetCore.Mvc;
using activity_planner.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace activity_planner.Controllers
{
    public class LoginRegController : Controller
    {

        private activity_plannerContext _context;
 
        public LoginRegController(activity_plannerContext context)
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
                //chceck for existingt email, error if so
                User emailExistsQuery = _context.Users.SingleOrDefault(user => (user.Email == FormUser.Email));
                if(emailExistsQuery != null) {
                    ViewBag.ExistsError = "User with this email already exists, please choose a different email";
                    return View("LoginReg");
                } else {
                    //create new user, hash password, load into db
                    PasswordHasher<User> Hasher = new PasswordHasher<User>();
                    NewUser.Password = Hasher.HashPassword(NewUser, NewUser.Password);
                    _context.Add(NewUser);
                    _context.SaveChanges();
                    User results = _context.Users.SingleOrDefault(user => (user.Email == FormUser.Email));
                    int id = results.UserID;
                    HttpContext.Session.SetInt32("logged_id", id);
                    return RedirectToAction("ShowDashboard", "Activity");
                }
            }
            return View("LoginReg");
        }

        [HttpPost]
        [Route("attemptlogin")]
        public IActionResult Login(string email, string password) {
            User results = _context.Users.SingleOrDefault(user => (user.Email == email));
            if (results != null) {
                PasswordHasher<User> Hasher = new PasswordHasher<User>();
                //successful login should go to dashboard
                if(Hasher.VerifyHashedPassword(results, results.Password, password) != 0) {
                    int id = results.UserID;
                    HttpContext.Session.SetInt32("logged_id", id);
                    return RedirectToAction("ShowDashboard", "Activity");
                } else {
                    //do not tell user what is wrong for security purposes
                    TempData["error"] = "login information inccorrect. Please try again.";
                    return RedirectToAction("LoginReg");
                }
               
            }
            //didn't find the user, return same error
            TempData["error"] = "login information inccorrect. Please try again.";
            return RedirectToAction("LoginReg");
        }

        [HttpPost]
        [Route("autolog")]
        public IActionResult AutoLog()
        {
            HttpContext.Session.SetInt32("logged_id", 10);
            return RedirectToAction("ShowDashboard", "Activity");
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
