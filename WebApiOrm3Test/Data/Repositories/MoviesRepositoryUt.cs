using DeepEqual;
using DeepEqual.Syntax;
using WebApiOrm.Core.DomainModel.Entities;
using WebApiOrmTest;
using WebApiOrmTest.Persistence.Repositories;
using WebOrmTest.Data.Repositories;
using Xunit;
namespace OrmTest.Data.Repositories;

[Collection(nameof(SystemTestCollectionDefinition))]
public class MoviesRepositoryUt : BaseRepository {
   
   [Fact]
   public void FindById() {
      // Arrange
      _peopleRepository.Add(_seed.Person1);
      _dataContext.SaveAllChanges();
      _dataContext.ClearChangeTracker();
      // retrieve person from database to track it
      var actualPerson = _peopleRepository.FindById(_seed.Person1.Id);
      Assert.NotNull(actualPerson);
      // domain model
      actualPerson.AddMovie(_seed.Movie1);
      //actualPerson.AddMovie(_seed.Movie3);
      // add cars to database
      _moviesRepository.Add(_seed.Movie1);
      //_moviesRepository.Add(_seed.Movie3);
      _dataContext.SaveAllChanges();
      _dataContext.ClearChangeTracker();
      // Act 
      var actual = _moviesRepository.FindById(_seed.Movie1.Id);
      var comparison = new ComparisonBuilder()
         //.IgnoreCircularReferences()
         .IgnoreProperty<Movie>(movie => movie.People)
         .Create();
      Assert.True(_seed.Movie1.IsDeepEqual(actual, comparison));
   }
   
   [Fact]
   public void SelectAll() {
      // Arrange
      _peopleRepository.AddRange(_seed.People);
      _dataContext.SaveAllChanges();
      _dataContext.ClearChangeTracker();
      // retrieve people from database to track it
      var people = _peopleRepository.SelectAll();
      // domain model add cars to people
      var (actualPeople, actualMovies) = 
         Seed.InitPeopleWithMovies(people,_seed.Movies);
      // add cars to database
      _moviesRepository.AddRange(actualMovies);
      _dataContext.SaveAllChanges();
      _dataContext.ClearChangeTracker();
      // Act 
      var actual = _moviesRepository.SelectAll();
      var comparison = new ComparisonBuilder()
         //       .IgnoreCircularReferences()
         .IgnoreProperty<Movie>(movie => movie.People)
         .Create();
      Assert.True(actualMovies.IsDeepEqual(actual, comparison));
   }
   
   [Fact]
   public void AddUt() {
      // Arrange
      var person = _seed.Person1;
      _peopleRepository.Add(person);
      _dataContext.SaveAllChanges();
      _dataContext.ClearChangeTracker();
      
      // Act
      // retrieve person from database which is tracked
      var actualPerson = _peopleRepository.FindById(person.Id);
      Assert.NotNull(actualPerson);
      // domain model
      var movie = _seed.Movie1;
      actualPerson?.AddMovie(movie); // movie is marked as added, Person remains unchanged from the database perspective 
      // add car to database
      _moviesRepository.Add(movie);
      _dataContext.SaveAllChanges();
      _dataContext.ClearChangeTracker();
      
      // Assert
      var actual = _moviesRepository.FindById(movie.Id);
      var comparison = new ComparisonBuilder()
         //       .IgnoreCircularReferences()
         .IgnoreProperty<Movie>(movie => movie.People)
         .Create();
      Assert.True(movie.IsDeepEqual(actual, comparison));
   }
   
   [Fact]
   public void AddRangeUt() {
      // Arrange
      _peopleRepository.AddRange(_seed.People);
      _dataContext.SaveAllChanges();
      _dataContext.ClearChangeTracker();
      // Act
      var actualPeople = _peopleRepository.SelectAll();
      var (_, actualMovies) = Seed.InitPeopleWithMovies(actualPeople,_seed.Movies);
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
   public void UpdateUt() {
      // Arrange
      _peopleRepository.Add(_seed.Person1);
      _dataContext.SaveAllChanges();
      _dataContext.ClearChangeTracker();
      
      // Act
      // retrieve person from database to track it
      var actualPerson = _peopleRepository.FindById(_seed.Person1.Id);
      Assert.NotNull(actualPerson);
      // domain model
      var car = _seed.Car1;
      actualPerson.AddCar(car);
      // add car to database
      _carsRepository.Add(car);
      _dataContext.SaveAllChanges();
      _dataContext.ClearChangeTracker();
      
      // Assert
      var actual = _carsRepository.FindById(_seed.Car1.Id);
      var comparison = new ComparisonBuilder()
         .IgnoreProperty<Car>(c => c.Person)
         .Create();
      Assert.True(car.IsDeepEqual(actual, comparison));
   }
   
   [Fact]
   public void RemoveUt() {
      // Arrange
      var person = _seed.Person1;
      var car1 = _seed.Car1;
      var car2 = _seed.Car2;
      person.AddCar(car1);
      person.AddCar(car2);
      
      // add person and cars to database
      _peopleRepository.Add(person);
      _dataContext.SaveAllChanges();
      _dataContext.ClearChangeTracker();
      
      // Act
      _carsRepository.Remove(car1);
      _dataContext.SaveAllChanges();
      
      // Assert
      var actual = _carsRepository.FindById(car1.Id);
      Assert.Null(actual);
   }

}
