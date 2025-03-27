using WebApiOrm.Core.DomainModel.NullEntities;
using WebApiOrm.Core.Utilities;
namespace WebApiOrm.Core.DomainModel.Entities;

public class User: AEntity {
   public override Guid Id { get; init; } = Guid.NewGuid();
   public string UserName { get; private set; } = string.Empty;
   public string Password { get; private set; } = string.Empty;
   public string Salt { get; private set; } = string.Empty;
   // 1:1 navigation property User <-> Person (0,1):(1,1)
   public Person Person { get; private set; } = NullPerson.Instance;
   // dependent entity must have a foreign key property
   public Guid PersonId { get; private set; } = NullPerson.Instance.Id;
   
   // EF Core uses this ctor and reflexion to construct new object,
   // while ignoring private set in the properties
   public User() { } 
   
   // ctor for Domain Model
   public User(Guid id, string userName, string salt, string password) {
      Id = id;
      UserName = userName;
      Salt = salt;
      Password = password;
   }
   // ctor when a User is created from input values, id can be null a will be ignored
   public User(Guid? id, string userName, string password) {
      if(id.HasValue) Id = id.Value;
      UserName = userName;
      Salt = Hashing.GenerateSalt();;
      Password = Hashing.HashPassword(password, Salt);;
   }
   
   // methods
   public void Update(string userName, string password) {
      UserName = userName;
      // generate a new salt and hash clear text password
      Salt = Hashing.GenerateSalt();
      Password = Hashing.HashPassword(password, Salt);
   }

   public void SetPerson(Person person) {
      Person = person;
      PersonId = person.Id;
   }
   
}