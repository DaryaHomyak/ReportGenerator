using DataAccessLibrary.Models;

namespace DataAccessLibrary
{
    public interface IPeopleData
    {
        Task<List<PersonModel>> GetLines();
        Task InsertPerson(PersonModel person);
    }
}