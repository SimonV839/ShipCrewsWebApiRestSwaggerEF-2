using System;
using System.Collections.Generic;

namespace ShipCrewsWebApiRestSwaggerEF.Models;

public partial class NeverAssignedPerson
{
    public string? FirstName { get; set; }

    public string LastName { get; set; } = null!;
}
