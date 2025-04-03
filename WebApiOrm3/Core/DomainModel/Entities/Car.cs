using WebApiOrm.Core.DomainModel.NullEntities;
namespace WebApiOrm.Core.DomainModel.Entities;

public class Car: AEntity {
   
   // properties with getter only
   public override Guid Id { get; init; } = Guid.NewGuid();
   public string Maker {get; private set;} = string.Empty;
   public string Model {get; private set;} = string.Empty;
   public int Year {get; private set;}
   public decimal Price {get; private set;}
   public string? ImageUrl { get; private set; }
   // 1:n navigation property Car <-> Person (0,n):(1,1)
   public Person Person { get; private set; } = NullPerson.Instance;
   // dependent entity must have a foreign key property
   public Guid PersonId { get; private set; } = NullPerson.Instance.Id;
   
   // ctor EF Core.
   // EF Coreuses this ctor and reflexion to construct new Person object,
   // while ignoring private set in the properties
   public Car() { }  
   
   
   public Car(Guid id, string maker, string model, int year, decimal price, 
      string? imageUrl = null, Guid? personId = null) {
      Id = id;
      Maker = maker;
      Model = model;
      Year = year;
      Price = price;
      if(imageUrl != null)  ImageUrl = imageUrl;
      if(personId.HasValue) PersonId = personId.Value;
   }
  
   public void SetImageUrl(string? imageUrl) =>
      ImageUrl = imageUrl;

   public void Set(Person person) {
      Person = person;
      PersonId = person.Id;
   }

   public void Update(
      string? maker = null, 
      string? model = null, 
      int? year = null, 
      decimal? price = null
   ) {
      if(maker != null) Maker = maker;     // can the car maker be updated?
      if(model != null) Model = model;     // can the car model be updated?
      if(year.HasValue) Year = year.Value;  // can the car year be updated?
      if(price.HasValue) Price = price.Value;
   }
   
}