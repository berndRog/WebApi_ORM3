// using DeepEqual;
// using DeepEqual.Syntax;
// using WebApiOrm.Core.DomainModel.Entities;
// using WebApiOrmTest;
// using WebApiOrmTest.Persistence.Repositories;
// using WebOrmTest.Data.Repositories;
// using Xunit;
// namespace OrmTest.Data.Repositories;
//
// [Collection(nameof(SystemTestCollectionDefinition))]
// public class UsersRepositoryUt : BaseRepository {
//
//    #region UsersWithPerson
//    [Fact]
//    public void FindByIdUt() {
//       // Arrange
//       _peopleRepository.Add(_seed.Person1);
//       _dataContext.SaveAllChanges();
//       _dataContext.ClearChangeTracker();
//
//       // retrieve person from database to track it
//       var actualPerson = _peopleRepository.FindById(_seed.Person1.Id);
//       Assert.NotNull(actualPerson);
//       actualPerson?.SetUser(_seed.User1); // domain model
//       // add user to database
//       _usersRepository.Add(_seed.User1);
//       _dataContext.SaveAllChanges();
//       _dataContext.ClearChangeTracker();
//       
//       // Act
//       var actual = _usersRepository.FindById(_seed.User1.Id);
//       // Assert
//       // Create a custom comparison that excludes the "Person" property in User.
//       var comparison = new ComparisonBuilder()
//          .IgnoreProperty<User>(u => u.Person)
//          .Create();
//       Assert.True(_seed.User1.IsDeepEqual(actual, comparison));
//    }
//    
//    [Fact]
//    public void SelectAllUt() {
//       // Arrange
//       _peopleRepository.AddRange(_seed.People);
//       _dataContext.SaveAllChanges();
//       _dataContext.ClearChangeTracker();
//       
//       var actualPeople = _peopleRepository.SelectAll();
//       var actualUsers = Seed.InitPeopleWithUser(actualPeople, _seed.Users);
//       
//       _usersRepository.AddRange(actualUsers);
//       _dataContext.SaveAllChanges();
//       _dataContext.ClearChangeTracker();
//      
//       // Act
//       var actual = _usersRepository.SelectAll();
//       
//       // Assert
//       var comparison = new ComparisonBuilder()
//          .IgnoreProperty<User>(u => u.Person)
//          .Create();
//       Assert.True(actual.IsDeepEqual(actual, comparison));
//    }
//    
//    [Fact]
//    public void AddUt() {
//       // Arrange
//       var person = _seed.Person1;
//       _peopleRepository.Add(person);
//       _dataContext.SaveAllChanges();
//       _dataContext.ClearChangeTracker();
//
//       // Act
//       // retrieve person from database to track it
//       var actualPerson = _peopleRepository.FindById(_seed.Person1.Id);
//       Assert.NotNull(actualPerson);
//       var actualUser = _seed.User1;
//       actualPerson?.SetUser(actualUser); // domain model
//       
//       // add user to database
//       _usersRepository.Add(actualUser);
//       _dataContext.SaveAllChanges();
//       _dataContext.ClearChangeTracker();
//
//       // Assert
//       var actual = _usersRepository.FindById(actualUser.Id);
//       var comparison = new ComparisonBuilder()
//          .IgnoreProperty<User>(u => u.Person)
//          .Create();
//       Assert.True(actualUser.IsDeepEqual(actual, comparison));
//    }
//
//    [Fact]
//    public void AddRangeUt() {
//       // Arrange
//       _peopleRepository.AddRange(_seed.People);
//       _dataContext.SaveAllChanges();
//       _dataContext.ClearChangeTracker();
//       
//       // Act
//       var actualPeople = _peopleRepository.SelectAll();
//       var actualUsers = Seed.InitPeopleWithUser(actualPeople, _seed.Users);
//       
//       _usersRepository.AddRange(actualUsers);
//       _dataContext.SaveAllChanges();
//       _dataContext.ClearChangeTracker();
//      
//       // Assert
//       var actual = _usersRepository.SelectAll();
//       var comparison = new ComparisonBuilder()
//          .IgnoreProperty<User>(u => u.Person)
//          .Create();
//       Assert.True(actual.IsDeepEqual(actual, comparison));
//    }
//
//
//    [Fact]
//    public void UpdateUt() {
//       // Arrange
//       _peopleRepository.Add(_seed.Person1);
//       _dataContext.SaveAllChanges();
//       _dataContext.ClearChangeTracker();
//       
//       // Act
//       // retrieve person from database to track it
//       var actualPerson = _peopleRepository.FindById(_seed.Person1.Id);
//       Assert.NotNull(actualPerson);
//       // domain model
//       var user = _seed.User1;
//       actualPerson.SetUser(user);
//       // add user to database
//       _usersRepository.Add(user);
//       _dataContext.SaveAllChanges();
//       _dataContext.ClearChangeTracker();
//       
//       // Assert
//       var actual = _peopleRepository.FindById(_seed.Person1.Id);
//       var comparison = new ComparisonBuilder()
//          .IgnoreProperty<User>(u => u.Person)
//          .Create();
//       Assert.True(actual.IsDeepEqual(actual, comparison));
//    }
//    
//    [Fact]
//    public void RemoveUt() {
//       // Arrange
//       // Arrange
//       var person = _seed.Person1;
//       var user = _seed.User1;
//       person.SetUser(user); // domain model
//       
//       // add person and user to database
//       _usersRepository.Add(user);
//       _dataContext.SaveAllChanges();
//       _dataContext.ClearChangeTracker();
//       
//       // Act
//       _usersRepository.Remove(user);
//       _dataContext.SaveAllChanges();
//       
//       // Assert
//       var actual = _usersRepository.FindById(user.Id);
//       Assert.Null(actual);
//    }
//    #endregion
//
//    
//    
// }