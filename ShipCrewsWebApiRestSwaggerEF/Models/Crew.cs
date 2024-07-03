using System;
using System.Collections.Generic;

namespace ShipCrewsWebApiRestSwaggerEF.Models;

public partial class Crew
{
    public int CrewId { get; set; }

    public string? Name { get; set; }

    public virtual ICollection<CrewAssignment> CrewAssignments { get; set; } = new List<CrewAssignment>();
}
