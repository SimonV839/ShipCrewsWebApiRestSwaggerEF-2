using ShipCrewsWebApiRestSwaggerEF.Models;

namespace ShipCrewsWebApiRestSwaggerEF.HackedModels
{
    public class RoleHacked
    {
        public RoleHacked() { }

        public RoleHacked(Role role) { efRole = role; }

        public int RoleId 
        {
            get => efRole.RoleId;
            set => efRole.RoleId = value;
        }

        public string? Name 
        { 
            get => efRole.Name;
            set => efRole.Name = value;
        }

        private Role efRole = new Role();
    }
}
