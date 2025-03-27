using Microsoft.EntityFrameworkCore;
using WebApiOrm.Core;
using WebApiOrm.Data;
using WebApiOrm.Data.Repositories_refactored;
using WebApiOrm.Data.Repositories;
namespace WebApiOrm;

public static class ServiceExtensions {
   
   public static IServiceCollection AddData(
      this IServiceCollection services,
      IConfiguration configuration
   ) {
      // services.AddScoped<IPeopleRepository, PeopleRepository>();
      // services.AddScoped<IUsersRepository, UsersRepository>();
      // services.AddScoped<ICarsRepository, CarsRepository>();
      
      services.AddScoped<IPeopleRepository, PeopleRepository_refactored>();
      services.AddScoped<IUsersRepository, UsersRepository_refactored>();
      services.AddScoped<ICarsRepository, CarsRepository_refactored>();
      services.AddScoped<IMoviesRepository, MoviesRepository_refactored>();
      
      // Add DbContext (Database) to DI-Container
      var (useDatabase, dataSource) = DataContext.EvalDatabaseConfiguration(configuration);
      
      switch (useDatabase) {
         case "Sqlite":
            services.AddDbContext<IDataContext, DataContext>(options => 
               options.UseSqlite(dataSource)
            );
            break;
         default:
            throw new Exception("appsettings.json UseDatabase not available");
      }
      return services;
   }
   
}