using DBConnection.Entity;
using DBConnection.Entity.Mail;
using Microsoft.EntityFrameworkCore;

namespace DBConnection
{
    public class RamssisCleaningContex : DbContext
    {
        public RamssisCleaningContex(DbContextOptions<RamssisCleaningContex> options)
            : base(options)
        {
        }

        #region DbSets

        public DbSet<BillHistory> BillHistories { get; set; }
        public DbSet<BillDescription> BillDescriptions { get; set; }

        public DbSet<Company> Companies { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Provider> Providers { get; set; }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Address> Addresses { get; set; }

        public DbSet<CompanyAddress> CompanyAddresses { get; set; }
        public DbSet<EmployeeAddress> EmployeeAddresses { get; set; }
        public DbSet<EmployeeCompany> EmployeeCompanies { get; set; }

        public DbSet<Work> Works { get; set; }

        public DbSet<MailCredential> MailCredentials { get; set; }

        public DbSet<WorkType> WorkTypes  { get; set; }

        public DbSet<CompanyPricingCalendar> CompanyPricingCalendars { get; set; }
        
        public DbSet<EmployeeCompagnyPricing> EmployeeCompagnyPricings { get; set; }

        public DbSet<EmployeeCredential> EmployeeCredentials { get; set; }





        #endregion

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            ConfigureEmployee(modelBuilder);
            ConfigureCompany(modelBuilder);
            ConfigureWork(modelBuilder);
            ConfigureBilling(modelBuilder);
            ConfigureCredentials(modelBuilder);
            SeedWorkTypes(modelBuilder);
            
        }

        #region Configurations

        private static void ConfigureEmployee(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>(entity =>
            {
                entity.HasMany(e => e.Addresses)
                      .WithMany(a => a.Employees)
                      .UsingEntity<EmployeeAddress>(
                          j => j.HasOne(ea => ea.Address).WithMany(),
                          j => j.HasOne(ea => ea.Employee).WithMany(),
                          j =>
                          {
                              j.ToTable("EmployeeAddresses");
                              j.HasKey(ea => new { ea.EmployeeId, ea.AddressId });
                          });

                entity.HasMany(e => e.EmployeeCompanies)
                      .WithOne(ec => ec.Employee)
                      .HasForeignKey(ec => ec.EmployeeId)
                      .IsRequired();
            });
        }

        private static void ConfigureCompany(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Company>(static entity =>
            {
                entity.HasMany(c => c.EmployeeCompanies)
                      .WithOne(ec => ec.Company)
                      .HasForeignKey(ec => ec.CompanyId)
                      .IsRequired();

                entity.HasMany(c => c.Addresses)
                      .WithMany(a => a.Companies)
                      .UsingEntity<CompanyAddress>(
                          j => j.HasOne(ca => ca.Address).WithMany(),
                          j => j.HasOne(ca => ca.Company).WithMany(),
                          j =>
                          {
                              j.ToTable("CompanyAddresses");
                              j.HasKey(ca => new { ca.CompanyId, ca.AddressId });
                          });

                entity.HasOne(c => c.MailCredential)
                      .WithOne(m => m.Company)
                      .HasForeignKey<MailCredential>(m => m.CompanyId)
                      .IsRequired(false);

                //////// Configure the one-to-one relationship between Company and WorkType
                entity.HasOne(c => c.WorkType)
                      .WithMany(m => m.Companies)
                      .HasForeignKey(m => m.WorkTypeId)
                      .IsRequired(false);  // Relation optionnelle;

            });


        modelBuilder.Entity<Client>()
                        .HasOne(c => c.Company)
                        .WithMany(co => co.Clients)
                        .HasForeignKey(c => c.CompanyId);

            modelBuilder.Entity<EmployeeCompany>()
                        .HasKey(ec => new { ec.EmployeeId, ec.CompanyId });


            
        }






        private static void ConfigureWork(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Work>(entity =>
            {
                entity.HasOne(w => w.Company)
                      .WithMany(c => c.Works)
                      .HasForeignKey(w => w.CompanyId)
                      .IsRequired();

                entity.HasOne(w => w.Employee)
                      .WithMany(e => e.Works);
            });
        }

        private static void ConfigureBilling(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BillHistory>()
                        .HasKey(b => b.billIdentifier);

            modelBuilder.Entity<BillHistory>()
                        .HasIndex(b => b.Id)
                        .IsUnique();

            modelBuilder.Entity<BillDescription>()
                        .HasOne(d => d.BillHistoris)
                        .WithMany(h => h.BillDescriptions)
                        .HasForeignKey(d => d.BillHistoryId)
                        .HasPrincipalKey(h => h.billIdentifier);
        }

        private static void ConfigureCredentials(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MailCredential>(entity =>
            {
                entity.HasKey(e => e.CompanyId);
                entity.HasIndex(e => e.CompanyId).IsUnique();
            });
        }

        private static void SeedWorkTypes(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<WorkType>().HasData(
                new WorkType { WorkTypeId = 1, Name = "Par visite" },
                new WorkType { WorkTypeId = 2, Name = "Hebdomadaire" },
                new WorkType { WorkTypeId = 3, Name = "Bi-hebdomadaire" },
                new WorkType { WorkTypeId = 4, Name = "Bi-mensuel" },
                new WorkType { WorkTypeId = 5, Name = "Mensuel" },
                new WorkType { WorkTypeId = 6, Name = "Horaire" }
            );
        }

        #endregion
    }
}
