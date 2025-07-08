using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace AttandanceManagementSystem.Models;

[Table("Designation")]
public partial class Designation
{
    [Key]
    [Column("DesignationID")]
    public int DesignationId { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string Role { get; set; } = null!;

    [InverseProperty("Designation")]
    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
