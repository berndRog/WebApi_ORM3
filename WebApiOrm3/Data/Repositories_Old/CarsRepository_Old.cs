using Microsoft.EntityFrameworkCore;
using WebApiOrm.Core;
using WebApiOrm.Core.DomainModel.Entities;
using WebApiOrm.Core.Interfaces_Old;
namespace WebApiOrm.Data.Repositories_Old;

public class CarsRepository_Old(
   DataContext dataContext
) : ICarsRepository_Old {
   private readonly DbSet<Car> _dbSetCars = dataContext.Cars; // => Set<Car>

   public Car? FindById(Guid id) {
      var car = _dbSetCars.FirstOrDefault(car => car.Id == id);
      dataContext.LogChangeTracker("User: FindById");
      return car;
   }
   
   public IEnumerable<Car> SelectAll() {
      var cars = _dbSetCars.ToList();
      dataContext.LogChangeTracker("User: SelectAll");
      return cars;
   }

   public void Add(Car car) =>
      _dbSetCars.Add(car);

   public void AddRange(IEnumerable<Car> cars) =>
     _dbSetCars.AddRange(cars);

   public void Update(Car car) {
      var entry = dataContext.Entry(car);
      if (entry == null) throw new ApplicationException(
         "Update failed, car with given id not found");
      if (entry.State == EntityState.Detached)
         _dbSetCars.Attach(car);
      entry.State = EntityState.Modified;
   }

   public void Remove(Car car) {
      var entry = dataContext.Entry(car);
      if (entry == null) throw new Exception(
         "User to be removed not found");
      if (entry.State == EntityState.Detached)
         _dbSetCars.Attach(car);
      entry.State = EntityState.Deleted;
   }

   public  IEnumerable<Car> SelectByAttributes(
      string? maker = null, 
      string? model = null,
      int? yearMin = null,
      int? yearMax = null,
      decimal? priceMin = null,
      decimal? priceMax = null
   ) {
      var query = _dbSetCars.AsQueryable();
   
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
      dataContext.LogChangeTracker("Car: SelectByAttributes ");
      return cars;
   }
   
   public IEnumerable<Car> SelectCarsByPersonId(Guid personId) {
      var cars = _dbSetCars
         .Where(car => car.PersonId == personId)
         .ToList();
      dataContext.LogChangeTracker("Car: SelectCarsByPersonId ");
      return cars;
   }

   public Car? FindByIdJoinPerson(Guid id) {
      var car = _dbSetCars
         .Include(car => car.Person)
         .FirstOrDefault(car => car.Id == id);
      dataContext.LogChangeTracker("Car: FindByIdJoinPerson ");
      return car;
   }
}