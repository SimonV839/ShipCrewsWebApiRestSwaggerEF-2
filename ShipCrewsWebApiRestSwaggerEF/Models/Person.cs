using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShipCrewsWebApiRestSwaggerEF.Models;

public partial class Person
{
    //[Key]   //  Simon: manually added - does not help with removing key from post
    //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]   //  Simon: manually added - does not help with removing key from post
    public int PersonId { get; set; }

    public string? FirstName { get; set; }

    public string LastName { get; set; } = null!;

    public int? RoleId { get; set; }

    public virtual ICollection<CrewAssignment> CrewAssignments { get; set; } = new List<CrewAssignment>();

    public virtual Role? Role { get; set; }
}
