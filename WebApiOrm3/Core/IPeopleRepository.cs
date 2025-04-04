using WebApiOrm.Core.DomainModel.Entities;
namespace WebApiOrm.Core;

public interface IPeopleRepository : IBaseRepository<Person>{
   // Person? FindById(Guid id);
   // IEnumerable<Person> SelectAll();
   // void Add(Person person);
   // void AddRange(IEnumerable<Person> people);
   // void Update(Person updPerson);
   // void Remove(Person person); 
   
   IEnumerable<Person>? SelectByName(string namePattern);
   Person? FindByIdJoinUser(Guid id);
   Person? FindByIdJoinCars(Guid id);
   Person? FindByIdJoinMovies(Guid id);
}