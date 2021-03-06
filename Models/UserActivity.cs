using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;


namespace activity_planner.Models
{
    public class UserActivity: BaseEntity
    {
        
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserActivityID {get; set;}

        public int UserID {get; set;}

        public User User {get; set;}

        public int ActivityID {get; set;}

        public Activity Activity {get; set;}

    }
}