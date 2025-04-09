using System;
using System.Collections.Generic;
using System.Linq;
using WebApiOrm.Core.DomainModel.Entities;
using WebApiOrm.Core.Utilities;
using WebApiOrm.Data.Repositories;
namespace WebApiOrmTest;

public class Seed {

   #region properties
   public Person Person1{ get; }
   public Person Person2{ get; }
   public Person Person3{ get; }
   public Person Person4{ get; }
   
   public User User1{ get; }
   public User User2{ get; }
   public User User3{ get; }
   public User User4{ get; }
   
   public Car Car1{ get; }
   public Car Car2{ get; }
   public Car Car3{ get; }
   public Car Car4{ get; }
   public Car Car5{ get; }
   public Car Car6{ get; }
   public Car Car7{ get; }
   public Car Car8{ get; }

   public Movie Movie1{ get; }
   public Movie Movie2{ get; }
   public Movie Movie3{ get; }
   public Movie Movie4{ get; }
   public Movie Movie5{ get; }
   public Movie Movie6{ get; }
   public Movie Movie7{ get; }
   public Movie Movie8{ get; }
   
   public Ticket Ticket1{ get; }
   public Ticket Ticket2{ get; }
   public Ticket Ticket3{ get; }
   public Ticket Ticket4{ get; }
   public Ticket Ticket5{ get; }
   public Ticket Ticket6{ get; }
   public Ticket Ticket7{ get; }
   public Ticket Ticket8{ get; }
   public Ticket Ticket9{ get; }
   public Ticket Ticket10{ get; }
   public Ticket Ticket11{ get; }
   public Ticket Ticket12{ get; }

   public List<Person> People{ get; private set; }
   public List<User> Users{ get; private set; } 
   public List<Car> Cars{ get; private set; } 
   public List<Movie> Movies{ get; private set; } 
   public List<Ticket> Tickets{ get; private set; }
   #endregion

   public Seed(){
      
      #region People
      Person1 = new Person(
         id: new Guid("10000000-0000-0000-0000-000000000000"),
         firstName: "Erika",
         lastName: "Mustermann",
         email: "erika.mustermann@t-online.de",
         phone: "05826 1234 5678"
      );
      Person2 = new Person (
         id: new Guid("20000000-0000-0000-0000-000000000000"),
         firstName: "Max",
         lastName: "Mustermann",
         email: "max.mustermann@gmail.com",
         phone: "05826 1234 5678"
      );
      Person3 = new Person (
         id: new Guid("30000000-0000-0000-0000-000000000000"),
         firstName: "Arno",
         lastName: "Arndt",
         email: "a.arndt@t-online.de",
         phone: "04131 9876 5432"
      );
      Person4 = new Person(
         id: new Guid("40000000-0000-0000-0000-000000000000"),
         firstName: "Benno",
         lastName: "Bauer",
         email: "b.bauer@gmail.com",
         phone: "05141 4321 9876"
      );
      #endregion

      #region Users
      User1 = new User(
         id: new Guid("01000000-0000-0000-0000-000000000000"),
         userName: "mustermanne",
         password: "geh1m_"
      );
      User2 = new User (
         id: new Guid("02000000-0000-0000-0000-000000000000"),
         userName: "mustermannm",
         password: "geh1m_"
      );
      User3 = new User (
         id: new Guid("30000000-0000-0000-0000-000000000000"),
         userName: "arndta",
         password: "geh1m_"
      );
      User4 = new User(
         id: new Guid("04000000-0000-0000-0000-000000000000"),
         userName: "bauerb",
         password: "geh1m_"
      );
      #endregion

      #region Cars
      Car1 = new Car(
         id: new Guid("00100000-0000-0000-0000-000000000000"),
         maker: "VW",
         model: "Golf",
         year: 2018,
         price: 15_000
      );
      Car2 = new Car(
         id: new Guid("00200000-0000-0000-0000-000000000000"),
         maker: "BMW",
         model: "520",
         year: 2020,
         price: 29_000
      );
      Car3 = new Car(
         id: new Guid("00300000-0000-0000-0000-000000000000"),
         maker: "Opel",
         model: "Mokka",
         year: 2022,
         price: 21_000
      );
      Car4 = new Car(
         id: new Guid("00400000-0000-0000-0000-000000000000"),
         maker: "VW",
         model: "Golf",
         year: 2015,
         price: 13_000
      );
      Car5 = new Car(
         id: new Guid("00500000-0000-0000-0000-000000000000"),
         maker: "BMW",
         model: "X5",
         year: 2021,
         price: 49_500
      );
      Car6 = new Car(
         id: new Guid("00600000-0000-0000-0000-000000000000"),
         maker: "Hyuandai",
         model: "Tucson",
         year: 2021,
         price: 24500
      );
      Car7 = new Car(
         id: new Guid("00700000-0000-0000-0000-000000000000"),
         maker: "VW",
         model: "Golf",
         year: 2010,
         price: 9500
      );
      Car8 = new Car(
         id: new Guid("00800000-0000-0000-0000-000000000000"),
         maker: "VW",
         model: "Golf",
         year: 2012,
         price: 10500
      );
      #endregion

      #region Movies
      Movie1 = new Movie(
         id: new Guid("00010000-0000-0000-0000-000000000000"),
         title: "Der Pate",
         director: "Francis Ford Coppola",
         year: 1972
      );
      Movie2 = new Movie(
         id: new Guid("00020000-0000-0000-0000-000000000000"),
         title: "The Dark Knight",
         director: "Christopher Nolan",
         year: 2008
      );
      Movie3 = new Movie(
         id: new Guid("00030000-0000-0000-0000-000000000000"),
         title: "Herr der Ringe: Rückkehr der Könige",
         director: "Peter Jackson",
         year: 2003
      );
      Movie4 = new Movie(
         id: new Guid("00040000-0000-0000-0000-000000000000"),
         title: "Pulp Fiction",
         director: "Quentin Tarantino",
         year: 1994
      );
      Movie5 = new Movie(
         id: new Guid("00050000-0000-0000-0000-000000000000"),
         title: "Schindler's Liste",
         director: "Steven Spielberg",
         year: 1994
      );
      Movie6 = new Movie(
         id: new Guid("00060000-0000-0000-0000-000000000000"),
         title: "Inception",
         director: "Christopher Nolan",
         year: 2010
      );
      Movie7 = new Movie(
         id: new Guid("00070000-0000-0000-0000-000000000000"),
         title: "Forrest Gump",
         director: "Robert Zemeckis",
         year: 1994
      );
      Movie8 = new Movie(
         id: new Guid("00080000-0000-0000-0000-000000000000"),
         title: "Matrix",
         director: "Lana&Lilly Wachowski",
         year: 1999
      );
      #endregion
      
      #region Tickets
      // Person1
      Ticket1 = new Ticket(
         id: new Guid("00000000-0001-0000-0000-000000000001"),
         dateTime: new DateTime(2025, 3, 18, 20, 0, 0),
         price: 10.0m,
         seat: "H1",
         person: Person1,
         movie: Movie1
      );
      Ticket2 = new Ticket(
         id: new Guid("00000000-0002-0000-0000-000000000002"),
         dateTime: new DateTime(2025, 3, 25, 20, 0, 0, 0),
         price: 10.0m,
         seat: "F2",
         person: Person1,
         movie: Movie3
      );
      Ticket3 = new Ticket(
         id: new Guid("00000000-0003-0000-0000-000000000003"),
         dateTime: new DateTime(2025, 4, 1, 20, 0, 0, 0),
         price: 10.0m,
         seat: "F3",
         person: Person1,
         movie: Movie5
      );
      Ticket4 = new Ticket(
         id: new Guid("00000000-0004-0000-0000-000000000004"),
         dateTime: new DateTime(2025, 4, 9, 20, 0, 0, 0),
         price: 10.0m,
         seat: "H4",
         person: Person1,
         movie: Movie7
      );
      // Person2
      Ticket5 = new Ticket(
         id: new Guid("00000000-0005-0000-0000-000000000005"),
         dateTime: new DateTime(2025, 3, 19, 20, 0, 0),
         price: 10.0m,
         seat: "H5",
         person: Person2,
         movie: Movie2
      );
      Ticket6 = new Ticket(
         id: new Guid("00000000-0006-0000-0000-000000000006"),
         dateTime: new DateTime(2025, 3, 26, 20, 0, 0),
         price: 10.0m,
         seat: "F6",
         person: Person2,
         movie: Movie4
      );
      Ticket7 = new Ticket(
         id: new Guid("00000000-0007-0000-0000-000000000007"),
         dateTime: new DateTime(2025, 4, 2, 20, 0, 0),
         price: 10.0m,
         seat: "G7",
         person: Person2,
         movie: Movie6
      );
      Ticket8 = new Ticket(
         id: new Guid("00000000-0008-0000-0000-000000000008"),
         dateTime: new DateTime(2025, 4, 9, 20, 0, 0),
         price: 10.0m,
         seat: "F8",
         person: Person2,
         movie: Movie8
      );
      // Person3
      Ticket9 = new Ticket(
         id: new Guid("00000000-0009-0000-0000-000000000009"),
         dateTime: new DateTime(2025, 3, 18, 20, 0, 0),
         price: 10.0m,
         seat: "H9",
         person: Person3,
         movie: Movie1
      );
      Ticket10 = new Ticket(
         id: new Guid("00000000-0010-0000-0000-000000000010"),
         dateTime: new DateTime(2025, 3, 26, 20, 0, 0, 0 ,0),
         price: 10.0m,
         seat: "F10",
         person: Person3,
         movie: Movie2
      );
      // Person4
      Ticket11 = new Ticket(
         id: new Guid("00000000-0011-0000-0000-000000000011"),
         dateTime: new DateTime(2025, 4, 1, 20, 0, 0, 0 ,0),
         price: 10.0m,
         seat: "F11",
         person: Person4,
         movie: Movie3
      );
      Ticket12 = new Ticket(
         id: new Guid("00000000-0012-0000-0000-000000000012"),
         dateTime: new DateTime(2025, 4, 8, 20, 0, 0, 0 ,0),
         price: 10.0m,
         seat: "H12",
         person: Person4,
         movie: Movie4
      );
      #endregion
      People = [Person1, Person2, Person3, Person4];
      Users = [User1, User2, User3, User4];
      Cars = [Car1, Car2, Car3, Car4, Car5, Car6, Car7, Car8];
      Movies = [Movie1, Movie2, Movie3, Movie4, Movie5, Movie6, Movie7, Movie8];
      Tickets = [Ticket1, Ticket2, Ticket3, Ticket4, Ticket5, Ticket6, Ticket7, Ticket8,
         Ticket9, Ticket10, Ticket11, Ticket12];
   }
   
   // Setup Relations between People and Users
   public Seed InitUsers(){
      Person1.SetUser(User1); 
      Person2.SetUser(User2);
      Person3.SetUser(User3); 
      Person4.SetUser(User4); 
      return this;
   }
   
   // Setup Relations between Users and Cars
   public Seed InitCars(){
      // Users
      Person1.AddCar(Car1); 
      Person1.AddCar(Car2);
      Person2.AddCar(Car3); 
      Person2.AddCar(Car4);
      Person3.AddCar(Car5); 
      Person3.AddCar(Car6);
      Person3.AddCar(Car7);
      Person4.AddCar(Car8); 
      return this;
   }
   
   public static IEnumerable<User> InitPeopleWithUser(
      IEnumerable<Person> people,
      IEnumerable<User> users
   ) {
      var lPeople = people.ToArray();
      var lUsers = users.ToArray();
      if(lPeople.Count() != 4 || lUsers.Count() != 4) 
         throw new ArgumentException("Invalid number of people or users");
      lPeople[0].SetUser(lUsers[0]);
      lPeople[1].SetUser(lUsers[1]);
      lPeople[2].SetUser(lUsers[2]);
      lPeople[3].SetUser(lUsers[3]);
      return lUsers;
   }
   
   public static (IEnumerable<Person>, IEnumerable<Car>) InitPeopleWithCars(
      IEnumerable<Person> people,
      IEnumerable<Car> cars
   ) {
      var lPeople = people.ToList();
      var lCars = cars.ToList();
      if(lPeople.Count() != 4 || lCars.Count() != 8) 
         throw new ArgumentException("Invalid number of people or cars");
      lPeople[0].AddCar(lCars[0]);
      lPeople[0].AddCar(lCars[1]);
      lPeople[1].AddCar(lCars[2]);
      lPeople[1].AddCar(lCars[3]);
      lPeople[2].AddCar(lCars[4]);
      lPeople[2].AddCar(lCars[5]);
      lPeople[2].AddCar(lCars[6]);
      lPeople[3].AddCar(lCars[7]);
      return (lPeople, lCars);
   }
   
   public static (IEnumerable<Person>, IEnumerable<Movie>) InitPeopleWithMovies(
      IEnumerable<Person> people,
      IEnumerable<Movie> movies
   ) {
      var lPeople = people.ToList();
      var lMovies = movies.ToList();
      if(lPeople.Count() != 4 || lMovies.Count() != 8) 
         throw new ArgumentException("Invalid number of people or cars");
      
      lPeople[0].AddMovie(lMovies[0]);
      lPeople[0].AddMovie(lMovies[2]);
      lPeople[0].AddMovie(lMovies[4]);
      lPeople[0].AddMovie(lMovies[6]);

      lPeople[1].AddMovie(lMovies[1]);
      lPeople[1].AddMovie(lMovies[3]);
      lPeople[1].AddMovie(lMovies[5]);
      lPeople[1].AddMovie(lMovies[7]);
         
      lPeople[2].AddMovie(lMovies[0]);
      lPeople[2].AddMovie(lMovies[1]);
      
      lPeople[3].AddMovie(lMovies[2]);
      return (lPeople, lMovies);
   }
   
   public static (IEnumerable<Person>, IEnumerable<Movie>, IEnumerable<Ticket>) InitTicketsWithPersonAndMovie(
      IEnumerable<Person> people,
      IEnumerable<Movie> movies,
      IEnumerable<Ticket> tickets
   ) {
      var lPeople = people.ToList();
      var lMovies = movies.ToList();
      var lTickets = tickets.ToList();
      if(lPeople.Count() != 4 || lMovies.Count() != 8 || lTickets.Count() != 12)
         throw new ArgumentException("Invalid number of people or movies or tickets");
      
      lTickets[0].SetPerson(lPeople[0]); lTickets[0].SetMovie(lMovies[0]);
      lTickets[1].SetPerson(lPeople[0]); lTickets[1].SetMovie(lMovies[2]);
      lTickets[2].SetPerson(lPeople[0]); lTickets[2].SetMovie(lMovies[4]);
      lTickets[3].SetPerson(lPeople[0]); lTickets[3].SetMovie(lMovies[6]);

      lTickets[4].SetPerson(lPeople[1]); lTickets[4].SetMovie(lMovies[1]);
      lTickets[5].SetPerson(lPeople[1]); lTickets[5].SetMovie(lMovies[3]);
      lTickets[6].SetPerson(lPeople[1]); lTickets[6].SetMovie(lMovies[4]);
      lTickets[7].SetPerson(lPeople[1]); lTickets[7].SetMovie(lMovies[5]);

      lTickets[8].SetPerson(lPeople[2]); lTickets[8].SetMovie(lMovies[0]);
      lTickets[9].SetPerson(lPeople[2]); lTickets[9].SetMovie(lMovies[1]);

      lTickets[10].SetPerson(lPeople[3]); lTickets[10].SetMovie(lMovies[2]);
      lTickets[11].SetPerson(lPeople[3]); lTickets[11].SetMovie(lMovies[3]);
      
      return (lPeople, lMovies, lTickets);
   }
   
}