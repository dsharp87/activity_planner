using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;


namespace activity_planner.Models
{
    public class ReviewViewModel: BaseEntity
    {
        [Required(ErrorMessage="You must give your review a title with at least 2 characters")]
        [MinLength(2)]
        [Display(Name = "Review Title")]
        public string Title { get; set; }

        [Required(ErrorMessage="You must give a star rating of 1-5")]
        [Range(1, 5, ErrorMessage="Must give a 1-5 star rating")]
        [Display(Name = "Star Rating (1-5)")]
        public int Rating { get; set; }

        [Required(ErrorMessage="Your review must be at least 4 characters long")]
        [MinLength(4)]
        [Display(Name = "Review")]
        public string Description { get;  set; }
    }
}