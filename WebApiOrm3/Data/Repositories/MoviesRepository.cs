using Microsoft.EntityFrameworkCore;
using WebApiOrm.Core;
using WebApiOrm.Core.DomainModel.Entities;
namespace WebApiOrm.Data.Repositories;

public class MoviesRepository(
   DataContext dataContext
) : ABaseRepository<Movie>(dataContext), IMoviesRepository {
   private readonly DataContext _dataContext = dataContext;

   // inherited from ABaseRepository<T>
   // protected readonly DbSet<T> _dbSet
   // public virtual T? FindById(Guid id)
   // public virtual IEnumerable<T>? SelectAll()
   // public virtual void Add(T entity) 
   // public virtual void AddRange(IEnumerable<T> entities) 
   // public virtual void Update(T entity) 
   // public virtual void Remove(T entity) 
   
   // get movies by title
   public Movie? FindByTitle(string title) {
      var movie= _dbSet.FirstOrDefault(movie => movie.Title == title);
      _dataContext.LogChangeTracker("User: FindByTitle");
      return movie;
   }

   // get visitors for a movie
   public Movie? FindByIdJoinPerson(Guid id) {
      var movie = _dbSet
         .Where(movie => movie.Id == id)
         .Include(movie => movie.People)
         .FirstOrDefault();
      _dataContext.LogChangeTracker("User: FindByIdJoinPerson");
      return movie;
   }
   
   public IEnumerable<Movie>? SelectByIds(IEnumerable<Guid> movieIds) {
      var movies = _dbSet
         .Where(movie => movieIds.Contains(movie.Id))
         .ToList();
      _dataContext.LogChangeTracker("Movie: SelectByIds");
      return movies;
   }
}
