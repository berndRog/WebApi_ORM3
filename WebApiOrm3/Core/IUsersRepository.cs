using WebApiOrm.Core.DomainModel.Entities;
namespace WebApiOrm.Core;

public interface IUsersRepository: IBaseRepository<User> {
   User? FindByUserName(string userName);
   User? FindByIdJoinPerson(Guid id);
}