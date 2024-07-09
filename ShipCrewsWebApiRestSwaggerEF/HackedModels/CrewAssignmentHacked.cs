using ShipCrewsWebApiRestSwaggerEF.Models;

namespace ShipCrewsWebApiRestSwaggerEF.HackedModels
{
    public class CrewAssignmentHacked
    {
        public CrewAssignmentHacked() { }

        public CrewAssignmentHacked(CrewAssignment crewAssignment) { efCrewAssignment = crewAssignment; }

        public int Id 
        {
            get => efCrewAssignment.Id;
            set => efCrewAssignment.Id = value;
        }

        public int? CrewId 
        {
            get => efCrewAssignment.CrewId;
            set => efCrewAssignment.CrewId = value;
        }

        public int? PersonId 
        { 
            get => efCrewAssignment.PersonId;
            set => efCrewAssignment.PersonId = value;
        }

        private readonly CrewAssignment efCrewAssignment = new CrewAssignment();
    }
}
