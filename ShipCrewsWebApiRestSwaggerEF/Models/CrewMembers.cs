namespace ShipCrewsWebApiRestSwaggerEF.Models
{
    public class CrewMembers
    {
        public int CrewId { get; set; }

        public ICollection<int> Members { get; set; } = new List<int>();
    }
}
