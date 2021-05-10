using System.Collections.Generic;

namespace activity_planner.Models
{
    public class DashboardViewModel: BaseEntity
    {
        public List<ActivityViewModel> FutureActivities { get; set; }
        public List<ActivityViewModel> PastActivities { get; set; }

        public User LoggedUser { get; set; }

        //converts list of activities data items into activity view models
        public static List<ActivityViewModel> GetActivityViewModels(List<Activity> activities)
        {
            List<ActivityViewModel> models = new List<ActivityViewModel>();
            foreach(Activity a in activities)
            {
                models.Add(ActivityViewModel.GetActivityViewModel(a));
            }
            return models;
        }

        public bool LoggedUserIsActivityCreator(ActivityViewModel activity)
        {
            return activity.Creator.UserID == LoggedUser.UserID ? true : false;
        }

    }
}