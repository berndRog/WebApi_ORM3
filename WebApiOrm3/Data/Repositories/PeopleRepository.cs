using Microsoft.EntityFrameworkCore;
using WebApiOrm.Core;
using WebApiOrm.Core.DomainModel.Entities;
namespace WebApiOrm.Data.Repositories;

public class PeopleRepository(
   DataContext dataContext
) : ABaseRepository<Person>(dataContext), IPeopleRepository {
   
   private readonly DataContext _dataContext = dataContext;

   // inherited from ABaseRepository<T>
   // protected readonly IDataContext dataContext
   // protected readonly DbSet<T> _dbSet
   // public virtual T? FindById(Guid id)
   // public virtual IEnumerable<T>? SelectAll()
   // public virtual void Add(T entity) 
   // public virtual void AddRange(IEnumerable<T> entities) 
   // public virtual void Update(T entity) 
   // public virtual void Remove(T entity) 
   
   public IEnumerable<Person>? SelectByName(string namePattern) {
      if (string.IsNullOrWhiteSpace(namePattern))
         return null;
      var people = _dbSet
         .Where(person => EF.Functions.Like(person.LastName, $"%{namePattern.Trim()}%"))
         .ToList();
      _dataContext.LogChangeTracker("Person: FindByNamePattern");
      return people;
   }

   public Person? FindByIdJoinUser(Guid id) {
      var person = _dbSet
         .Where(person => person.Id == id)
         .Include(person => person.User)
         .FirstOrDefault();
      _dataContext.LogChangeTracker("Person: FindByIdJoinUser");
      return person;
   }

   public Person? FindByIdJoinCars(Guid id) {
      var person = _dbSet
         .Where(person => person.Id == id)
         .Include(person => person.Cars)
         .FirstOrDefault();
      _dataContext.LogChangeTracker("Person: FindByIdJoinCars");
      return person;
   }
   
   public Person? FindByIdJoinMovies(Guid id) {
      var person = _dbSet
         .Where(person => person.Id == id)
         .Include(person => person.Movies)
         .FirstOrDefault();
      _dataContext.LogChangeTracker("Person: FindByIdJoinMovies");
      return person;
   }
}