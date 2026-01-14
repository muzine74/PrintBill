

using DBConnection.Entity;
using DBConnection.Entity.Mail;
//using DBConnection.Entity.Tax;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.Reflection.Metadata;


namespace DBConnection
{
    public class RamssisCleaningContex : DbContext
    {

        public RamssisCleaningContex(DbContextOptions<RamssisCleaningContex> option) : base(option)//"RamssisCleaningDB"
        {
            // Configurez la chaîne de connexion ici  

        }
        public DbSet<BillHistory> BillHistories { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Provider> Providers { get; set; }
        public DbSet<CompanyAddress> CompanyAddresses { get; set; }
        public DbSet<EmployeeAddress> EmployeeAddresses { get; set; }


        public DbSet<Work> Works { get; set; }
        public DbSet<EmployeeCompany> EmployeeCompanies { get; set; }

       // public DbSet<TaxCredential> TaxCredentials { get; set; }
        public DbSet<MailCredential> MailCredentials { get; set; }
        public DbSet<BillDescription> BillDescriptions { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {



            // Configuration des entités
            modelBuilder.Entity<Employee>(entity =>
            {
                // Relation many-to-many avec Address
                entity.HasMany(e => e.Addresses)
                    .WithMany(a => a.Employees)
                    .UsingEntity<EmployeeAddress>(
                        j => j.HasOne(ea => ea.Address).WithMany(),
                        j => j.HasOne(ea => ea.Employee).WithMany(),
                        j => j.ToTable("EmployeeAddresses")
                              .HasKey(ea => new { ea.EmployeeId, ea.AddressId }));

                // Relation one-to-many avec EmployeeCompany
                entity.HasMany(e => e.EmployeeCompanies)
                    .WithOne(ec => ec.Employee)
                    .HasForeignKey(ec => ec.EmployeeId)
                    .IsRequired();
            });

            modelBuilder.Entity<Company>(entity =>
            {
                // Relation one-to-many avec EmployeeCompany
                entity.HasMany(c => c.EmployeeCompanies)
                    .WithOne(ec => ec.Company)
                    .HasForeignKey(ec => ec.CompanyId)
                    .IsRequired();

                // Relation many-to-many avec Address
                entity.HasMany(c => c.Addresses)
                    .WithMany(a => a.Companies)
                    .UsingEntity<CompanyAddress>(
                        j => j.HasOne(ca => ca.Address).WithMany(),
                        j => j.HasOne(ca => ca.Company).WithMany(),
                        j => j.ToTable("CompanyAddresses")
                              .HasKey(ca => new { ca.CompanyId, ca.AddressId }));


                entity.HasOne(c => c.MailCredential)
                    .WithOne(m => m.Company)
                    .HasForeignKey<MailCredential>(m => m.CompanyId)
                    .IsRequired(false); // Makes the relationship optional

                //entity.HasOne(c => c.TaxCredential)
                //    .WithOne(t => t.Company)
                //    .HasForeignKey<TaxCredential>(t => t.CompanyId)
                //    .IsRequired(false); // Makes the relationship optional
            });

            // Configuration des autres entités
            modelBuilder.Entity<Client>()
                .HasOne(c => c.Company)
                .WithMany(co => co.Clients)
                .HasForeignKey(c => c.CompanyId);

            modelBuilder.Entity<EmployeeCompany>()
                .HasKey(ec => new { ec.EmployeeId, ec.CompanyId });

            modelBuilder.Entity<Work>(entity =>
            {
                entity.HasOne(w => w.Company)
                    .WithMany(c => c.Works)
                    .HasForeignKey(w => w.CompanyId)
                    .IsRequired();

                entity.HasOne(w => w.Employee)
                    .WithMany(e => e.Works);
            });

            modelBuilder.Entity<BillHistory>()
                .HasIndex(e => e.Id)
                .IsUnique();

          



            //modelBuilder.Entity<TaxCredential>(entity =>
            //{
            //    // Primary key
            //    entity.HasKey(e => e.CompanyId);

            //    // Unique index
            //    entity.HasIndex(e => e.CompanyId)
            //          .IsUnique();
            //});

            modelBuilder.Entity<MailCredential>(entity =>
            {
                // Primary key
                entity.HasKey(e => e.CompanyId);

                // Unique index
                entity.HasIndex(e => e.CompanyId)
                      .IsUnique();
            });


            //modelBuilder.Entity<BillHistory>()  
            //            .HasMany(b => b.BillDescriptions) // Un blog a plusieurs articles
            //            .WithOne(a => a.BillHistoris)     // Chaque article a un seul blog
            //            .HasForeignKey(a => a.BillHistoryId); // Spécifie la clé étrangère

            modelBuilder.Entity<BillHistory>()
                .HasKey(b => b.billIdentifier);

                        modelBuilder.Entity<BillDescription>()
                .HasOne(d => d.BillHistoris)
                .WithMany(h => h.BillDescriptions)
                .HasForeignKey(d => d.BillHistoryId)
                .HasPrincipalKey(h => h.billIdentifier); // TRÈS IMPORTANT
        }
    }
}
