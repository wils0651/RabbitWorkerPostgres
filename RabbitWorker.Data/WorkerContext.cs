using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RabbitWorker.Domain;

namespace RabbitWorker.Data
{
    public class WorkerContext : DbContext
    {

        public WorkerContext() { }

        public WorkerContext(DbContextOptions options) :base(options) { }

        public DbSet<ProbeData> ProbeData { get; set; }

        public static readonly ILoggerFactory ConsoleLoggerFactory = LoggerFactory.Create(builder =>
        {
            builder
            .AddFilter((category, level) => category == DbLoggerCategory.Database.Command.Name && level == LogLevel.Information)
            ;//.AddConsole();
        });

        // protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        // {
        //     if (!optionsBuilder.IsConfigured)
        //     {
        //         optionsBuilder
        //             .UseLoggerFactory(ConsoleLoggerFactory)
        //             .UseSqlServer("Data Source = (localdb)\\MSSQLLocalDB; Initial Catalog = ProbeData");
        //     }
        // }

        const string parabolaString = "Host=localhost;Port=5432;Database=postgres;Username=postgres;Password=!QAZxsw2";
        const string piString = "Host=192.168.1.2;Port=5432;Database=test2;Username=pi;Password=C0de";

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseNpgsql(piString);
    }
}
