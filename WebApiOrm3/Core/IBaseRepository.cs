using WebApiOrm.Core.DomainModel.Entities;
namespace WebApiOrm.Core;

public interface IBaseRepository<T> where T : AEntity {
   T? FindById(Guid id);
   IEnumerable<T>? SelectAll();
   void Add(T entity);
   void AddRange(IEnumerable<T> entities);
   void Update(T updEntity);
   void Remove(T entity);
}