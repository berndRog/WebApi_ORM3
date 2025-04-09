using DeepEqual;
using DeepEqual.Syntax;
using WebApiOrm.Core.DomainModel.Entities;
using WebApiOrmTest;
using WebApiOrmTest.Persistence.Repositories;
using WebOrmTest.Data.Repositories;
using Xunit;
namespace WebOrmTest.Data.Repositories;

[Collection(nameof(SystemTestCollectionDefinition))]
public class TicketsRepositoryUt : SetupRepositories {
   
   [Fact]
   public void FindById() {
      // Arrange
      _peopleRepository.Add(_seed.Person1);
      _moviesRepository.Add(_seed.Movie1);
      _dataContext.SaveAllChanges();
      _dataContext.ClearChangeTracker();
      
      // retrieve person and movie from database to track it
      var actualPerson = _peopleRepository.FindById(_seed.Person1.Id);
      var actualMovie = _moviesRepository.FindById(_seed.Movie1.Id);
      Assert.NotNull(actualPerson);
      Assert.NotNull(actualMovie);
      
      // Act
      var ticket = _seed.Ticket1;
      ticket.SetMovie(actualMovie);
      // domain model
      actualPerson.AddTicket(actualMovie, ticket);
      // add ticket and movie to the repositories, not the person and save to database
      _ticketsRepository.Add(ticket);
      _dataContext.SaveAllChanges();
      _dataContext.ClearChangeTracker();
      
      // Assert
      var actualTicket = _ticketsRepository.FindById(_seed.Ticket1.Id);
      var comparison = new ComparisonBuilder()
         //.IgnoreCircularReferences()
         .IgnoreProperty<Ticket>(ticket => ticket.Person)  // check PersonId only
         .IgnoreProperty<Ticket>(ticket => ticket.Movie)   // check MovieId only
         .Create();
      Assert.True(ticket.IsDeepEqual(actualTicket, comparison));
   }
   
   [Fact]
   public void SelectAll() {
      // Arrange
      _peopleRepository.AddRange(_seed.People);
      _moviesRepository.AddRange(_seed.Movies);
      _dataContext.SaveAllChanges();
      _dataContext.ClearChangeTracker();
      
      // retrieve people from database to track it
      var people = _peopleRepository.SelectAll();
      var movies = _moviesRepository.SelectAll();
      Assert.NotNull(people);
      Assert.NotNull(movies);
      
      // domain model add cars to people
      var (_, _, actualTickets) = 
         Seed.InitTicketsWithPersonAndMovie(people, movies, _seed.Tickets);
      Assert.NotNull(actualTickets);
      // add tickets to the repository and save to database
      _ticketsRepository.AddRange(actualTickets);
      _dataContext.SaveAllChanges();
      _dataContext.ClearChangeTracker();

      // Act 
      var actual = _ticketsRepository.SelectAll();
      var comparison = new ComparisonBuilder()
         //       .IgnoreCircularReferences()
         .IgnoreProperty<Ticket>(ticket => ticket.Person)  // check PersonId only
         .IgnoreProperty<Ticket>(ticket => ticket.Movie)   // check MovieId only
         .Create();
      Assert.True(actualTickets.IsDeepEqual(actual, comparison));
   }
   
   [Fact]
   public void AddUt() {
      // Arrange
      _peopleRepository.Add(_seed.Person1);
      _moviesRepository.Add(_seed.Movie1);
      _dataContext.SaveAllChanges();
      _dataContext.ClearChangeTracker();
      
      // Act
      // retrieve person from database which is tracked
      var actualPerson = _peopleRepository.FindById(_seed.Person1.Id);
      var actualMovie = _moviesRepository.FindById(_seed.Movie1.Id);
      Assert.NotNull(actualPerson);
      // domain model
      var ticket = _seed.Ticket1;
      actualPerson?.AddTicket(actualMovie,ticket);  
      // add car to database
      _ticketsRepository.Add(ticket);
      _dataContext.SaveAllChanges();
      _dataContext.ClearChangeTracker();
      
      // Assert
      var actual = _ticketsRepository.FindById(ticket.Id);
      var comparison = new ComparisonBuilder()
         //       .IgnoreCircularReferences()
         .IgnoreProperty<Ticket>(ticket => ticket.Person)
         .IgnoreProperty<Ticket>(ticket => ticket.Movie)
         .Create();
      Assert.True(ticket.IsDeepEqual(actual, comparison));
   }
   
   [Fact]
   public void AddRangeUt() {
      // Arrange
      _peopleRepository.AddRange(_seed.People);
      _dataContext.SaveAllChanges();
      _dataContext.ClearChangeTracker();
      // Act
      var actualPeople = _peopleRepository.SelectAll();
      Assert.NotNull(actualPeople);
      var (_, actualMovies) = Seed.InitPeopleWithMovies(actualPeople,_seed.Movies);
      Assert.NotNull(actualMovies);
      _moviesRepository.AddRange(actualMovies);
      _dataContext.SaveAllChanges();
      _dataContext.ClearChangeTracker();
      // Assert
      var actual = _moviesRepository.SelectAll();
      var comparison = new ComparisonBuilder()
         //       .IgnoreCircularReferences()
         .IgnoreProperty<Movie>(movie => movie.People)
         .Create();
      Assert.True(actualMovies.IsDeepEqual(actual, comparison));
   }
   
   [Fact]
   public void RemoveUt() {
      // Arrange
      // domain model
      _seed.Person1.AddTicket(_seed.Movie1, _seed.Ticket1);
      // repositories and database
      _peopleRepository.Add(_seed.Person1);
      _moviesRepository.Add(_seed.Movie1);
      _ticketsRepository.Add(_seed.Ticket1);
      _dataContext.SaveAllChanges();
      _dataContext.ClearChangeTracker();
      
      // Act
      var actualPerson = _peopleRepository.FindById(_seed.Person1.Id);
      var actualMovie = _moviesRepository.FindById(_seed.Movie1.Id);
      var actualTicket = _ticketsRepository.FindById(_seed.Ticket1.Id);
      Assert.NotNull(actualPerson);
      Assert.NotNull(actualMovie);
      Assert.NotNull(actualTicket);
      // domain model
      actualPerson.RemoveTicket(actualMovie, actualTicket);
      _ticketsRepository.Remove(actualTicket);
      _dataContext.SaveAllChanges();
      _dataContext.ClearChangeTracker();
      
      
      // Assert
      var actual = ticketsRepository.FindById(_seed.Ticket1.Id);
      Assert.Null(actual);
   }

}
