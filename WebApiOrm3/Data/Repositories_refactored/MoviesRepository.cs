using Microsoft.EntityFrameworkCore;
using WebApiOrm.Core;
using WebApiOrm.Core.DomainModel.Entities;
namespace WebApiOrm.Data.Repositories_refactored;

public class MoviesRepository(
   DataContext dataContext
) : ABaseRepository<Movie>(dataContext), IMoviesRepository {
   private readonly DataContext _dataContext = dataContext;

   // inherited from ABaseRepository<T>
   // protected readonly DataContext dataContext
   // protected readonly DbSet<T> _dbSet
   // public virtual T? FindById(Guid id)
   // public virtual IEnumerable<T> SelectAll()
   // public virtual void Add(T entity) 
   // public virtual void AddRange(IEnumerable<T> entities) 
   // public virtual void Update(T entity) 
   // public virtual void Remove(T entity) 
   
   public Movie? FindByTitle(string title) {
      var movie= _dbSet.FirstOrDefault(movie => movie.Title == title);
      _dataContext.LogChangeTracker("User: FindById");
      return movie;
   }
   
   

}
