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

        public DbSet<EmployeePayment> EmployeePayments { get; set; }
        public DbSet<EmployeePaymentHistory> EmployeePaymentHistories { get; set; }
        public DbSet<BankTransaction> BankTransactions { get; set; }
        public DbSet<BankLexiconEntry> BankLexiconEntries { get; set; }
        public DbSet<Communication> Communications { get; set; }
        public DbSet<Attachment> Attachments { get; set; }
        public DbSet<TelephonyConfig> TelephonyConfigs { get; set; }
        public DbSet<CallRecordingAccess> CallRecordingAccesses { get; set; }
        public DbSet<MailboxConfig> MailboxConfigs { get; set; }

        public DbSet<PointageValidation> PointageValidations { get; set; }
        public DbSet<PointageValidationCompany> PointageValidationCompanies { get; set; }

        public DbSet<Note> Notes { get; set; }
        public DbSet<NoteLink> NoteLinks { get; set; }

        public DbSet<EmployeeFile> EmployeeFiles { get; set; }

        public DbSet<Charge>         Charges         { get; set; }
        public DbSet<ChargeCompany>  ChargeCompanies { get; set; }
        public DbSet<ChargeDocument> ChargeDocuments { get; set; }

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
            ConfigureCharges(modelBuilder);
            ConfigureEmployeePayments(modelBuilder);
            ConfigureNoteLinks(modelBuilder);
            ConfigurePointageValidationCompanies(modelBuilder);
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
            modelBuilder.Entity<EmployeeCompany>().Property(ec => ec.HourlyRate).HasColumnType("decimal(18,2)");

            // Facturation par heure (migration 20261009000000_AddHourlyBilling)
            modelBuilder.Entity<Company>().Property(c => c.BillingMode).HasMaxLength(20);
            modelBuilder.Entity<Company>().Property(c => c.HourlyClientRate).HasColumnType("decimal(18,2)");
            modelBuilder.Entity<Company>().Property(c => c.HourlyEmployeeRate).HasColumnType("decimal(18,2)");
            modelBuilder.Entity<EmployeeTimeLog>().Property(l => l.ClientHourlyRate).HasColumnType("decimal(18,2)");
            // Mode de rémunération employé (migration 20261010000000_AddEmployeePayMode)
            modelBuilder.Entity<Employee>().Property(e => e.PayMode).HasMaxLength(20);
            modelBuilder.Entity<EmployeeCompany>().Property(ec => ec.PayMode).HasMaxLength(20);   // 20261011000000
            // Hiérarchie d'équipe (migration 20261013000000_AddTeamHierarchy) — pas de FK : la cohérence
            // (même tenant, chef d'équipe, sans boucle) est validée par EmployeeService / TeamHierarchy.
            modelBuilder.Entity<Employee>().Property(e => e.IsTeamLead).HasDefaultValue(false);
            modelBuilder.Entity<Employee>().Property(e => e.UiTheme).HasMaxLength(20);   // 20261014000000_AddEmployeeUiTheme
            // Plusieurs plages horaires par jour (migration 20261012000000_AddTimeLogRanges)
            modelBuilder.Entity<EmployeeTimeLog>().Property(l => l.WorkedHours).HasColumnType("decimal(9,2)");
            modelBuilder.Entity<EmployeeTimeLog>().Property(l => l.TimeRanges).HasMaxLength(400);
            modelBuilder.Entity<EmployeeTimeLog>().Property(l => l.PayHourlyRate).HasColumnType("decimal(18,2)");


            
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

        private static void ConfigureCharges(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Charge>(entity =>
            {
                entity.HasKey(c => c.ChargeId);
                entity.Property(c => c.Title).IsRequired().HasMaxLength(300);
                entity.Property(c => c.Amount).HasColumnType("decimal(18,2)");
                entity.Property(c => c.ChargeDate).HasColumnType("date");
                entity.Property(c => c.RecurringThrough).HasColumnType("date");

                entity.HasMany(c => c.ChargeCompanies)
                      .WithOne(cc => cc.Charge)
                      .HasForeignKey(cc => cc.ChargeId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasMany(c => c.ChargeDocuments)
                      .WithOne(cd => cd.Charge)
                      .HasForeignKey(cd => cd.ChargeId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<ChargeCompany>(entity =>
            {
                entity.HasKey(cc => new { cc.ChargeId, cc.CompanyId });
                entity.Property(cc => cc.Percentage).HasColumnType("decimal(5,2)");
                entity.HasOne(cc => cc.Company).WithMany().HasForeignKey(cc => cc.CompanyId);
            });

            modelBuilder.Entity<ChargeDocument>(entity =>
            {
                entity.HasKey(cd => cd.ChargeDocumentId);
                entity.Property(cd => cd.FileName).IsRequired().HasMaxLength(500);
                entity.Property(cd => cd.OriginalName).IsRequired().HasMaxLength(500);
            });
        }

        private static void ConfigurePointageValidationCompanies(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PointageValidationCompany>(entity =>
            {
                entity.HasKey(c => new { c.PointageValidationId, c.CompanyId });
                entity.HasOne(c => c.PointageValidation)
                      .WithMany(v => v.Companies)
                      .HasForeignKey(c => c.PointageValidationId)
                      .OnDelete(DeleteBehavior.Cascade);   // annuler la validation supprime la photo
            });
        }

        private static void ConfigureNoteLinks(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<NoteLink>(entity =>
            {
                entity.HasKey(l => l.NoteLinkId);
                entity.Property(l => l.EntityType).HasMaxLength(20).IsRequired();
                entity.HasIndex(l => new { l.EntityType, l.EntityId });
                entity.HasIndex(l => new { l.NoteId, l.EntityType, l.EntityId }).IsUnique();
                entity.HasOne(l => l.Note)
                      .WithMany(n => n.Links)
                      .HasForeignKey(l => l.NoteId)
                      .OnDelete(DeleteBehavior.Cascade);
            });
        }

        private static void ConfigureEmployeePayments(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<EmployeePayment>(entity =>
            {
                entity.HasKey(p => p.EmployeePaymentId);
                entity.Property(p => p.TpsAmount).HasColumnType("decimal(18,2)");
                entity.Property(p => p.TvqAmount).HasColumnType("decimal(18,2)");
                entity.Property(p => p.AmountPaid).HasColumnType("decimal(18,2)");
                entity.Property(p => p.Note).HasMaxLength(1000);
                entity.Property(p => p.PaidDates).HasMaxLength(4000);
                // Non unique depuis 2026-10 : une semaine peut avoir plusieurs versements (une ligne par versement)
                entity.HasIndex(p => new { p.TenantId, p.EmployeeId, p.PeriodStart, p.PeriodEnd })
                      .HasDatabaseName("IX_EmployeePayments_Tenant_Employee_Period");
                entity.HasOne(p => p.Employee).WithMany().HasForeignKey(p => p.EmployeeId);
            });

            modelBuilder.Entity<BankLexiconEntry>(entity =>
            {
                entity.HasKey(l => l.BankLexiconEntryId);
                entity.Property(l => l.Keyword).HasMaxLength(100);
                entity.Property(l => l.TargetType).HasMaxLength(20);
                entity.Property(l => l.CreatedBy).HasMaxLength(256);
                entity.HasIndex(l => l.TenantId);
            });

            modelBuilder.Entity<Communication>(entity =>
            {
                entity.HasKey(c => c.CommunicationId);
                entity.Property(c => c.TargetType).HasMaxLength(20);
                entity.Property(c => c.Channel).HasMaxLength(20);
                entity.Property(c => c.Direction).HasMaxLength(10);
                entity.Property(c => c.Contact).HasMaxLength(500);
                entity.Property(c => c.Subject).HasMaxLength(300);
                entity.Property(c => c.Body).HasMaxLength(4000);
                entity.Property(c => c.Attachment).HasMaxLength(260);
                entity.Property(c => c.Error).HasMaxLength(1000);
                entity.Property(c => c.CreatedBy).HasMaxLength(256);
                entity.Property(c => c.CallNumber).HasMaxLength(20);
                entity.Property(c => c.CallSid).HasMaxLength(64);
                entity.Property(c => c.CallStatus).HasMaxLength(20);
                entity.Property(c => c.RecordingFile).HasMaxLength(100);
                entity.Property(c => c.ExternalId).HasMaxLength(400);
                entity.HasIndex(c => new { c.TenantId, c.TargetType, c.TargetId, c.OccurredAt })
                      .HasDatabaseName("IX_Communications_Tenant_Target_Date");
                entity.HasIndex(c => new { c.TenantId, c.ExternalId })
                      .HasDatabaseName("IX_Communications_Tenant_ExternalId");
            });

            modelBuilder.Entity<MailboxConfig>(entity =>
            {
                entity.HasKey(m => m.TenantId);
                entity.Property(m => m.TenantId).ValueGeneratedNever();
                entity.Property(m => m.DirectoryId).HasMaxLength(64);
                entity.Property(m => m.ClientId).HasMaxLength(64);
                entity.Property(m => m.ClientSecret).HasMaxLength(2000);
                entity.Property(m => m.Mailbox).HasMaxLength(256);
                entity.Property(m => m.LastError).HasMaxLength(1000);
            });

            modelBuilder.Entity<TelephonyConfig>(entity =>
            {
                entity.HasKey(t => t.TenantId);
                entity.Property(t => t.TenantId).ValueGeneratedNever();
                entity.Property(t => t.AccountSid).HasMaxLength(64);
                entity.Property(t => t.AuthToken).HasMaxLength(1000);
                entity.Property(t => t.ApiKeySid).HasMaxLength(64);
                entity.Property(t => t.ApiKeySecret).HasMaxLength(1000);
                entity.Property(t => t.TwimlAppSid).HasMaxLength(64);
                entity.Property(t => t.CallerNumber).HasMaxLength(20);
                entity.Property(t => t.PublicBaseUrl).HasMaxLength(200);
                entity.Property(t => t.RecordingMode).HasMaxLength(10);
                entity.Property(t => t.RecordingNotice).HasMaxLength(500);
            });

            modelBuilder.Entity<CallRecordingAccess>(entity =>
            {
                entity.HasKey(a => a.CallRecordingAccessId);
                entity.Property(a => a.ListenedBy).HasMaxLength(256);
                entity.HasIndex(a => new { a.TenantId, a.CommunicationId })
                      .HasDatabaseName("IX_CallRecordingAccesses_Tenant_Communication");
            });

            modelBuilder.Entity<Attachment>(entity =>
            {
                entity.HasKey(a => a.AttachmentId);
                entity.Property(a => a.OwnerType).HasMaxLength(20);
                entity.Property(a => a.FileName).HasMaxLength(100);
                entity.Property(a => a.OriginalName).HasMaxLength(260);
                entity.Property(a => a.UploadedBy).HasMaxLength(256);
                entity.HasIndex(a => new { a.TenantId, a.OwnerType, a.OwnerId })
                      .HasDatabaseName("IX_Attachments_Tenant_Owner");
            });

            modelBuilder.Entity<BankTransaction>(entity =>
            {
                entity.HasKey(b => b.BankTransactionId);
                entity.Property(b => b.Description).HasMaxLength(500);
                entity.Property(b => b.Withdrawal).HasColumnType("decimal(18,2)");
                entity.Property(b => b.Deposit).HasColumnType("decimal(18,2)");
                entity.Property(b => b.AccountNumber).HasMaxLength(50);
                entity.Property(b => b.ImportedBy).HasMaxLength(256);
                entity.Property(b => b.ValidatedBy).HasMaxLength(256);
                entity.Property(b => b.SourceFile).HasMaxLength(260);
                entity.HasIndex(b => new { b.TenantId, b.TransactionDate });
            });

            modelBuilder.Entity<EmployeePaymentHistory>(entity =>
            {
                entity.HasKey(h => h.EmployeePaymentHistoryId);
                entity.Property(h => h.PreviousAmount).HasColumnType("decimal(18,2)");
                entity.Property(h => h.NewAmount).HasColumnType("decimal(18,2)");
                entity.Property(h => h.Delta).HasColumnType("decimal(18,2)");
                entity.Property(h => h.TpsAmount).HasColumnType("decimal(18,2)");
                entity.Property(h => h.TvqAmount).HasColumnType("decimal(18,2)");
                entity.Property(h => h.ChangedBy).HasMaxLength(256);
                entity.Property(h => h.PaidDates).HasMaxLength(4000);
                entity.Property(h => h.Note).HasMaxLength(1000);
                entity.HasIndex(h => new { h.TenantId, h.EmployeeId, h.PeriodStart, h.PeriodEnd });
                entity.HasOne(h => h.Employee).WithMany().HasForeignKey(h => h.EmployeeId).OnDelete(DeleteBehavior.NoAction);
            });
        }

        #endregion
    }
}
