using SimonV839.ShipCrewsWebApiRestSwaggerEF.Models;

namespace SimonV839.ShipCrewsWebApiRestSwaggerEF.HackedModels
{
    public class PersonHacked
    {
        public PersonHacked() { }

        public PersonHacked(Person person) { efPerson = person; }

        public Person ToPerson() 
        { 
            return new Person() { 
            PersonId = this.PersonId, 
            FirstName = this.FirstName,
            LastName = this.LastName,
            RoleId = this.RoleId}; 
        }

        public Person Update(Person person)
        {
            person.PersonId = this.PersonId;
            person.FirstName = this.FirstName;
            person.LastName = this.LastName;
            person.RoleId = this.RoleId;

            return person;
        }

        public int PersonId
        {
            get => efPerson.PersonId;
            set => efPerson.PersonId = value;
        }

        public string? FirstName 
        { 
            get => efPerson?.FirstName;
            set => efPerson.FirstName = value;
        }

        public string LastName {
            get => efPerson.LastName;
            set => efPerson.LastName = value;
        }

        public int? RoleId 
        {
            get => efPerson.RoleId;
            set => efPerson.RoleId = value;
        }

        private readonly Person efPerson = new Person();
    }
}
