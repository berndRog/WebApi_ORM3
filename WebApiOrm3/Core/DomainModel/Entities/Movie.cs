using WebApiOrm.Core.DomainModel.NullEntities;
using WebApiOrm.Core.Utilities;
namespace WebApiOrm.Core.DomainModel.Entities;

public class Movie: AEntity {
   public override Guid Id { get; init; } = Guid.NewGuid();
   public string Title { get; private set; } = string.Empty;
   public string Director { get; private set; } = string.Empty;
   public int Year { get; private set; } = 0;
   // n:m navigation collection Movie <-> People (0,n):(0,m)
   public ICollection<Person> People { get; private set; } = [];
   // n:1 Movies -> Tickets [0..*]
   public ICollection<Ticket> Tickets { get; private set; } = [];

   // ctor: EF Core uses this ctor and reflexion to construct new object,
   // while ignoring private set 
   public Movie() { } 
   
   // ctor for Domain Model
   public Movie(Guid id, string title, string director, int year) {
      Id = id;
      Title = title;
      Director = director;
      Year = year;
   }
   
   // Movie <-> People
   public void AddPerson(Person person) {
      People.Add(person);
   }
   public void RemovePerson(Person person) {
      People.Remove(person);
   }
   
   // Movie <-> Ticket
   public void AddTicket(Ticket ticket) {
      Tickets.Add(ticket);
   }
   public void RemoveTicket(Ticket ticket) {
      Tickets.Remove(ticket);
   }
}