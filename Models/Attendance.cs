using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace AttandanceManagementSystem.Models;

[Table("Attendance")]
[Index(nameof(UserId), nameof(Date), Name = "UQ_User_Date", IsUnique = true)]
public partial class Attendance
{
    public int AttendanceId { get; set; }

    public int UserId { get; set; }

    public DateTime Date { get; set; }

    //public TimeSpan Time { get; set; }
    public TimeSpan Time { get; set; }

    public string Status { get; set; } = null!;

    public string? TaskDetails { get; set; }

    public virtual User? User { get; set; }


}
