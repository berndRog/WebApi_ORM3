namespace WebApiOrm.Core.DomainModel.Entities;

public class Ticket: AEntity {
   
   public override Guid Id { get; init; } = Guid.NewGuid();
   public DateTime DateTime { get; private set; } = DateTime.UtcNow.Date;
   public decimal Price { get; private set; } = 0.0m;
   public string Seat { get; private set; } = string.Empty;
   
   // 1:1 navigation property Ticket -> Person [1]
   public Person Person { get; private set; }
   //     navigation foreign key
   public Guid PersonId { get; private set; }

   // 1:1 navigation property Ticket -> Movie [1]
   public Movie Movie { get; private set; }
   //    navigation foreign key
   public Guid MovieId { get; private set; }

   // ctor for EF Core
   public Ticket() { } 
   // ctor domain model
   public Ticket(Guid id, DateTime dateTime, decimal price, string seat, Person person, Movie movie) {
      Id = id;
      
      DateTime = dateTime;
      Price = price;
      Seat = seat;
      
      Person = person;
      PersonId = person.Id;
      Movie = movie;
      MovieId = movie.Id;
   }
   
}