using System;
using System.Collections.Generic;
using System.Linq;
using WebApiOrm.Core.DomainModel.Entities;
using WebApiOrm.Core.Utilities;
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

   public List<Person> People{ get; private set; }
   public List<User> Users{ get; private set; } 
   public List<Car> Cars{ get; private set; } 
   public List<Movie> Movies{ get; private set; } 
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
      
      People = [Person1, Person2, Person3, Person4];
      Users = [User1, User2, User3, User4];
      Cars = [Car1, Car2, Car3, Car4, Car5, Car6, Car7, Car8];
      Movies = [Movie1, Movie2, Movie3, Movie4, Movie5, Movie6, Movie7, Movie8];
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
      var arrayPeople = people.ToArray();
      var arrayUsers = users.ToArray();
      if(arrayPeople.Count() != 4 || arrayUsers.Count() != 4) 
         throw new ArgumentException("Invalid number of people or users");
      arrayPeople[0].SetUser(arrayUsers[0]);
      arrayPeople[1].SetUser(arrayUsers[1]);
      arrayPeople[2].SetUser(arrayUsers[2]);
      arrayPeople[3].SetUser(arrayUsers[3]);
      return arrayUsers;
   }
   
   public static (IEnumerable<Person>, IEnumerable<Car>) InitPeopleWithCars(
      IEnumerable<Person> people,
      IEnumerable<Car> cars
   ) {
      var arrayPeople = people.ToArray();
      var arrayCars = cars.ToArray();
      if(arrayPeople.Count() != 4 || arrayCars.Count() != 8) 
         throw new ArgumentException("Invalid number of people or cars");
      arrayPeople[0].AddCar(arrayCars[0]);
      arrayPeople[0].AddCar(arrayCars[1]);
      arrayPeople[1].AddCar(arrayCars[2]);
      arrayPeople[1].AddCar(arrayCars[3]);
      arrayPeople[2].AddCar(arrayCars[4]);
      arrayPeople[2].AddCar(arrayCars[5]);
      arrayPeople[2].AddCar(arrayCars[6]);
      arrayPeople[3].AddCar(arrayCars[7]);
      return (arrayPeople, arrayCars);
   }
   
   public static (IEnumerable<Person>, IEnumerable<Movie>) InitPeopleWithMovies(
      IEnumerable<Person> people,
      IEnumerable<Movie> movies
   ) {
      var arrayPeople = people.ToArray();
      var arrayMovies = movies.ToArray();
      if(arrayPeople.Count() != 4 || arrayMovies.Count() != 8) 
         throw new ArgumentException("Invalid number of people or cars");
      
      arrayPeople[0].AddMovie(arrayMovies[0]);
      arrayPeople[0].AddMovie(arrayMovies[2]);
      arrayPeople[0].AddMovie(arrayMovies[4]);
      arrayPeople[0].AddMovie(arrayMovies[6]);

      arrayPeople[1].AddMovie(arrayMovies[1]);
      arrayPeople[1].AddMovie(arrayMovies[3]);
      arrayPeople[1].AddMovie(arrayMovies[5]);
      arrayPeople[1].AddMovie(arrayMovies[7]);
         
      arrayPeople[2].AddMovie(arrayMovies[0]);
      arrayPeople[2].AddMovie(arrayMovies[1]);
      
      arrayPeople[3].AddMovie(arrayMovies[2]);
      return (arrayPeople, arrayMovies);
   }
   
}