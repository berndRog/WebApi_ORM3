using WebApiOrm.Core.DomainModel.Entities;
namespace WebApiOrm.Core;

public interface IDataContext {
   bool SaveAllChanges(string? text = null);
   void LogChangeTracker(string text);
   void ClearChangeTracker();
}