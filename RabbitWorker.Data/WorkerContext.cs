using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RabbitWorker.Domain;
using System.Configuration;

namespace RabbitWorker.Data
{
    public class WorkerContext : DbContext
    {
        public DbSet<ProbeData> ProbeData { get; set; }
        string Host { get; set; } = "192.168.1.2";
        string Port { get; set; } = "5432";
        string DatabaseName { get; set; } = "test2";
        string Username { get; set; } = "pi";
        string Password { get; set; } = "C0de";

        public WorkerContext() {
            Init();
        }

        public WorkerContext(DbContextOptions options) :base(options) {
            Init();
        }

        private void Init()
        {
            Host = ConfigurationManager.AppSettings.Get("HostName");
            Port = ConfigurationManager.AppSettings.Get("Port");
            DatabaseName = ConfigurationManager.AppSettings.Get("DatabaseName");
            Username = ConfigurationManager.AppSettings.Get("DBUserName");
            Password = ConfigurationManager.AppSettings.Get("DBPassword");
        }

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

        //const string parabolaString = "Host=localhost;Port=5432;Database=postgres;Username=postgres;Password=!QAZxsw2";
        //string piString = $"Host={Host};Port={port};Database={database};Username={username};Password={password}";

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseNpgsql($"Host={Host};Port={Port};Database={DatabaseName};Username={Username};Password={Password}");
    }
}
