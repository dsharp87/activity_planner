using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace activity_planner.Models
{
    public class User: BaseEntity
    {
        
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserID {get; set;}

        public string FirstName { get; set; }
        
        public string LastName { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public List<Activity> MyActivities { get; set; }

        public List<UserActivity> AttendingActivities { get; set; }


        public User() {
            MyActivities = new List<Activity>();
            AttendingActivities = new List<UserActivity>();
        }


    }
}