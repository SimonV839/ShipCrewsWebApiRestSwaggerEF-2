using System;
using System.Collections.Generic;

namespace ShipCrewsWebApiRestSwaggerEF.Models;

public partial class CrewAssignment
{
    public int Id { get; set; }

    public int? CrewId { get; set; }

    public int? PersonId { get; set; }

    public virtual Crew? Crew { get; set; }

    public virtual Person? Person { get; set; }
}
