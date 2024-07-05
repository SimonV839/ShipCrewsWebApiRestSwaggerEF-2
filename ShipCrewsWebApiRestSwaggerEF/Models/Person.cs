using System;
using System.Collections.Generic;

namespace ShipCrewsWebApiRestSwaggerEF.Models;

public partial class Person
{
    public int PersonId { get; set; }

    public string? FirstName { get; set; }

    public string LastName { get; set; } = null!;

    public int? RoleId { get; set; }

    public virtual ICollection<CrewAssignment> CrewAssignments { get; set; } = new List<CrewAssignment>();

    public virtual Role? Role { get; set; }
}
