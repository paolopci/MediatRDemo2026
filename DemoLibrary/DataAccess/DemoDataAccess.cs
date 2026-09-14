using DemoLibrary.Models;

namespace DemoLibrary.DataAccess
{
    

    public class DemoDataAccess : IDemoDataAccess
    {
        private List<PersonModel> people = new();

        public DemoDataAccess()
        {
            people.Add(new PersonModel { Id = 1, FirstName = "John", LastName = "Doe" });
            people.Add(new PersonModel { Id = 2, FirstName = "Paolo", LastName = "Paci" });
            people.Add(new PersonModel { Id = 3, FirstName = "Celeste", LastName = "Paci" });
            people.Add(new PersonModel { Id = 4, FirstName = "Elisa", LastName = "Calbini" });
        }

        public List<PersonModel> GetPeople()
        {
            return people;
        }

        public PersonModel GetPersonById(int id)
        {
            if (id == null)
            {
                return null;
            }
            return people.FirstOrDefault(p => p.Id == id);
        }

        public PersonModel InsertPerson(string firstName, string lastName)
        {
            PersonModel p = new() { FirstName = firstName, LastName = lastName };
            p.Id = people.Max(x => x.Id) + 1;
            people.Add(p);
            return p;
        }
    }
}
