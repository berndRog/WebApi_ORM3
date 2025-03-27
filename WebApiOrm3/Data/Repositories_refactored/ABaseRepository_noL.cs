using Microsoft.EntityFrameworkCore;
using WebApiOrm.Core;
using WebApiOrm.Core.DomainModel.Entities;
namespace WebApiOrm.Data.Repositories_refactored;

public abstract class ABaseRepository_noLogs<T>(
   DataContext dataContext
) where T : AEntity {
   
   // fields
   protected readonly DbSet<T> _dbSet = dataContext.Set<T>();
   
   // virtual methods, can be overridden in derived classes
   public virtual T? FindById(Guid id) =>
      _dbSet.Find(id);
   
   public virtual IEnumerable<T> SelectAll() =>
      _dbSet.ToList();
   
   public virtual void Add(T entity) =>
      _dbSet.Add(entity);

   public virtual void AddRange(IEnumerable<T> entities) =>
      _dbSet.AddRange(entities);

   public virtual void Update(T entity) {
      var entry = dataContext.Entry(entity);
      if (entry == null)
         throw new ApplicationException($"Update failed, entity with given id not found");
      if (entry.State == EntityState.Detached) _dbSet.Attach(entity);
      entry.State = EntityState.Modified;
   }
   
   public virtual void Remove(T entity) {
      var found = _dbSet.FirstOrDefault(e => e.Id == entity.Id);
      if (found == null)
         throw new Exception($"{typeof(T).Name} to be removed not found");
      _dbSet.Remove(found);
   }
}