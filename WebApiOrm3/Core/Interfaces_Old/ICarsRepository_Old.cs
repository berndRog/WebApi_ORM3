using WebApiOrm.Core.DomainModel.Entities;
namespace WebApiOrm.Core;

public interface ICarsRepository_Old {
   
   Car? FindById(Guid id);
   IEnumerable<Car> SelectAll();
   
   void Add(Car car);
   void AddRange(IEnumerable<Car> cars);
   void Update(Car updCar);
   void Remove(Car car);
   
   IEnumerable<Car> SelectByAttributes(
      string? maker, string? model, int? yearMin, int? yearMax, 
      decimal? priceMin, decimal? priceMax);
   
   IEnumerable<Car> SelectCarsByPersonId(Guid personId);
   Car? FindByIdJoinPerson(Guid id);
   
}