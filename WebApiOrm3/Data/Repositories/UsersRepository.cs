using Microsoft.EntityFrameworkCore;
using WebApiOrm.Core;
using WebApiOrm.Core.DomainModel.Entities;
namespace WebApiOrm.Data.Repositories;

public class UsersRepository(
   DataContext dataContext
) : IUsersRepository {
   private readonly DbSet<User> _dbSetUsers = dataContext.Users; // => Set<Car>

   public User? FindById(Guid id) {
      var user = _dbSetUsers.FirstOrDefault(user => user.Id == id);
      dataContext.LogChangeTracker("User: FindById");
      return user;
   }
   
   public IEnumerable<User> SelectAll() {
      var users = _dbSetUsers.ToList();
      dataContext.LogChangeTracker("User: SelectAll");
      return users;
   }

   public void Add(User user) =>
      _dbSetUsers.AddAsync(user);

   public void AddRange(IEnumerable<User> users) =>
      _dbSetUsers.AddRange(users);
   
   public  void Update(User user) {
      var entry = dataContext.Entry(user);
      if (entry == null)
         throw new ApplicationException($"Update failed, user with given id not found");
      if (entry.State == EntityState.Detached) _dbSetUsers.Attach(user);
      entry.State = EntityState.Modified;
   }
   
   public void Remove(User user) {
      var entry = dataContext.Entry(user);
      if (entry == null) throw new Exception("Person to be removed not found");
      if (entry.State == EntityState.Detached) _dbSetUsers.Attach(user);
      entry.State = EntityState.Deleted;
   }

   public User? FindByUserName(string userName) {
      var user = _dbSetUsers.FirstOrDefault(user => user.UserName == userName);
      dataContext.LogChangeTracker("User: FindById");
      return user;
   }

   public Person? FindPersonByUserId(Guid userId) {
      var user = _dbSetUsers.FirstOrDefault(user => user.Id == userId); 
      dataContext.LogChangeTracker("User: FindPersonByUserId");
      var person = user?.Person;
      return person;
   }
}