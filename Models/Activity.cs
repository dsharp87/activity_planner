using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;


namespace activity_planner.Models
{
    public class Activity: BaseEntity
    {
        
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ActivityID {get; set;}

        [Required]
        [MinLength(2)]
        public string Name { get; set; }
        

        public DateTime StartTime { get; set; }

        //this will get added and updated at later point
        // public DateTime EndTime { get; set; }

        //This is in Minutes
        public int Duration { get; set; }

        public string StreetAddress {get; set; }

        public string City { get; set; }

        public string State { get; set; }

        public string ZipCode { get; set; }

        public string Description { get;  set; }

        public int CreatorID { get; set; }

        public User Creator { get; set; }

        public List<UserActivity> UsersAttending { get; set;}

        public List<Review> Reviews { get; set;}



        public Activity() {
            UsersAttending = new List<UserActivity>();
            Reviews = new List<Review>();
        }

    }
}