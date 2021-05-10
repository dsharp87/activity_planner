using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace activity_planner.Models
{
    public class Review: BaseEntity
    {
        
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ReviewID {get; set;}

        public string Title { get; set; }

        public int Rating { get; set; }

        public string Description { get;  set; }

        public int ActivityID { get; set; }

        public Activity Activity { get; set; }

        public int ReviewerID { get; set; }

        public User Reviewer { get; set; }

    }
}