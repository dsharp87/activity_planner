using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Html;
using Microsoft.EntityFrameworkCore;


namespace activity_planner.Models
{
    public class ActivityViewModel: BaseEntity
    {
        
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
        public string Zip { get; set; }

        [Required]
        [MinLength(10)]
        public string Description { get;  set; }

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