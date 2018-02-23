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
    public class ActivityController : Controller
    {

        private Belt1Context _context;
 
        public ActivityController(Belt1Context context)
        {
            _context = context;
        }
        
        [HttpGet]
        [Route("Dashboard")]
        public IActionResult ShowDashboard()
        {
            if (HttpContext.Session.GetInt32("logged_id") == null) {
                return RedirectToAction("LoginReg", "LoginReg");
            }
            ViewBag.LoggedUser = _context.Users.Include(user => user.AttendingActivities).ThenInclude(ua => ua.Activity).SingleOrDefault(user => (user.UserID == HttpContext.Session.GetInt32("logged_id")));
            ViewBag.AllActivities = _context.Activities.Where(activity => activity.StartTime > DateTime.Now).Include(activity => activity.UsersAttending).Include(activity => activity.Creator).OrderByDescending(activity=> activity.CreatedAt).ToList();
            return View("Dashboard");
        }


        [HttpGet]
        [Route("AddActivityForm")]
        public IActionResult AddActivityForm() {
            if (HttpContext.Session.GetInt32("logged_id") == null) {
                return RedirectToAction("LoginReg", "LoginReg");
            }
            return View("AddActivityForm");
        }

        [HttpPost]
        [Route("CreateActivity")]
        public IActionResult CreateActivity(ActivityViewModel NewActivityViewModel, string TimeDenomination, TimeSpan StartTime) {
            if(ModelState.IsValid) {
                int LoggedID = (int)HttpContext.Session.GetInt32("logged_id");
                    TimeSpan ActivityDuration = new TimeSpan(0, 0, 0);
                if (TimeDenomination == "Days") {
                    ActivityDuration += new TimeSpan(24*NewActivityViewModel.Duration, 0, 0);
                } else if (TimeDenomination == "Hours") {
                    ActivityDuration += new TimeSpan(1*NewActivityViewModel.Duration, 0, 0);
                } else {
                    ActivityDuration += new TimeSpan(0, 1*NewActivityViewModel.Duration, 0);
                }
                Activity NewActivity = new Activity() {
                    Name = NewActivityViewModel.Name,
                    Description = NewActivityViewModel.Description,
                    CreatorID = LoggedID,
                    Duration = (int)ActivityDuration.TotalMinutes,
                    StartTime = NewActivityViewModel.StartDate + StartTime
                };
                _context.Activities.Add(NewActivity);
                _context.SaveChanges();
                Activity SavedActivity = _context.Activities.SingleOrDefault(activity => activity.Name == NewActivityViewModel.Name && activity.CreatorID == LoggedID);
                return RedirectToAction("ShowActivity", new {ActivityID = SavedActivity.ActivityID});
            }
            return View("AddActivityForm");
        }

        [HttpPost]
        [Route("DeleteActivity")]
        public IActionResult DeleteActivity(int ActivityID) {
            Activity DeleteMe = _context.Activities.SingleOrDefault(activity => activity.ActivityID == ActivityID);
            _context.Activities.Remove(DeleteMe);
            _context.SaveChanges();
            return RedirectToAction("ShowDashboard");
        }

        [HttpPost]
        [Route("JoinActivity")]
        public IActionResult JoinActivity(int ActivityID) {
            UserActivity NewUserActivity = new UserActivity() {
                UserID = (int)HttpContext.Session.GetInt32("logged_id"),
                ActivityID = ActivityID
            };
            _context.UserActivities.Add(NewUserActivity);
            _context.SaveChanges();
            return RedirectToAction("ShowDashboard");
        }

        [HttpPost]
        [Route("LeaveActivity")]
        public IActionResult LeaveActivity(int ActivityID) {
            UserActivity DeleteMe =  _context.UserActivities.SingleOrDefault(ua => ua.UserID == (int)HttpContext.Session.GetInt32("logged_id") && ua.ActivityID == ActivityID);
            _context.UserActivities.Remove(DeleteMe);
            _context.SaveChanges();
            return RedirectToAction("ShowDashboard");
        }

        [HttpGet]
        [Route("ShowActivity/{ActivityID}")]
        public IActionResult ShowActivity(int ActivityID) {
            if (HttpContext.Session.GetInt32("logged_id") == null) {
                return RedirectToAction("LoginReg", "LoginReg");
            }
            ViewBag.LoggedUser = _context.Users.Include(user => user.AttendingActivities).ThenInclude(ua => ua.Activity).SingleOrDefault(user => (user.UserID == HttpContext.Session.GetInt32("logged_id")));
            ViewBag.Activity = _context.Activities.Include(activity => activity.Creator).Include(activity => activity.UsersAttending).ThenInclude(ua => ua.User).SingleOrDefault(activity => activity.ActivityID == ActivityID);
            return View("ShowActivity");
        }



        [HttpPost]
        [Route("Logout")]
        public IActionResult Logout() {
            HttpContext.Session.Clear();
            return RedirectToAction("LoginReg", "LoginReg");
        }

    }
}