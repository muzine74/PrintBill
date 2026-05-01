using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace DBConnection
{
    // Utilisée uniquement par les outils EF (dotnet ef migrations add/update).
    // Lire la connection string depuis appsettings.json du projet de démarrage.
    public class RamssisCleaningContextFactory : IDesignTimeDbContextFactory<RamssisCleaningContex>
    {
        public RamssisCleaningContex CreateDbContext(string[] args)
        {
            var appSettingsPath = FindAppSettings(Directory.GetCurrentDirectory())
                ?? throw new InvalidOperationException(
                    "appsettings.json introuvable. " +
                    "Lancez 'dotnet ef' avec --startup-project pointant vers le projet TimeGuard.");

            var config = new ConfigurationBuilder()
                .SetBasePath(Path.GetDirectoryName(appSettingsPath)!)
                .AddJsonFile("appsettings.json", optional: false)
                .AddJsonFile("appsettings.Development.json", optional: true)
                .Build();

            var connectionString = config.GetConnectionString("Default")
                ?? throw new InvalidOperationException(
                    "ConnectionStrings:Default manquant dans appsettings.json.");

            var optionsBuilder = new DbContextOptionsBuilder<RamssisCleaningContex>();
            optionsBuilder.UseSqlServer(connectionString);

            return new RamssisCleaningContex(optionsBuilder.Options);
        }

        private static string? FindAppSettings(string startDir)
        {
            var dir = new DirectoryInfo(startDir);
            while (dir != null)
            {
                var candidate = Path.Combine(dir.FullName, "appsettings.json");
                if (File.Exists(candidate))
                    return candidate;
                dir = dir.Parent;
            }
            return null;
        }
    }
}