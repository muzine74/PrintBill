using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace DBConnection
{
    public class RamssisCleaningContextFactory : IDesignTimeDbContextFactory<RamssisCleaningContex>
    {
        public RamssisCleaningContex CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<RamssisCleaningContex>();
            optionsBuilder.UseSqlServer("Data Source=DESKTOP-71ON71H\\SQLEXPRESS;Initial Catalog=RamssisCleaningDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False");

            return new RamssisCleaningContex(optionsBuilder.Options);
        }
    }
}