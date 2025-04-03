using Microsoft.EntityFrameworkCore;
using WebApiOrm.Core;
using WebApiOrm.Core.DomainModel.Entities;
namespace WebApiOrm.Data.Repositories;

public class UsersRepository(
   DataContext dataContext
) : ABaseRepository<User>(dataContext), IUsersRepository {
   private readonly DataContext _dataContext = dataContext;
   // Inherited CRUD functionality from ABaseRepository<User>

   // inherited from ABaseRepository<T>
   // protected readonly DbSet<T> _dbSet
   // public virtual T? FindById(Guid id)
   // public virtual IEnumerable<T>? SelectAll()
   // public virtual void Add(T entity) 
   // public virtual void AddRange(IEnumerable<T> entities) 
   // public virtual void Update(T entity) 
   // public virtual void Remove(T entity) 
   
   public User? FindByUserName(string userName) {
      var user = _dbSet.FirstOrDefault(user => user.UserName == userName);
      _dataContext.LogChangeTracker("User: FindById");
      return user;
   }
   
   // Custom method: load a user along with its associated person.
   public User? FindByIdJoinPerson(Guid id) {
      var user = _dbSet
          .Where(u => u.Id == id)
          .Include(u => u.Person)
          .FirstOrDefault();
       _dataContext.LogChangeTracker("User: FindByIdJoinPerson");
       return user;
   }
}
