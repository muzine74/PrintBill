using DBConnection.Entity;
using DBConnection.Entity.Mail;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

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
        public DbSet<EmployeeWork> EmployeeWorks { get; set; }  // Nouvelle entité

        public DbSet<MailCredential> MailCredentials { get; set; }

        public DbSet<WorkType> WorkTypes { get; set; }
        public DbSet<CompanyWork> CompanyWorks { get; set; }
        public DbSet<WorkSchedule> WorkSchedules { get; set; }
        public DbSet<WorkHour> WorkHours { get; set; }

        #endregion

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configuration globale pour les types décimaux
            modelBuilder.ApplyDecimalPrecision();

            ConfigureEmployee(modelBuilder);
            ConfigureCompany(modelBuilder);
            ConfigureWork(modelBuilder);
            ConfigureEmployeeWork(modelBuilder);  // Nouvelle configuration
            ConfigureBilling(modelBuilder);
            ConfigureCredentials(modelBuilder);
            SeedWorkTypes(modelBuilder);
            ConfigureCompanyWorkEmployee(modelBuilder);
            ConfigureWorkPlanning(modelBuilder);
        }

        #region Configurations

        private static void ConfigureEmployee(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>(entity =>
            {
                entity.HasKey(e => e.EmployeeId);

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

                // Relation avec EmployeeWork
                entity.HasMany(e => e.EmployeeWorks)
                      .WithOne(ew => ew.Employee)
                      .HasForeignKey(ew => ew.EmployeeId)
                      .OnDelete(DeleteBehavior.Restrict);
            });
        }

        private static void ConfigureCompany(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Company>(entity =>
            {
                entity.HasKey(c => c.CompanyId);

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

                // Relations avec les travaux
                entity.HasMany(c => c.Works)
                      .WithOne(w => w.Company)
                      .HasForeignKey(w => w.CompanyId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasMany(c => c.CompanyWorks)
                      .WithOne(cw => cw.Company)
                      .HasForeignKey(cw => cw.CompanyId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Client>()
                        .HasKey(c => c.ClientID);

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
                entity.HasKey(w => w.WorkId);

                entity.HasOne(w => w.Company)
                      .WithMany(c => c.Works)
                      .HasForeignKey(w => w.CompanyId)
                      .IsRequired();

                entity.HasOne(w => w.Employee)
                      .WithMany(e => e.Works)
                      .HasForeignKey(w => w.EmployeeId)
                      .IsRequired(false);

                // Configuration des propriétés
                entity.Property(w => w.ClientPrice)
                      .HasPrecision(18, 2);

                entity.HasIndex(w => w.CompanyId);
                entity.HasIndex(w => w.EmployeeId);
                entity.HasIndex(w => w.Workdate);
            });
        }

        private static void ConfigureEmployeeWork(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<EmployeeWork>(entity =>
            {
                entity.HasKey(e => e.EmployeeWorkId);

                // Index pour les recherches fréquentes
                entity.HasIndex(e => e.EmployeeId);
                entity.HasIndex(e => e.CompanyWorkId);
                entity.HasIndex(e => e.WorkDate);
                entity.HasIndex(e => e.Status);
                entity.HasIndex(e => new { e.EmployeeId, e.WorkDate });  // Index composite

                // Relations
                entity.HasOne(e => e.Employee)
                    .WithMany(e => e.EmployeeWorks)
                    .HasForeignKey(e => e.EmployeeId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(e => e.CompanyWork)
                    .WithMany()
                    .HasForeignKey(e => e.CompanyWorkId)
                    .OnDelete(DeleteBehavior.Restrict);

                // Configuration des types décimaux
                entity.Property(e => e.HoursWorked)
                    .HasPrecision(5, 2)  // max 999.99 heures
                    .IsRequired();

                entity.Property(e => e.Amount)
                    .HasPrecision(18, 2)
                    .IsRequired();

                // Configuration des dates
                entity.Property(e => e.WorkDate)
                    .HasColumnType("date");

                // Conversion pour TimeOnly
                var timeOnlyConverter = new ValueConverter<TimeOnly?, TimeSpan?>(
                    v => v.HasValue ? v.Value.ToTimeSpan() : (TimeSpan?)null,
                    v => v.HasValue ? TimeOnly.FromTimeSpan(v.Value) : (TimeOnly?)null);

                entity.Property(e => e.StartTime)
                    .HasConversion(timeOnlyConverter);

                entity.Property(e => e.EndTime)
                    .HasConversion(timeOnlyConverter);

                // Contraintes de vérification
                entity.HasCheckConstraint("CK_EmployeeWork_HoursWorked",
                    "[HoursWorked] > 0");

                entity.HasCheckConstraint("CK_EmployeeWork_Amount",
                    "[Amount] >= 0");

                entity.HasCheckConstraint("CK_EmployeeWork_Status",
                    "[Status] IN (0, 1, 2, 3, 4)");  // Valeurs de l'enum WorkStatus
            });
        }

        private static void ConfigureBilling(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BillHistory>()
                        .HasKey(b => b.billIdentifier);

            modelBuilder.Entity<BillHistory>()
                        .HasIndex(b => b.Id)
                        .IsUnique();

            modelBuilder.Entity<BillHistory>()
                        .Property(b => b.Id)
                        .ValueGeneratedOnAdd();

            modelBuilder.Entity<BillDescription>()
                        .HasKey(d => d.BillDescriptionId);

            modelBuilder.Entity<BillDescription>()
                        .HasOne(d => d.BillHistoris)
                        .WithMany(h => h.BillDescriptions)
                        .HasForeignKey(d => d.BillHistoryId)
                        .HasPrincipalKey(h => h.billIdentifier)
                        .OnDelete(DeleteBehavior.Cascade);

            //modelBuilder.Entity<BillDescription>()
            //            .Property(d => d.Amount)
            //            .HasPrecision(18, 2);
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

        private static void ConfigureCompanyWorkEmployee(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CompanyWork>(entity =>
            {
                entity.HasKey(cw => cw.CompanyWorkId);

                entity.HasOne(cw => cw.Company)
                      .WithMany(c => c.CompanyWorks)
                      .HasForeignKey(cw => cw.CompanyId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(cw => cw.WorkType)
                      .WithMany(wt => wt.CompanyWorks)
                      .HasForeignKey(cw => cw.WorkTypeId)
                      .OnDelete(DeleteBehavior.Restrict);

                // Configuration des taux
                entity.Property(cw => cw.HourlyRate)
                      .HasPrecision(18, 2);

                entity.Property(cw => cw.WeeklyRate)
                      .HasPrecision(18, 2);

                entity.Property(cw => cw.MonthlyRate)
                      .HasPrecision(18, 2);
            });

            // Table de jointure entre Employee et CompanyWork
            modelBuilder.Entity<CompanyWork>()
                .HasMany(cw => cw.Employees)
                .WithMany(e => e.CompanyWorks)
                .UsingEntity<Dictionary<string, object>>(
                    "EmployeeCompanyWork",
                    j => j.HasOne<Employee>()
                          .WithMany()
                          .HasForeignKey("EmployeeId")
                          .OnDelete(DeleteBehavior.Cascade),

                    j => j.HasOne<CompanyWork>()
                          .WithMany()
                          .HasForeignKey("CompanyWorkId")
                          .OnDelete(DeleteBehavior.Cascade),

                    j =>
                    {
                        j.HasKey("EmployeeId", "CompanyWorkId");
                        j.ToTable("EmployeeCompanyWorks");
                        j.HasIndex("CompanyWorkId");
                    });
        }

        private static void ConfigureWorkPlanning(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<WorkSchedule>(entity =>
            {
                entity.HasKey(ws => ws.WorkScheduleId);

                entity.HasOne(ws => ws.CompanyWork)
                      .WithMany(cw => cw.WorkSchedules)
                      .HasForeignKey(ws => ws.CompanyWorkId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.Property(ws => ws.Price)
                      .HasPrecision(18, 2);

                entity.HasIndex(ws => ws.CompanyWorkId);
                entity.HasIndex(ws => ws.DayOfWeek);
            });

            modelBuilder.Entity<WorkHour>(entity =>
            {
                entity.HasKey(wh => wh.WorkHourId);

                entity.HasOne(wh => wh.CompanyWork)
                      .WithMany(cw => cw.WorkHours)
                      .HasForeignKey(wh => wh.CompanyWorkId)
                      .OnDelete(DeleteBehavior.Cascade);

                // Conversion pour TimeOnly
                var timeOnlyConverter = new ValueConverter<TimeOnly, TimeSpan>(
                    v => v.ToTimeSpan(),
                    v => TimeOnly.FromTimeSpan(v));

                entity.Property(wh => wh.StartTime)
                      .HasConversion(timeOnlyConverter);

                entity.Property(wh => wh.EndTime)
                      .HasConversion(timeOnlyConverter);

                entity.HasIndex(wh => wh.CompanyWorkId);
            });
        }

        #endregion

        protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
        {
            base.ConfigureConventions(configurationBuilder);

            // Configuration globale pour TimeOnly
            configurationBuilder.Properties<TimeOnly>()
                .HaveConversion<TimeOnlyConverter>()
                .HaveColumnType("time");

            configurationBuilder.Properties<TimeOnly?>()
                .HaveConversion<TimeOnlyNullableConverter>()
                .HaveColumnType("time");
        }
    }

    #region Helper Classes

    // Convertisseurs pour TimeOnly
    public class TimeOnlyConverter : ValueConverter<TimeOnly, TimeSpan>
    {
        public TimeOnlyConverter() : base(
            timeOnly => timeOnly.ToTimeSpan(),
            timeSpan => TimeOnly.FromTimeSpan(timeSpan))
        { }
    }

    public class TimeOnlyNullableConverter : ValueConverter<TimeOnly?, TimeSpan?>
    {
        public TimeOnlyNullableConverter() : base(
            timeOnly => timeOnly.HasValue ? timeOnly.Value.ToTimeSpan() : (TimeSpan?)null,
            timeSpan => timeSpan.HasValue ? TimeOnly.FromTimeSpan(timeSpan.Value) : (TimeOnly?)null)
        { }
    }

    // Extension method pour la configuration des décimaux
    public static class ModelBuilderExtensions
    {
        public static void ApplyDecimalPrecision(this ModelBuilder modelBuilder)
        {
            foreach (var property in modelBuilder.Model.GetEntityTypes()
                .SelectMany(t => t.GetProperties())
                .Where(p => p.ClrType == typeof(decimal) || p.ClrType == typeof(decimal?)))
            {
                property.SetPrecision(18);
                property.SetScale(2);
            }
        }
    }

    #endregion
}