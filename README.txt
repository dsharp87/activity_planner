Activity Planner is a event manager applicaton.  It allows users to register, view, create, join, and review activities that have been posted.

This project was built using ASP.NET CORE attatched to a MySQL database. It was styled using Boostrap 4.

Feature List:

Registration and Login:
    - Register if you do not have a login already
    - Login if you are a returning user

Dashboard
    - Onboarding and main display hub that displays summary information about upcoming and past activities
    - Upcoming activities table links to CRUD forms/pages of each activity 
    - Past activites allows users to review and view posted reviews
    - Razor logic displays various actions based on currently logged user's relation to a given activity

Add Activity
    - Basic add functionality
    - Validates for overlapping time period with existing user owned Activity
    - Utilizes C# datetime and timespan objects to calculate duration and end-time for activities


Join/Leave Activity
    - Users can join existing activites if they are currently unschedueled during the timespan of the activity
    - If you are already attending this activity, the option to leave is available

View/Add reviews
    - Users can view a past activity and its reviews by following any links found with the table
    - Reviews have title, 5 star rating, and description field


Want to Try it Yourself?
- clone respository
- run dotnet restore
- using MySQL workbench run dbgenerate.sql script to create empty database
- from top level, run dotnet run
- go to localhost:5000


Features I'd like to add
- Fully responsive for screensizes (headers/tables/ect)
    -tables are repsonsive, but not ideal for mobile, would want to restructure display type for mobile
- Location data integrate google maps api
- profle page
- When registered activity overlaps with existing joined activities, displaying a warning that allows users to manage that conflic
- Allow users to upvote reviews, so that most relevant or most liked reviews filter to top of list


