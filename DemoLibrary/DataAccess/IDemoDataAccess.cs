using DemoLibrary.Models;

namespace DemoLibrary.DataAccess;

public interface IDemoDataAccess
{
    List<PersonModel> GetPeople();
    PersonModel InsertPerson(string firstName, string lastName);
}