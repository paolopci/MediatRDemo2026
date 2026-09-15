using DemoLibrary.Models;

namespace DemoLibrary.DataAccess;

public interface IDemoDataAccess
{
    List<PersonModel> GetPeople();
    PersonModel GetPersonById(int id);
    PersonModel InsertPerson(string firstName, string lastName);
    PersonModel UpdatePerson(int id,string firstName, string lastName);
    PersonModel? DeletePerson(int id);
}