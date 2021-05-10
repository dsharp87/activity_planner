using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using activity_planner.UtilityClasses;
using Microsoft.AspNetCore.Html;
using Microsoft.EntityFrameworkCore;


namespace activity_planner.Models
{
    public class ActivityViewModel: BaseEntity
    {
        public int ActivityID {get; set;}
        
        [Required]
        [MinLength(2)]
        public string Name { get; set; }
        
        [FutureDate(ErrorMessage="Date should be in the future.")]
        public DateTime StartDate { get; set; }

        [Required(ErrorMessage="Specify the denomination and amount of time for this activity")]
        public int Duration { get; set; }

        [Required(ErrorMessage="An address is required")]
        [Display(Name = "Street")]
        public string StreetAddress {get; set; }

        [Required(ErrorMessage="A City is required")]
        [Display(Name = "City")]
        public string City { get; set; }

        [Required(ErrorMessage="A State")]
        [Display(Name = "State")]
        public string State { get; set; }

        [Required(ErrorMessage="A Zip Code is required")]
        [Display(Name = "Zip Code")]
        public string ZipCode { get; set; }

        [Required]
        [MinLength(10)]
        public string Description { get;  set; }

        public User Creator { get; set; }

        public List<UserActivity> UsersAttending { get; set;}

        public List<Review> Reviews { get; set;}

        public static HtmlString GetStates()
        {
            List<string> stateNames = new List<string>()
            {
            "Select State", "Alabama", "Alaska", "Arizona", "Arkansas", "California", "Colorado", "Connecticut", "Delaware", "District of Columbia", "Florida", "Georgia", "Hawaii", "Idaho", "Illinois", "Indiana", "Iowa", "Kansas", "Kentucky", "Louisiana", "Maine", "Maryland", "Massachusetts", "Michigan", "Minnesota", "Mississippi", "Missouri", "Montana", "Nebraska", "Nevada", "New Hampshire", "New Jersey", "New Mexico", "New York", "North Carolina", "North Dakota", "Ohio", "Oklahoma", "Oregon", "Pennsylvania", "Rhode Island", "South Carolina", "South Dakota", "Tennessee", "Texas", "Utah", "Vermont", "Virginia", "Washington", "West Virginia", "Wisconsin", "Wyoming", "American Samoa", "Guam", "Marshall Islands", "Micronesia", "Northern Mariana Islands", "Palau", "Puerto Rico", "Virgin Islands"
            };
            List<string> abbreviations = new List<string>()
            {
                "null", "AL", "AK", "AZ", "AR", "CA", "CO", "CT", "DE", "DC", "FL", "GA", "HI", "ID", "IL", "IA", "IN", "KS", "KY", "LA", "ME", "MD", "MA", "MI", "MN", "MS", "MO", "MT", "NE", "NV", "NH", "NJ", "NM", "NY", "NC", "ND", "OH", "OK", "OR", "PA", "RI", "SC", "SD", "TN", "TX", "UT", "VT", "VA", "WA", "WV", "WI", "WY", "AS", "GU", "MH", "FM", "MP", "PW", "PR", "VI"
            };
            string stringVals = String.Empty;
            for(int i = 0; i < stateNames.Count; i++)
            {
                stringVals += "<option value='" + abbreviations[i] + "'>" + stateNames[i] + "</option>";
            }
            HtmlString options = new HtmlString(stringVals);
            return options;
        }

        public string GetMapSrcString()
        {
            string[] vals = new string[] {StreetAddress, City, State, ZipCode};

            for(int i=0; i < vals.Length; i++)
            {
                vals[i] = StreetAddress.Replace(' ', '+');
            }

            string srcString="https://www.google.com/maps/embed/v1/place?key=AIzaSyC95F71hOI6L7Zp8n-6r1Jg3fYK36RrLQo&q=";
            for(var i = 0; i < vals.Length; i++)
            {
                string subString = "";
                if(i != 0)
                {
                    subString+= ",+";
                }
                subString+= vals[i];
                srcString += subString;
            }

            return srcString;

        }

        public static ActivityViewModel GetActivityViewModel(Activity a)
        {
            ActivityViewModel viewModel = new ActivityViewModel()
            {
                ActivityID = a.ActivityID,
                Name = a.Name,
                StartDate = a.StartTime,
                Duration = a.Duration,
                StreetAddress = a.StreetAddress,
                City = a.City,
                State = a.State,
                ZipCode = a.ZipCode,
                Description = a.Description,
                Creator = a.Creator,
                UsersAttending = a.UsersAttending,
                Reviews = a.Reviews,
            };
            return viewModel;
        }

        public string GetlocalStartDateTimeStringFormatted()
        {
            string abbreviation = TimeZoneAbbreviator.Convertion(TimeZoneInfo.Local);
            return StartDate.ToLocalTime().ToString("MM/dd/yy h:mmtt") + " " + abbreviation;
        }

        public string GetDurationString()
        {
            string formatedDuration = "";
            if(Duration == 1)
            {
                formatedDuration += Duration + " Minute";
            }
            else if(Duration > 1 && Duration <= 59)
            {
                formatedDuration += Duration + " Minutes";
            }
            else if(Duration == 60)
            {
                formatedDuration += Duration/60 + " Hour";
            }
            else if(Duration > 60 && Duration < 1440)
            {
                formatedDuration += Duration/60 + " Hours";
            }
            else if(Duration == 1440)
            {
                formatedDuration += Duration/1440 + " Day";
            }
            else
            {
                formatedDuration += Duration/1440 + " Days";
                
            }
            return formatedDuration;
        }

        public bool UserIsAttending(int userID)
        {
            foreach(UserActivity ua in UsersAttending)
            {
                if (ua.UserID == userID)
                {
                    return true;
                }
            }
            return false;
        }

        //search through all activities you have created/joined looking for overlapping time lengths
        public bool UserHasTimeOverlap(User loggedUser)
        {
            foreach(UserActivity ua in loggedUser.AttendingActivities)
            {
                DateTime JoinedActivityStart = ua.Activity.StartTime;
                DateTime JoinedActivityEnd = ua.Activity.StartTime + new TimeSpan(0, ua.Activity.Duration, 0);
                DateTime ThisActivityStart = StartDate;
                DateTime ThisActivityEnd = StartDate + new TimeSpan(0, Duration, 0);
                if(JoinedActivityStart > ThisActivityStart && JoinedActivityStart < ThisActivityEnd || ThisActivityStart > JoinedActivityStart && ThisActivityStart < JoinedActivityEnd)
                {
                   return true;
                }
            }
            return false;
        }

    }

    //custom validation attribute that makes sure you are stargin the activity in the future
    public class FutureDateAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            bool valid = false;
            if (value != null && (DateTime)value > DateTime.Now) {
                valid = true;
            }
            return valid; 
            // return value != null && (DateTime)value > DateTime.Now;
        }
    }




}