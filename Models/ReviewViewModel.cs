using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;


namespace activity_planner.Models
{
    public class ReviewViewModel: BaseEntity
    {

        [Required]
        [MinLength(2)]
        public string Title { get; set; }

        [Required]
        [Range(0, 5)]
        public int Rating { get; set; }

        [Required]
        [MinLength(4)]
        public string Description { get;  set; }
    }
}