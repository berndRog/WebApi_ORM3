using Microsoft.EntityFrameworkCore;
using WebApiOrm.Core;
using WebApiOrm.Core.DomainModel.Entities;
namespace WebApiOrm.Data.Repositories;

public class TicketsRepository(
   DataContext dataContext
) : ABaseRepository<Ticket>(dataContext), ITicketsRepository {
   private readonly DataContext _dataContext = dataContext;

   // get tickets for a personId
   public IEnumerable<Ticket>? SelectByPersonId(Guid personId) {
      var tickets = _dbSet
         .Where(ticket => ticket.PersonId == personId)
         .ToList();
      _dataContext.LogChangeTracker("SelectByPersonId");
      return tickets;
   }

   // get ticket by id and include Person and Movie
   public Ticket? FindByIdJoinPersonAndMovie(Guid id) {
      var movie = _dbSet
         .Where(movie => movie.Id == id)
         .Include(movie => movie.Person)
         .Include(movie => movie.Movie)
         .FirstOrDefault();
      _dataContext.LogChangeTracker("FindByIdJoinPersonAndMovie");
      return movie;
   }
   
}
