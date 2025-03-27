using WebApiOrm.Core.DomainModel.Entities;
namespace WebApiOrm.Core;

public interface IUsersRepository {
   
   User? FindById(Guid id);
   IEnumerable<User> SelectAll();
   
   void Add(User user);
   void AddRange(IEnumerable<User> users);
   void Update(User user);
   void Remove(User user);
   
   User? FindByUserName(string userName);
   Person? FindPersonByUserId(Guid userId);
}