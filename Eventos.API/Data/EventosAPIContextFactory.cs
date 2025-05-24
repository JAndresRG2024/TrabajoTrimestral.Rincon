using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

public class EventosAPIContextFactory : IDesignTimeDbContextFactory<EventosAPIContext>
{
    public EventosAPIContext CreateDbContext(string[] args)
    {
        var config = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();

        var optionsBuilder = new DbContextOptionsBuilder<EventosAPIContext>();
        
        //var connectionString = config.GetConnectionString("EventosAPIContextPostgres");
        var connectionString = config.GetConnectionString("EventosAPIContextSqlServer");

        //Configurar segun la base de  datos a usar
        //optionsBuilder.UseNpgsql(connectionString);
        optionsBuilder.UseSqlServer(connectionString);


        return new EventosAPIContext(optionsBuilder.Options);
    }
}
