using WebApiOrm.Core.DomainModel.Entities;
namespace WebApiOrm.Core;

public interface ITicketsRepository: IBaseRepository<Ticket> {
   // Ticket? FindById(Guid id);
   // IEnumerable<Ticket>? SelectAll();
   // void Add(Ticket ticket);
   // void AddRange(IEnumerable<Ticket> Tickets);
   // void Update(Ticket ticket);
   // void Remove(Ticket ticket);


   IEnumerable<Ticket>? SelectByPersonId(Guid personId);
   
   Ticket? FindByIdJoinPersonAndMovie(Guid id);
}