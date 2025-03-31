using Microsoft.EntityFrameworkCore;
using WebApiOrm.Core;
using WebApiOrm.Core.DomainModel.Entities;
namespace WebApiOrm.Data.Repositories_refactored;

public class CarsRepository(
   DataContext dataContext
) : ABaseRepository<Car>(dataContext), ICarsRepository {
   
   private readonly DataContext _dataContext = dataContext;
   
   // inherited from ABaseRepository<T>
   // protected readonly DbSet<T> _dbSet
   // public virtual T? FindById(Guid id)
   // public virtual IEnumerable<T> SelectAll()
   // public virtual void Add(T entity) 
   // public virtual void AddRange(IEnumerable<T> entities) 
   // public virtual void Update(T entity) 
   // public virtual void Remove(T entity) 
   
   public  IEnumerable<Car> SelectByAttributes(
      string? maker = null, 
      string? model = null,
      int? yearMin = null,
      int? yearMax = null,
      decimal? priceMin = null,
      decimal? priceMax = null
   ) {
      var query = _dbSet.AsQueryable();
   
      if (!string.IsNullOrEmpty(maker))
         query = query.Where(car => car.Maker == maker);
      if (!string.IsNullOrEmpty(model))
         query = query.Where(car => car.Model == model);
      if (yearMin.HasValue)
         query = query.Where(car => car.Year >= yearMin.Value);
      if (yearMax.HasValue)
         query = query.Where(car => car.Year <= yearMax.Value);
      if (priceMin.HasValue)
         query = query.Where(car => car.Price >= priceMin.Value);
      if (priceMax.HasValue)
         query = query.Where(car => car.Price <= priceMax.Value);

      var cars = query.ToList();
      _dataContext.LogChangeTracker("Car: SelectByAttributesAsync ");
      return cars;
   }


   public IEnumerable<Car> SelectCarsByPersonId(Guid personId) {
      var cars = _dbSet
         .Where(car => car.PersonId == personId)
         .ToList();
      _dataContext.LogChangeTracker("Car: SelectCarsByPersonIdAsync ");
      return cars;
   }
   
   public Car? FindByIdJoinPerson(Guid id) {
      var car = _dbSet
         .Include(car => car.Person)
         .FirstOrDefault(car => car.Id == id);
      _dataContext.LogChangeTracker("Car: FindByIdJoinPerson ");
      return car;
   }
}