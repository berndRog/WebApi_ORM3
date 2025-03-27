using WebApiOrm.Core.DomainModel.Entities;
namespace WebApiOrm.Core.DomainModel.NullEntities;
// https://jonskeet.uk/csharp/singleton.html

public sealed class NullPerson: Person {  
   // Singleton Skeet Version 4
   public static NullPerson Instance { get; } = new ();
   static NullPerson() { }
   private NullPerson() { Id = Guid.Empty; }
}