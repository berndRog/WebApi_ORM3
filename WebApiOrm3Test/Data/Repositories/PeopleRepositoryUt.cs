using System;
using System.Collections.Generic;
using System.Linq;
using DeepEqual;
using DeepEqual.Syntax;
using WebApiOrm.Core.DomainModel.Entities;
using WebApiOrmTest;
using WebApiOrmTest.Persistence.Repositories;
using WebOrmTest.Data.Repositories;
using Xunit;
namespace WebOrmTest.Data.Repositories;

[Collection(nameof(SystemTestCollectionDefinition))]
public class PeopleRepositoryUt : SetupRepositories {

   #region PersonOnly
   [Fact]
   public void FindByIdUt() {
      // Arrange
      _peopleRepository.Add(_seed.Person1);
      _dataContext.SaveAllChanges();
      _dataContext.ClearChangeTracker();
      // Act
      var actual = _peopleRepository.FindById(_seed.Person1.Id);
      // Assert
      Assert.Equivalent(_seed.Person1, actual);
   }
   
   [Fact]
   public void AddUt() {
      // Arrange
      var person = _seed.Person1;
      // Act
      _peopleRepository.Add(person);
      _dataContext.SaveAllChanges();
      // Assert
      var actual = _peopleRepository.FindById(person.Id);
      Assert.Equal(person, actual);
   }

   [Fact]
   public void AddRangeUt() {
      // Arrange
      var expected = _seed.People;
      // Act
      _peopleRepository.AddRange(_seed.People);
      _dataContext.SaveAllChanges();
      _dataContext.ClearChangeTracker();
      // Assert
      var actual = _peopleRepository.SelectAll();
      Assert.Equivalent(expected, actual);
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
      actualPerson.Update("Erika","Meier", _seed.Person1.Email, _seed.Person1.Phone);
      // update person in repository
      _peopleRepository.Update(actualPerson);
      _dataContext.SaveAllChanges();
      // Assert
      var actual = _peopleRepository.FindById(_seed.Person1.Id);
      Assert.Equivalent(actualPerson, actual);
   }
   
   [Fact]
   public void RemoveUt() {
      // Arrange
      _peopleRepository.AddRange(_seed.People);
      _dataContext.SaveAllChanges();
      _dataContext.ClearChangeTracker();
      // Act
      _peopleRepository.Remove(_seed.Person1);
      _dataContext.SaveAllChanges();
      // Assert
      var actual = _peopleRepository.FindById(_seed.Person1.Id);
      Assert.Null(actual);
   }
   
   [Fact]
   public void SelectByNameUt() {
      // Arrange
      _peopleRepository.AddRange(_seed.People);
      _dataContext.SaveAllChanges();
      _dataContext.ClearChangeTracker();
      var expected = new List<Person> { _seed.Person1, _seed.Person2 };
      
      // Act
      var actual = _peopleRepository.SelectByName("Muster"); 
      
      // Assert
      Assert.Equivalent(expected, actual);
   }
   
   #endregion

   #region PersonJoinUser
   [Fact]
   public void AddJoinUserUt() {
      // Arrange
      var person = _seed.Person1;
      // add person to repository first
      _peopleRepository.Add(person);  
      _dataContext.SaveAllChanges();
      _dataContext.ClearChangeTracker();
      
      // Act
      var actualPerson = _peopleRepository.FindById(person.Id);
      Assert.NotNull(actualPerson);
      // Domain model
      var user = _seed.User1;
      actualPerson.SetUser(user);   
      // the add user to the repository
      _usersRepository.Add(user);
      _dataContext.SaveAllChanges();
      _dataContext.ClearChangeTracker();
      
      // Arrange
      var actual = _peopleRepository.FindByIdJoinUser(_seed.Person1.Id);
      
      // Assert
      var comparison = new ComparisonBuilder()
         .IgnoreCircularReferences()
         .Create();
      Assert.True(actualPerson.IsDeepEqual(actual, comparison));
   }
   
   [Fact]
   public void FindByIdJoinUserUt() {
      // Arrange
      // domain model: add one user to each person
      _seed.InitUsers();
      // add people and users to repository
      _peopleRepository.AddRange(_seed.People);
      _dataContext.SaveAllChanges();
      _dataContext.ClearChangeTracker();

      // Act
      var actual = _peopleRepository.FindByIdJoinUser(_seed.Person1.Id);      

      // Assert
      var comparison = new ComparisonBuilder()
         .IgnoreCircularReferences()
         .Create();
      Assert.True(_seed.Person1.IsDeepEqual(actual, comparison));
   }
   #endregion
   
   #region PersonJoinCars
   [Fact]
   public void FindByIdJoinCarsUt() {
      // Arrange
      // add person to repository first
      _peopleRepository.Add(_seed.Person1);  
      _dataContext.SaveAllChanges();
      _dataContext.ClearChangeTracker();
      
      // Act
      var person = _peopleRepository.FindById(_seed.Person1.Id);
      Assert.NotNull(person);
      // Domain model
      var car1 = _seed.Car1;
      var car2 = _seed.Car2;
      person.AddCar(car1);
      person.AddCar(car2);
      _dataContext.LogChangeTracker("AddJoinCarsUt");
      // add the cars to the repository
      ticketsRepository.Add(car1);
      ticketsRepository.Add(car2);
      _dataContext.SaveAllChanges();
      _dataContext.ClearChangeTracker();
      
      // Assert 
      var actual = _peopleRepository.FindByIdJoinCars(person.Id);      
      Assert.NotNull(actual);
      
      Console.WriteLine(SetupRepositories.ToPrettyJson("person", person));
     
      Console.WriteLine(SetupRepositories.ToPrettyJson("actual", actual));
      
      
      // Assert
      var comparison = new ComparisonBuilder()
         .IgnoreCircularReferences()
         .Create();
      Assert.True(person.IsDeepEqual(actual, comparison));
   }
   
   [Fact]
   public void DeleteWithCarsCascadingUt() {
      // Arrange
      _peopleRepository.Add(_seed.Person1);
      _dataContext.SaveAllChanges();
      _dataContext.ClearChangeTracker();
      
      var person = _peopleRepository.FindById(_seed.Person1.Id);
      Assert.NotNull(person);
      person.AddCar(_seed.Car1);
      person.AddCar(_seed.Car2);
      ticketsRepository.Add(_seed.Car1);
      ticketsRepository.Add(_seed.Car2);
      _dataContext.SaveAllChanges();
      _dataContext.ClearChangeTracker();

      // Act
      _peopleRepository.Remove(_seed.Person1); 
      _dataContext.SaveAllChanges();

      // Assert
      var actualPerson= _peopleRepository.FindById(_seed.Person1.Id);
      var actualCar1 = ticketsRepository.FindById(_seed.Car1.Id);
      var actualCar2 = ticketsRepository.FindById(_seed.Car2.Id);

      Assert.Null(actualPerson);
      Assert.Null(actualCar1);
      Assert.Null(actualCar2);

   }
   #endregion
   
   
   #region PersonWithMovies
   [Fact]
   public void AddWithMovies_FindByIdJoinMoviesUt() {
      // Arrange
      var person = _seed.Person1;
      // add person to repository first
      _peopleRepository.Add(person);  
      _dataContext.SaveAllChanges();
      _dataContext.ClearChangeTracker();
      
      // Act
      var actualPerson = _peopleRepository.FindById(person.Id);
      Assert.NotNull(actualPerson);
      // Domain model
      actualPerson.AddMovie(_seed.Movie1);
      actualPerson.AddMovie(_seed.Movie3);
      // add the cars to the repository
      _moviesRepository.Add(_seed.Movie1);
      _moviesRepository.Add(_seed.Movie3);
      _dataContext.SaveAllChanges();
      _dataContext.ClearChangeTracker();
      
      // Assert 
      var actual = _peopleRepository.FindByIdJoinMovies(person.Id);      

      // Assert
      var comparison = new ComparisonBuilder()
         .IgnoreCircularReferences()
         .Create();
      Assert.True(actualPerson.IsDeepEqual(actual, comparison));
   }
   
   [Fact]
   public void AddRangeWithMovies_FindByIdJoinMoviesUt() {
      // Arrange
      _peopleRepository.AddRange(_seed.People);
      _dataContext.SaveAllChanges();
      _dataContext.ClearChangeTracker();
      
      // Act
      // domain model
      var people = _peopleRepository.SelectAll();
      Assert.NotNull(people);
      var (updPeople, updMovies) =  Seed.InitPeopleWithMovies(people, _seed.Movies);
      Assert.NotNull(updPeople);
      _moviesRepository.AddRange(updMovies);
      _dataContext.SaveAllChanges();
      _dataContext.ClearChangeTracker();
      
      // Assert
      var comparison = new ComparisonBuilder()
         .IgnoreProperty<Movie>(m => m.People)
         .Create();
      foreach (var expected in updPeople) {
         var actPerson = _peopleRepository.FindByIdJoinMovies(expected.Id);
         Assert.True(actPerson.IsDeepEqual(expected, comparison));

      }
   }
   
   [Fact]
   public void DeleteWithMoviesCascadingUt() {
      // Arrange
      // Arrange
      _peopleRepository.AddRange(_seed.People);
      _dataContext.SaveAllChanges();
      _dataContext.ClearChangeTracker();
      // domain model
      var people = _peopleRepository.SelectAll();
      Assert.NotNull(people);
      var (updPeople, updMovies) =  Seed.InitPeopleWithMovies(people, _seed.Movies);
      Assert.NotNull(updPeople);
      Assert.NotNull(updMovies);
      _moviesRepository.AddRange(updMovies);
      _dataContext.SaveAllChanges();
      _dataContext.ClearChangeTracker();
   
      // Act
      _peopleRepository.Remove(_seed.Person1); 
      _dataContext.SaveAllChanges();
   
      // Assert
      var actualPerson= _peopleRepository.FindById(_seed.Person1.Id);
     
      Assert.Null(actualPerson);
     // Assert.Null(actualCar1);
     // Assert.Null(actualCar2);
   
   }
   #endregion
   
}