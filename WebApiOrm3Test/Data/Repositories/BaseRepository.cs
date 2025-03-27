using System;
using Microsoft.Extensions.DependencyInjection;
using WebApiOrm.Core;
using WebApiOrm.Data;
using WebApiOrmTest;
using WebApiOrmTest.Di;
using WebApiOrmTest.Persistence.Repositories;
using Xunit;
namespace WebOrmTest.Data.Repositories;
[Collection(nameof(SystemTestCollectionDefinition))]
public abstract class BaseRepository {
   
   protected readonly IPeopleRepository _peopleRepository;
   protected readonly IUsersRepository _usersRepository;
   protected readonly ICarsRepository _carsRepository;
   protected readonly IMoviesRepository _moviesRepository;
   protected readonly IDataContext _dataContext;
   protected readonly Seed _seed;

   protected BaseRepository() {
      
      // Test DI-Container
      IServiceCollection services = new ServiceCollection();
      // Add Core, UseCases, Mapper, ...
      //services.AddCore();
      // Add Repositories, Test Databases, ...
      services.AddDataTest();
      // Build ServiceProvider,
      // and use Dependency Injection or Service Locator Pattern
      var serviceProvider = services.BuildServiceProvider()
         ?? throw new Exception("Failed to create an instance of ServiceProvider");

      //-- Service Locator 
      var dbContext = serviceProvider.GetRequiredService<DataContext>()
         ?? throw new Exception("Failed to create DbContext");
      // File based
      dbContext.Database.EnsureDeleted();
      // In-Memory
      // dbContext.Database.OpenConnection();
      dbContext.Database.EnsureCreated();
      
      _dataContext = serviceProvider.GetRequiredService<IDataContext>()
         ?? throw new Exception("Failed to create an instance of IDataContext");

      _peopleRepository = serviceProvider.GetRequiredService<IPeopleRepository>()
         ?? throw new Exception("Failed create an instance of IPeopleRepository");
      _usersRepository = serviceProvider.GetRequiredService<IUsersRepository>()
         ?? throw new Exception("Failed create an instance of IUsersRepository");
      _carsRepository = serviceProvider.GetRequiredService<ICarsRepository>()
         ?? throw new Exception("Failed create an instance of ICarsRepository");
      _moviesRepository = serviceProvider.GetRequiredService<IMoviesRepository>()
         ?? throw new Exception("Failed create an instance of IMoviesRepository");
      _seed = new Seed();
   }
}