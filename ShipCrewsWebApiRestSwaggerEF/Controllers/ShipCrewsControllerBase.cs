using Microsoft.AspNetCore.Mvc;
using ShipCrewsWebApiRestSwaggerEF.Models;

namespace ShipCrewsWebApiRestSwaggerEF.Controllers
{
    public class ShipCrewsControllerBase<T> : ControllerBase
    {
        public ShipCrewsControllerBase(ILogger<T> logger, ShipCrewsContext context)
        {
            Logger = logger;
            Context = context;
        }

        protected ILogger<T> Logger { get; private init; }
        protected ShipCrewsContext Context { get; private init; }
    }
}
