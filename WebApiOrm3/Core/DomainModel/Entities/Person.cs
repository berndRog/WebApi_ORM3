using System.Text.Json.Serialization;
using WebApiOrm.Core.DomainModel.NullEntities;
namespace WebApiOrm.Core.DomainModel.Entities; 

public class Person: AEntity {
   
   public override Guid Id { get; init; } = Guid.NewGuid();
   public string FirstName { get; private set; } = string.Empty;
   public string LastName { get; private set; } = string.Empty;
   public string? Email { get; private set; }
   public string? Phone { get; private set; } 
   // 1:1 navigation property Person <-> User (1,1):(0,1)
   public User? User { get; private set; } = null;
   // 1:n navigation collection Person <-> Cars (0,1):(0,n)
   public ICollection<Car> Cars { get; private set; } = [];
   // m:n navigation collection Person <-> Movies (0,m):(0,n)
   public ICollection<Movie> Movies { get; private set; } = [];
   // 1:n navigation collection Person <-> Tickets (1,1):(0,n) 
   public ICollection<Ticket> Tickets { get; private set; } = [];
   
   // ctor EF Core.
   // EF Coreuses this ctor and reflexion to construct new Person object,
   // while ignoring private set in the properties
   public Person() { } // also needed for NullPerson
   
   // ctor Domain Model
   public Person(Guid id, string firstName, string lastName, string? email = null,
      string? phone = null) {
      Id = id;
      FirstName= firstName;
      LastName = lastName;
      Email = email;
      Phone = phone;
   }   
   
   // methods
   public void Set(string? email = null, string? phone = null) {
      if(email != null) Email = email;
      if(phone != null) Phone = phone;
   } 
   
   public void Update(string firstName, string lastName, string? email = null, string? phone = null) {
      FirstName = firstName;
      LastName = lastName;
      if(email != null) Email = email;
      if(phone != null) Phone = phone;
   }
   
   // 1:1 Person <-> User
   public void SetUser(User? user) {
      if (user != null) {
         // User -> Person [1]
         user.SetPerson(this);
         // Person -> User [0..1]
         User = user;
      }
      else {
         if (User != null) {
            // User -> Person [1]
            User.SetPerson(NullPerson.Instance);
         }
         // Person -> User [0..1]
         User = null;
      }
   }
   
   // 1:n Person <-> Cars
   public void AddCar(Car car) {
      car.Set(this);
      Cars.Add(car);
   }
   public void RemoveCar(Car car) {
      car.Set(NullPerson.Instance);
      Cars.Remove(car);
   }
   
   // m:n Person <-> Movies
   public void AddMovie(Movie movie) {
      movie.AddPerson(this);
      Movies.Add(movie);
   }
   public void RemoveMovie(Movie movie) {
      movie.RemovePerson(this);
      Movies.Remove(movie);
   }
   
   
   // 1:n Person <-> Tickets
   public void CreateTicket(Movie movie, DateTime dateTime, decimal price, string seat) {
      var ticket = new Ticket(Guid.NewGuid(), dateTime, price, seat, this, movie);
      ticket.Movie.Tickets.Add(ticket);
      Tickets.Add(ticket);
   }
   public void RemoveTicket(Ticket ticket) {
      ticket.Movie.Tickets.Remove(ticket);
      Tickets.Remove(ticket);
   }
   
}