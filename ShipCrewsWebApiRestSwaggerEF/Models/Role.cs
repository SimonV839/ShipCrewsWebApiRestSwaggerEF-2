using System;
using System.Collections.Generic;

namespace ShipCrewsWebApiRestSwaggerEF.Models;

public partial class Role
{
    public int RoleId { get; set; }

    public string? Name { get; set; }

    public virtual ICollection<Person> People { get; set; } = new List<Person>();
}
