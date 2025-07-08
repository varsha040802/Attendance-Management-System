using AttandanceManagementSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Globalization;

namespace AttandanceManagementSystem.Controllers
{
    public class AttendanceController : Controller
    {
        private readonly AMSDbContext _context;

        public AttendanceController(AMSDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult InsertAttendance()
        {
            ViewBag.Users = _context.Users
                .Select(u => new { u.UserId, u.UserName })
                .ToList();
            return View();
        }

        [HttpGet]
        public IActionResult GetDesignation(int userId)
        {
            var designation = _context.Users
                .Include(u => u.Designation)
                .Where(u => u.UserId == userId)
                .Select(u => u.Designation.Role)
                .FirstOrDefault();

            if (designation == null)
                return NotFound("Not found");

            return Content(designation);
        }

        [HttpPost]
        public IActionResult InsertAttendance(Attendance attendance)
        {
            if (!ModelState.IsValid)
            {
                foreach (var key in ModelState.Keys)
                {
                    var errors = ModelState[key].Errors;
                    foreach (var error in errors)
                    {
                        Console.WriteLine($"ModelState Error → Field: '{key}', Message: '{error.ErrorMessage}'");
                    }
                }
                
                ViewBag.Users = _context.Users
                    .Select(u => new { u.UserId, u.UserName })
                    .ToList();

                TempData["Error"] = "Form data is invalid. Please ensure all fields are filled correctly.";
                return View("InsertAttendance", attendance);
            }


            if (string.IsNullOrWhiteSpace(attendance.Status))
            {
                TempData["Error"] = "Please select a valid attendance status.";
                return RedirectToAction("InsertAttendance");
            }


            bool alreadyMarked = _context.Attendances.Any(a =>
                a.UserId == attendance.UserId &&
                a.Date.Date == attendance.Date.Date);

            if (alreadyMarked)
            {
                TempData["Error"] = "Attendance has already been submitted for this user today.";
                return RedirectToAction("InsertAttendance");
            }


            try
            {
                _context.Attendances.Add(attendance);
                _context.SaveChanges();
                TempData["Success"] = "Attendance submitted successfully!";
            }
            catch (Exception ex)
            {
                TempData["Error"] = "An error occurred while saving attendance: " + ex.Message;
            }

            return RedirectToAction("InsertAttendance");
        }


        public IActionResult ViewAttendance()
        {
            ViewBag.Users = _context.Users
                .Select(u => new { u.UserId, u.UserName })
            .ToList();

            return View();

        }

        public IActionResult AttendanceDetails(int userId, int year = 0, int month = 0)
        {
            if (year == 0 || month == 0)
            {
                var today = DateTime.Today;
                year = today.Year;
                month = today.Month;
            }

            var firstDay = new DateTime(year, month, 1);
            var totalDays = DateTime.DaysInMonth(year, month);
            var startDayIndex = ((int)firstDay.DayOfWeek + 6) % 7; // Adjusting for Monday = 0

            var userAttendance = _context.Attendances
                .Where(a => a.UserId == userId &&
                            a.Date.Year == year &&
                            a.Date.Month == month)
                .ToList();


            var attendanceData = new Dictionary<int, string>();
            foreach (var att in userAttendance)
            {
                attendanceData[att.Date.Day] = att.Status;
            }


            int presentCount = userAttendance.Count(a => a.Status == "Present");
            int absentCount = userAttendance.Count(a => a.Status == "Absent");
            int leaveCount = userAttendance.Count(a => a.Status == "Leave");

            ViewBag.PresentCount = presentCount;
            ViewBag.AbsentCount = absentCount;
            ViewBag.LeaveCount = leaveCount;


            var user = _context.Users.FirstOrDefault(u => u.UserId == userId);
            ViewBag.UserName = user != null ? user.UserName : "Unknown";

            ViewBag.UserId = userId;
            ViewBag.Year = year;
            ViewBag.Month = month;
            ViewBag.MonthName = firstDay.ToString("MMMM");
            ViewBag.TotalDays = totalDays;
            ViewBag.StartDayIndex = startDayIndex;
            ViewBag.AttendanceData = attendanceData;

            return View();
        }

        public IActionResult AttendanceReport(DateTime? selectedDate)
        {
            DateTime dateToSearch = selectedDate ?? DateTime.Today;


            var attendanceData = _context.Attendances
                .Include(a => a.User)
                .Where(a => a.Date.Date == dateToSearch.Date)
                .ToList();


            var presentUsers = attendanceData.Where(a => a.Status == "Present").ToList();
            var absentUsers = attendanceData.Where(a => a.Status == "Absent").ToList();
            var leaveUsers = attendanceData.Where(a => a.Status == "Leave").ToList();

            ViewBag.SelectedDate = dateToSearch.ToString("yyyy-MM-dd");
            ViewBag.PresentUsers = presentUsers;
            ViewBag.AbsentUsers = absentUsers;
            ViewBag.LeaveUsers = leaveUsers;

            return View();
        }

        [HttpGet]
        public IActionResult EditTask(int id)
        {
            var attendance = _context.Attendances
                .Include(a => a.User)
                .FirstOrDefault(a => a.AttendanceId == id);

            if (attendance == null)
                return NotFound();

            return View(attendance);
        }

        [HttpPost]
        public IActionResult EditTask(Attendance updated)
        {
            var attendance = _context.Attendances.FirstOrDefault(a => a.AttendanceId == updated.AttendanceId);
            if (attendance == null)
                return NotFound();

            attendance.TaskDetails = updated.TaskDetails;
            _context.SaveChanges();

            TempData["Success"] = "Task updated successfully!";
            return RedirectToAction("AttendanceReport");
        }

        public IActionResult ExportToExcel()
        {
            return View();
        }

    }

}
