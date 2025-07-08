Attendance Management System:-
The Attendance Management System is a web-based application built using ASP.NET Core MVC and Entity Framework Core, designed to simplify and streamline attendance tracking for organizations, teams, or institutions. It allows administrators or team leads to efficiently record, view, update, and report employee or user attendance.

Features:-
1. Insert Attendance: Mark daily attendance with status (Present, Absent, Leave) and optional task details.
2. View Attendance: Visual calendar-style view of monthly attendance with color-coded statuses.
3. Attendance Report: Generate and filter date-wise attendance reports grouped by status.
4. Edit Tasks: Modify task details for any attendance entry.
5. Success Messages: Bootstrap modal popup for form submission feedback.
6. User Management: Integrated with a user and designation system for flexible role handling.

Tech Stack:-
1. Backend: ASP.NET Core MVC (.NET 8), Entity Framework Core
2. Frontend: Bootstrap 5, HTML, Razor Views
3. Database: SQL Server (Database-First approach)
4. Tools: Visual Studio, SQL Server Management Studio

Folder Structure:-
1. /Controllers         → MVC controllers for Attendance logic
2. /Models              → EF Core models for Users, Designations, Attendance
3. /Views               → Razor views for inserting, viewing, reporting
4. /wwwroot             → Static assets (images, CSS, JS)
5. /Db context          → Database context (AMSDbContext)

Getting Started:-
1. Clone the repository:
git clone https://github.com/varsha040802/AttendanceManagementSystem.git
2. Open the solution in Visual Studio.
3. Configure your database connection in appsettings.json.
4. Run the application using IIS Express or dotnet run.

Future Enhancements:-
1. Role-based login (Admin, User)
2. Export reports to Excel/PDF
3. Attendance reminders via email
4. Mobile responsiveness
5. Leave application and approval flow

Developed By:-
Varsha Sahu
