using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using activity_planner.Models;
using Microsoft.AspNetCore.Http;

namespace activity_planner.Controllers
{
    public class ActivityController : Controller
    {
        
        private activity_plannerContext _context;
 
        public ActivityController(activity_plannerContext context)
        {
            _context = context;
        }
        
        [HttpGet]
        [Route("Dashboard")]
        public IActionResult ShowDashboard()
        {
            //check for logged in
            int? loggedId = HttpContext.Session.GetInt32("logged_id");
            if (HttpContext.Session.GetInt32("logged_id") == null) {
                return RedirectToAction("LoginReg", "LoginReg");
            }
            //grab all activites
            ViewBag.LoggedUser = _context.Users.Include(user => user.AttendingActivities).ThenInclude(ua => ua.Activity).SingleOrDefault(user => (user.UserID == HttpContext.Session.GetInt32("logged_id")));
            ViewBag.AllFutureActivities = _context.Activities.Where(activity => activity.StartTime > DateTime.Now).Include(activity => activity.UsersAttending).Include(activity => activity.Creator).OrderBy(activity=> activity.StartTime).ToList();
            ViewBag.PastActivities = _context.Activities.Where(activity => activity.StartTime < DateTime.Now).Include(activity => activity.UsersAttending).Include(activity => activity.Creator).Include(activity => activity.Reviews).OrderByDescending(activity=> activity.StartTime).ToList(); 
            return View("Dashboard");
        }


        [HttpGet]
        [Route("AddActivityForm")]
        public IActionResult AddActivityForm() {
            //check for logged in
            if (HttpContext.Session.GetInt32("logged_id") == null) {
                return RedirectToAction("LoginReg", "LoginReg");
            }
            return View("AddActivityForm");
        }


        //NEED LOGIC TO NOT ALLOW YOU TO CREATE AN ACTIVITY IF THAT BOUNDS ONE YOU ALREADY HAVE GOING OR ARE PARTICIPATING IN??
        [HttpPost]
        [Route("CreateActivity")]
        public IActionResult CreateActivity(ActivityViewModel NewActivityViewModel, string TimeDenomination, TimeSpan StartTime) {
            ViewBag.OverlapError = "";
            if(ModelState.IsValid) {
                int LoggedID = (int)HttpContext.Session.GetInt32("logged_id");
                //convert the denomination into the appropriate number of minutes to be stored in db
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
                    StreetAddress = NewActivityViewModel.StreetAddress,
                    City = NewActivityViewModel.City,
                    State = NewActivityViewModel.State,
                    ZipCode = NewActivityViewModel.ZipCode,
                    CreatorID = LoggedID,
                    Duration = (int)ActivityDuration.TotalMinutes,
                    StartTime = NewActivityViewModel.StartDate + StartTime
                };
                //query db for all actvivities made by this person
                List<Activity> CreatedActivities = _context.Activities.Where(activity => activity.CreatorID == LoggedID).ToList();
                bool overlapFlag = false;
                Activity overlappingActivity = new Activity();
                DateTime NewActivityStartTime = NewActivity.StartTime;
                DateTime NewActivityEndTime = NewActivity.StartTime + new TimeSpan(0, NewActivity.Duration, 0);
                //loop finds overlap if it exists
                foreach (Activity activity in CreatedActivities) {
                        DateTime ThisActivityStart = activity.StartTime;
                        DateTime ThisActivityEnd = activity.StartTime + new TimeSpan(0, activity.Duration, 0);
                        if(NewActivityStartTime < ThisActivityEnd && ThisActivityStart < NewActivityEndTime) {
                            overlapFlag = true;
                            overlappingActivity = activity;
                            break;
                        } 
                }
                if (overlapFlag) {
                    ViewBag.OverlapError = $"This activty would overlap with your upcoming activity: '{overlappingActivity.Name}', which begins {overlappingActivity.StartTime} and ends at {overlappingActivity.StartTime + new TimeSpan(0, overlappingActivity.Duration, 0)}. Please chose a different time or cancel the conflicting activity first.";
                    return View("AddActivityForm");
                }
                _context.Activities.Add(NewActivity);
                _context.SaveChanges();
                //there is where logic for overlap of joined activities goes *******

                //it will need to find all useractivites that this user is part of, then see if any overlap, if they do, add them to a list
                // somehow this list needs to be passed to showactivity page, maybe json into session?

                // ***********
                Activity SavedActivity = _context.Activities.SingleOrDefault(activity => activity.Name == NewActivityViewModel.Name && activity.CreatorID == LoggedID);
                UserActivity NewUserActivity = new UserActivity() {
                UserID = (int)HttpContext.Session.GetInt32("logged_id"),
                ActivityID = SavedActivity.ActivityID
                };
                _context.UserActivities.Add(NewUserActivity);
                _context.SaveChanges();
                return RedirectToAction("ShowActivity", new {ActivityID = SavedActivity.ActivityID});
            }
            return View("AddActivityForm");
        }

        [HttpPost]
        [Route("DeleteActivity")]
        public IActionResult DeleteActivity(int ActivityID) {
            //find and delete activity by id
            Activity DeleteMe = _context.Activities.SingleOrDefault(activity => activity.ActivityID == ActivityID);
            _context.Activities.Remove(DeleteMe);
            _context.SaveChanges();
            return RedirectToAction("ShowDashboard");
        }

        [HttpPost]
        [Route("JoinActivity")]
        public IActionResult JoinActivity(int ActivityID) {
            //create many to many entry and add to db
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
            //delete many to many for this user/activity combo
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
            
            //logic here to check session for overlapping joined activities ***********
            // instantiate empty viewbag list so html won't break if no overlapp

            // will need to check if something is in overlapp session.  If so grab it and put it in viewbag., then clear that particular session value OR set it to null?

            //   *************
            Activity a = _context.Activities.Include(activity => activity.Creator).Include(activity => activity.UsersAttending).ThenInclude(ua => ua.User).SingleOrDefault(activity => activity.ActivityID == ActivityID);
            ActivityViewModel viewModel = ActivityViewModel.GetActivityViewModel(a);
            ViewBag.LoggedUser = _context.Users.Include(user => user.AttendingActivities).ThenInclude(ua => ua.Activity).SingleOrDefault(user => (user.UserID == HttpContext.Session.GetInt32("logged_id")));
            ViewBag.Activity = a;
            return View("ShowActivity", viewModel);
        }

        [HttpPost]
        [Route("Logout")]
        public IActionResult Logout() {
            HttpContext.Session.Clear();
            return RedirectToAction("LoginReg", "LoginReg");
        }

        

    }
}