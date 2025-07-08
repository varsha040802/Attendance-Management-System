using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace AttandanceManagementSystem.Models;

public partial class User
{
    [Key]
    [Column("UserID")]
    public int UserId { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string UserName { get; set; } = null!;

    [Column("DesignationID")]
    public int DesignationId { get; set; }

    [InverseProperty("User")]
    public virtual ICollection<Attendance> Attendances { get; set; } = new List<Attendance>();

    [ForeignKey("DesignationId")]
    [InverseProperty("Users")]
    public virtual Designation Designation { get; set; } = null!;
}
