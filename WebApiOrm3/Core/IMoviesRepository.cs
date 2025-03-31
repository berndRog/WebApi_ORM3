using WebApiOrm.Core.DomainModel.Entities;
namespace WebApiOrm.Core;

public interface IMoviesRepository: IBaseRepository<Movie> {
   
   // Movie? FindById(Guid id);
   // IEnumerable<Movie> SelectAll();
   // void Add(Movie user);
   // void AddRange(IEnumerable<Movie> movies);
   // void Update(Movie movie);
   // void Remove(Movie movie);

   Movie? FindByTitle(string title);
}