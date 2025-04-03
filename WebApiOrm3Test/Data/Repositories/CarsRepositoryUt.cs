﻿using System.Collections.Generic;
using DeepEqual;
using DeepEqual.Syntax;
using WebApiOrm.Core.DomainModel.Entities;
using WebApiOrmTest;
using WebApiOrmTest.Persistence.Repositories;
using WebOrmTest.Data.Repositories;
using Xunit;
namespace WebOrmTest.Data.Repositories;

[Collection(nameof(SystemTestCollectionDefinition))]
public class CarsRepositoryUt : BaseRepository {
   
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
      actualPerson.AddCar(_seed.Car1);
      actualPerson.AddCar(_seed.Car2);
      // add cars to database
      _carsRepository.Add(_seed.Car1);
      _carsRepository.Add(_seed.Car2);
      _dataContext.SaveAllChanges();
      _dataContext.ClearChangeTracker();
      // Act 
      var actual = _carsRepository.FindById(_seed.Car1.Id);
      var comparison = new ComparisonBuilder()
//       .IgnoreCircularReferences()
         .IgnoreProperty<Car>(c=> c.Person)
         .Create();
      Assert.True(_seed.Car1.IsDeepEqual(actual, comparison));
   }
   
   [Fact]
   public void SelectAll() {
      // Arrange
      _peopleRepository.AddRange(_seed.People);
      _dataContext.SaveAllChanges();
      _dataContext.ClearChangeTracker();
      // retrieve people from database to track it
      var people = _peopleRepository.SelectAll();
      Assert.NotNull(people);
      // domain model add cars to people
      var (actualPeople, actualCars) = 
         Seed.InitPeopleWithCars(people,_seed.Cars);
      Assert.NotNull(actualPeople);
      Assert.NotNull(actualCars);
      // add cars to database
      _carsRepository.AddRange(actualCars);
      _dataContext.SaveAllChanges();
      _dataContext.ClearChangeTracker();
      // Act 
      var actual = _carsRepository.SelectAll();
      var comparison = new ComparisonBuilder()
         //       .IgnoreCircularReferences()
         .IgnoreProperty<Car>(c=> c.Person)
         .Create();
      Assert.True(actualCars.IsDeepEqual(actual, comparison));
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
      var car = _seed.Car1;
      actualPerson?.AddCar(car); // car is marked as added, Person remains unchanged from the database perspective 
      // add car to database
      _carsRepository.Add(car);
      _dataContext.SaveAllChanges();
      _dataContext.ClearChangeTracker();
      
      // Assert
      var actual = _carsRepository.FindById(car.Id);
      var comparison = new ComparisonBuilder()
         //       .IgnoreCircularReferences()
         .IgnoreProperty<Car>(c=> c.Person)
         .Create();
      Assert.True(car.IsDeepEqual(actual, comparison));
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
      var (_, actualCars) = Seed.InitPeopleWithCars(actualPeople,_seed.Cars);
      Assert.NotNull(actualCars);
      _carsRepository.AddRange(actualCars);
      _dataContext.SaveAllChanges();
      _dataContext.ClearChangeTracker();
      // Assert
      var actual = _carsRepository.SelectAll();
      var comparison = new ComparisonBuilder()
         //       .IgnoreCircularReferences()
         .IgnoreProperty<Car>(c=> c.Person)
         .Create();
      Assert.True(actualCars.IsDeepEqual(actual, comparison));
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
   
   [Fact]
   public void SelectCarsByPersonIdUt() {
      // Arrange
      _peopleRepository.Add(_seed.Person1);
      _dataContext.SaveAllChanges();
      _dataContext.ClearChangeTracker();
      // retriv person from database to track it
      var actualPerson = _peopleRepository.FindById(_seed.Person1.Id);
      Assert.NotNull(actualPerson);
      // domain model
      actualPerson.AddCar(_seed.Car1);
      actualPerson.AddCar(_seed.Car2);
      // add cars cars to repository and save all cars to database
      _carsRepository.Add(_seed.Car1);
      _carsRepository.Add(_seed.Car2);
      _dataContext.SaveAllChanges();
      _dataContext.ClearChangeTracker();
      var expectedCars = new List<Car> {_seed.Car1, _seed.Car2};
      
      // Act
      var actual = _carsRepository.SelectCarsByPersonId(_seed.Person1.Id);
      
      // Assert
      var comparison = new ComparisonBuilder()
         //       .IgnoreCircularReferences()
         .IgnoreProperty<Car>(c=> c.Person)
         .Create();
      Assert.True(expectedCars.IsDeepEqual(actual, comparison));
   }


}
