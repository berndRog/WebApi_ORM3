using WebApiOrm.Core.DomainModel.Entities;
using WebApiOrm.Core.DomainModel.NullEntities;
using WebApiOrm.Core.Utilities;
using WebApiOrmTest;
using Xunit;

namespace WebOrmTest.Core.DomainModel.Entities;
public class UserUt {
   
   private readonly Seed _seed = new();
   
   [Fact]
   public void Ctor() {
      // Arrange
      User actual = new User(
         id: _seed.User1.Id,
         userName: _seed.User1.UserName,
         salt:  _seed.User1.Salt,
         password: _seed.User1.Password
      );
      // Assert
      Assert.NotNull(actual);
      Assert.IsType<User>(actual);
      Assert.Equivalent(_seed.User1, actual);
   }
   
   [Fact]
   public void Ctor2(){
      // Arrange
      // Act
      var actual = new User(
         id: _seed.User1.Id,
         userName: _seed.User1.UserName,
         password: "geh1m_"
      );
      // Assert
      Assert.NotNull(actual);
      Assert.IsType<User>(actual);
      Assert.Equivalent(_seed.User1.Id, actual.Id);
      Assert.Equal(_seed.User1.UserName, actual.UserName);
      Assert.True(Hashing.VerifyPassword("geh1m_", actual.Password, actual.Salt ));
   }
   
   [Fact]
   public void GetterUt(){
      // Arrange
      var actual = _seed.User1;
      // Act
      var actualId = actual.Id;
      var actualUserName = actual.UserName;
      var actualSalt = actual.Salt;
      var actualPasssword = actual.Password;
      // Assert
      Assert.Equivalent(_seed.User1, actual);
   }

   [Fact]
   public void SetPersonUt() {
      // Arrange
      var actual = _seed.User1;
      // Act
      actual.SetPerson(_seed.Person1);
      // Assert
     //Assert.Equivalent(_seed.Person1, actual.Person);
      //Assert.Equivalent(_seed.Person1.Id, actual.PersonId);
   }

   [Fact]
   public void SetPersonToNullUt() {
      // Arrange
      var actual = _seed.User1;
      // Act
      actual.SetPerson(NullPerson.Instance);
      // Assert
      //Assert.Equivalent(NullPerson.Instance, actual.Person);
      //Assert.Equivalent(NullPerson.Instance.Id, actual.Person.Id);

   }
}