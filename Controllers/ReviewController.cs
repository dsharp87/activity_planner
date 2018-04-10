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
    public class ReviewController : Controller
    {

        private activity_plannerContext _context;
 
        public ReviewController(activity_plannerContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("ReviewForm/{ActivityID}")]
        public IActionResult ReviewForm(int ActivityID) {
            if (HttpContext.Session.GetInt32("logged_id") == null) {
                return RedirectToAction("LoginReg", "LoginReg");
            }
            ViewBag.LoggedUser = _context.Users.Include(user => user.AttendingActivities).ThenInclude(ua => ua.Activity).SingleOrDefault(user => (user.UserID == HttpContext.Session.GetInt32("logged_id")));
            ViewBag.Activity = _context.Activities.Include(activity => activity.Creator).Include(activity => activity.UsersAttending).ThenInclude(ua => ua.User).SingleOrDefault(activity => activity.ActivityID == ActivityID);
            return View("ReviewForm");
        }

        [HttpPost]
        [Route("AddReview")]
        public IActionResult AddReview(int ActivityID, ReviewViewModel review) {
            if(ModelState.IsValid) {
                Review reviewToAdd = new Review {
                    Title = review.Title,
                    Rating = review.Rating,
                    Description = review.Description,
                    ActivityID = ActivityID,
                    ReviewerID = (int)HttpContext.Session.GetInt32("logged_id")
                };
                _context.Reviews.Add(reviewToAdd);
                _context.SaveChanges();
                return RedirectToAction("ShowActivity", "Activity", new {ActivityID = ActivityID});
            }
            ViewBag.Activity = _context.Activities.Include(activity => activity.Creator).Include(activity => activity.UsersAttending).ThenInclude(ua => ua.User).SingleOrDefault(activity => activity.ActivityID == ActivityID);
            return View("ReviewForm");
        }

        [HttpGet]
        [Route("ShowReviews/{ActivityID}")]
        public IActionResult ShowReviews(int ActivityID) {
            if (HttpContext.Session.GetInt32("logged_id") == null) {
                return RedirectToAction("LoginReg", "LoginReg");
            }
            ViewBag.Activity = _context.Activities.Include(activity => activity.Creator).Include(activity => activity.UsersAttending).ThenInclude(ua => ua.User).Include(activity => activity.Reviews).ThenInclude(review => review.Reviewer).SingleOrDefault(activity => activity.ActivityID == ActivityID);
            return View("ShowReviews");
        }

    }

}