using Microsoft.EntityFrameworkCore;
using WebApiOrm.Core;
using WebApiOrm.Data;
using WebApiOrm.Data.Repositories;
namespace WebApiOrm;

public static class ServiceExtensions {
   
   public static IServiceCollection AddData(
      this IServiceCollection services,
      IConfiguration configuration
   ) {
      // services.AddScoped<IPeopleRepository_Old, PeopleRepository_Old>();
      // services.AddScoped<ICarsRepository_Old, CarsRepository_Old>();
      
      services.AddScoped<IPeopleRepository, PeopleRepository>();
      services.AddScoped<IUsersRepository, UsersRepository>();
      services.AddScoped<ICarsRepository, CarsRepository>();
      services.AddScoped<IMoviesRepository, MoviesRepository>();
      services.AddScoped<ITicketsRepository, TicketsRepository>();
      
      // Add DbContext (Database) to DI-Container
      var (useDatabase, dataSource) = DataContext.EvalDatabaseConfiguration(configuration);
      
      switch (useDatabase) {
         case "Sqlite": 
         case "SqliteInMemory":
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