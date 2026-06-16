using DBConnection.Entity;
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
        public DbSet<CompanyContact> CompanyContacts { get; set; }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Address> Addresses { get; set; }

        public DbSet<CompanyAddress> CompanyAddresses { get; set; }
        public DbSet<EmployeeAddress> EmployeeAddresses { get; set; }
        public DbSet<EmployeeCompany> EmployeeCompanies { get; set; }

        public DbSet<CompanyPricingCalendar> CompanyPricingCalendars { get; set; }
        
        public DbSet<EmployeeCompagnyPricing> EmployeeCompagnyPricings { get; set; }

        public DbSet<EmployeeCredential> EmployeeCredentials { get; set; }

        public DbSet<EmployeeTimeLog> EmployeeTimeLogs { get; set; }

        public DbSet<PointageValidation> PointageValidations { get; set; }

        public DbSet<EmployeeFile> EmployeeFiles { get; set; }

        public DbSet<AppGroup>         AppGroups         { get; set; }
        public DbSet<AppGroupEmployee> AppGroupEmployees { get; set; }
        public DbSet<AppUserRole>      AppUserRoles      { get; set; }
        public DbSet<AppPermission>    AppPermissions    { get; set; }
        public DbSet<AppConfig>        AppConfigs        { get; set; }
        public DbSet<Tenant>           Tenants           { get; set; }



        #endregion

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            ConfigureEmployee(modelBuilder);
            ConfigureCompany(modelBuilder);
            ConfigureBilling(modelBuilder);
            ConfigureAppGroups(modelBuilder);
            ConfigureAppPermissions(modelBuilder);
            ConfigureAppConfig(modelBuilder);
            ConfigureTenants(modelBuilder);
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

                entity.HasMany(e => e.EmployeeFiles)
                      .WithOne(f => f.Employee)
                      .HasForeignKey(f => f.EmployeeId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<EmployeeFile>(entity =>
            {
                entity.HasKey(f => f.EmployeeFileId);
                entity.Property(f => f.FileName).IsRequired().HasMaxLength(500);
                entity.Property(f => f.OriginalName).IsRequired().HasMaxLength(500);
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

            });


        modelBuilder.Entity<CompanyContact>(e =>
            {
                e.ToTable("CompanyContacts");
                e.HasKey(c => c.ContactId);
                e.Property(c => c.Name).IsRequired().HasMaxLength(200);
                e.Property(c => c.Mail).HasMaxLength(200);
                e.Property(c => c.Phone).HasMaxLength(50);
                e.Property(c => c.IsActive).HasDefaultValue(true);
                e.HasOne(c => c.Company)
                 .WithMany(co => co.CompanyContacts)
                 .HasForeignKey(c => c.CompanyId);
            });

            modelBuilder.Entity<EmployeeCompany>()
                        .HasKey(ec => new { ec.EmployeeId, ec.CompanyId });


            
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

        private static void ConfigureAppGroups(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AppGroup>(e =>
            {
                e.ToTable("AppGroups");
                e.HasKey(g => g.GroupId);
                e.Property(g => g.Name).IsRequired().HasMaxLength(100);
                e.Property(g => g.Description).HasDefaultValue(string.Empty);
                e.Property(g => g.PermissionsJson).HasDefaultValue("[]");
            });

            modelBuilder.Entity<AppGroupEmployee>(e =>
            {
                e.ToTable("AppGroupEmployees");
                e.HasKey(ge => new { ge.GroupId, ge.EmployeeId });
                e.HasOne(ge => ge.Group)
                 .WithMany(g => g.GroupEmployees)
                 .HasForeignKey(ge => ge.GroupId);
            });

            modelBuilder.Entity<AppUserRole>(e =>
            {
                e.ToTable("AppUserRoles");
                e.HasKey(r => r.CredentialId);
                e.Property(r => r.Role).HasDefaultValue("USER").HasMaxLength(20);
            });
        }

        private static void ConfigureAppPermissions(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AppPermission>(e =>
            {
                e.ToTable("AppPermissions");
                e.HasKey(p => p.PermissionId);
                e.Property(p => p.Key).IsRequired().HasMaxLength(100);
                e.Property(p => p.Label).IsRequired().HasMaxLength(100);
                e.Property(p => p.Module).IsRequired().HasMaxLength(100);
                e.HasIndex(p => p.Key).IsUnique();
            });

            modelBuilder.Entity<AppPermission>().HasData(
                new AppPermission { PermissionId =  1, Module = "Employés",   Key = "employees.view",   Label = "Voir"       },
                new AppPermission { PermissionId =  2, Module = "Employés",   Key = "employees.edit",   Label = "Modifier"   },
                new AppPermission { PermissionId =  3, Module = "Employés",   Key = "employees.create", Label = "Créer"      },
                new AppPermission { PermissionId =  4, Module = "Employés",   Key = "employees.delete", Label = "Supprimer"  },
                new AppPermission { PermissionId =  5, Module = "Compagnies", Key = "companies.view",   Label = "Voir"       },
                new AppPermission { PermissionId =  6, Module = "Compagnies", Key = "companies.edit",   Label = "Modifier"   },
                new AppPermission { PermissionId =  7, Module = "Factures",   Key = "invoices.view",    Label = "Voir"       },
                new AppPermission { PermissionId =  8, Module = "Factures",   Key = "invoices.edit",    Label = "Modifier"   },
                new AppPermission { PermissionId =  9, Module = "Factures",   Key = "invoices.send",    Label = "Envoyer"    },
                new AppPermission { PermissionId = 10, Module = "Pointage",   Key = "pointage.view",    Label = "Voir"       },
                new AppPermission { PermissionId = 11, Module = "Pointage",   Key = "pointage.validate",Label = "Valider"    },
                new AppPermission { PermissionId = 1001, Module = "Groupes",      Key = "groups.manage",  Label = "Gérer"      },
                new AppPermission { PermissionId = 1002, Module = "Application", Key = "config.manage",  Label = "Configurer" }
            );
        }

        private static void ConfigureAppConfig(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AppConfig>(e =>
            {
                e.ToTable("AppConfigs");
                e.HasKey(c => c.ConfigId);
                e.Property(c => c.ConfigId).ValueGeneratedNever();
                e.Property(c => c.TpsRate).HasColumnType("decimal(6,4)").HasDefaultValue(5m);
                e.Property(c => c.TvqRate).HasColumnType("decimal(6,4)").HasDefaultValue(9.975m);
                e.Property(c => c.LogoPath).HasMaxLength(500);
                e.Property(c => c.CompanyName).HasMaxLength(200);
                e.Property(c => c.CompanyAddress).HasMaxLength(500);
                e.Property(c => c.CompanyPhone).HasMaxLength(50);
                e.Property(c => c.CompanyEmail).HasMaxLength(200);
                e.Property(c => c.SmtpServer).HasMaxLength(200);
                e.Property(c => c.SmtpUser).HasMaxLength(200);
                e.Property(c => c.SmtpPassword).HasMaxLength(200);
                e.Property(c => c.TpsNumber).HasMaxLength(50);
                e.Property(c => c.TvqNumber).HasMaxLength(50);
                e.Property(c => c.ContactName).HasMaxLength(200);
                e.Property(c => c.ContactPhone).HasMaxLength(50);
                e.Property(c => c.ContactEmail).HasMaxLength(200);
                e.Property(c => c.AppVersion).HasMaxLength(50);
            });
        }

        private static void ConfigureTenants(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Tenant>(e =>
            {
                e.ToTable("Tenants");
                e.HasKey(t => t.TenantId);
                e.Property(t => t.Name).IsRequired().HasMaxLength(200);
                e.Property(t => t.Slug).IsRequired().HasMaxLength(100);
                e.HasIndex(t => t.Slug).IsUnique();
                e.Property(t => t.OwnerEmail).HasMaxLength(200);
                e.Property(t => t.Plan).HasDefaultValue("starter").HasMaxLength(50);
            });
        }

        #endregion
    }
}
