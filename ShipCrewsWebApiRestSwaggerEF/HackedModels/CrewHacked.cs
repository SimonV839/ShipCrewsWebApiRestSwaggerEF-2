using ShipCrewsWebApiRestSwaggerEF.Models;

namespace ShipCrewsWebApiRestSwaggerEF.HackedModels
{
    public class CrewHacked
    {
        public CrewHacked() { }

        public CrewHacked(Crew crew) { efCrew = crew; }

        public Crew ToCrew() { return new Crew() { CrewId = this.CrewId, Name = this.Name }; }

        public Crew Update(Crew crew)
        {
            crew.CrewId = this.CrewId;
            crew.Name = this.Name;
            return crew;
        }

        public int CrewId 
        {
            get => efCrew.CrewId;
            set => efCrew.CrewId = value;
        }

        public string? Name 
        {
            get => efCrew.Name;
            set => efCrew.Name = value;
        }

        private readonly Crew efCrew = new Crew();
    }
}
