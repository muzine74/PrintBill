

using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using Microsoft.Extensions.Options;
using DBConnection.Entity;
using Microsoft.VisualBasic.FileIO;


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
        public DbSet<WorkDay> WorkDays { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Provider> Providers { get; set; }
        public DbSet<CompanyEmployee> CompanyEmployees { get; set; }
        public DbSet<CompanyAddress> CompanyAddresses { get; set; }       
        public DbSet<EmployeeAddress> EmployeeAddresses { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //optionsBuilder.UseSqlServer(@"Server=(Localdb)\Local;Database=CompagnyDB;Trusted_Connection=True;");
            modelBuilder.Entity<Employee>()
                .HasMany(e => e.Addresses)
                .WithMany(a => a.Employees)
                .UsingEntity<EmployeeAddress>(
                    j => j.HasOne(ea => ea.Address).WithMany(),
                    j => j.HasOne(ea => ea.Employee).WithMany(),
                    j =>
                    {
                        j.ToTable("EmployeeAddresses");
                        j.HasKey(ea => new { ea.EmployeeId, ea.AddressId });
                    });

            modelBuilder.Entity<Company>()
                .HasMany(c => c.Addresses)
                .WithMany(a => a.Companies)
                .UsingEntity<CompanyAddress>(
                    j => j.HasOne(ca => ca.Address).WithMany(),
                    j => j.HasOne(ca => ca.Company).WithMany(),
                    j =>
                    {
                        j.ToTable("CompanyAddresses");
                        j.HasKey(ca => new { ca.CompanyId, ca.AddressId });
                    });

            modelBuilder.Entity<Client>()
                .HasOne(c => c.Company)
                .WithMany(co => co.Clients)
                .HasForeignKey(c => c.CompanyId);


            modelBuilder.Entity<Company>()
                .HasMany(c => c.Employees)
                .WithMany(a => a.Companies)
                .UsingEntity<CompanyEmployee>(
                    j => j.HasOne(ca => ca.Employee).WithMany(),
                    j => j.HasOne(ca => ca.Company).WithMany(),
                    j =>
                    {
                        j.ToTable("CompanyEmployee");
                        j.HasKey(ca => new { ca.CompanyId, ca.EmployeeId });
                    });

            modelBuilder.Entity<BillHistory>()
            .HasIndex(e => e.Id)
            .IsUnique(); // Pour éviter les doublons sur la colonne "Nom"
        }
    }
}
