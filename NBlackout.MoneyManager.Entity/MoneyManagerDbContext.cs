using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.Entity.Infrastructure.Annotations;
using System.Linq;
using System.Threading;
using EntityFramework.DynamicFilters;
using NBlackout.MoneyManager.Models;

namespace NBlackout.MoneyManager.Entity
{
    public class MoneyManagerDbContext : DbContext
    {
        public virtual DbSet<UserCustomerModel> UserCustomers { get; set; }
        public virtual DbSet<CustomerModel> Customers { get; set; }
        public virtual DbSet<AccountModel> Accounts { get; set; }
        public virtual DbSet<AccountCategoryModel> AccountCategories { get; set; }
        public virtual DbSet<TransactionModel> Transactions { get; set; }
        public virtual DbSet<AutomationElementModel> AutomationElements { get; set; }
        public virtual DbSet<AutomationElementTagModel> AutomationElementTags { get; set; }
        public virtual DbSet<OrganizationModel> Organizations { get; set; }
        public virtual DbSet<OrganizationCategoryModel> OrganizationCategories { get; set; }
        public virtual DbSet<TransactionForecastModel> TransactionForecasts { get; set; }
        public virtual DbSet<TransactionForecastHitModel> TransactionForecastHits { get; set; }
        public virtual DbSet<ImportHistoryModel> ImportHistories { get; set; }

        public MoneyManagerDbContext()
            : this("MoneyManagerContext")
        {
        }

        public MoneyManagerDbContext(string nameOrConnectionString)
            : base(nameOrConnectionString)
        {
            Configuration.ProxyCreationEnabled = false;

            Database.SetInitializer(new MoneyManagerDatabaseInitializer());
        }

        public static MoneyManagerDbContext Create(string nameOrConnectionString)
        {
            return new MoneyManagerDbContext(nameOrConnectionString);
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            if (modelBuilder == null)
                throw new ArgumentNullException(nameof(modelBuilder));

            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<CustomerModel>().ToTable("Customer");
            modelBuilder.Entity<CustomerModel>().Property(t => t.FirstName).IsRequired();
            modelBuilder.Entity<CustomerModel>().Property(t => t.LastName).IsRequired();
            modelBuilder.Filter("Customer_Visible", (CustomerModel t, string userName) => t.UserCustomers.Any(uc => uc.UserName == userName), () => Thread.CurrentPrincipal.Identity.Name);

            modelBuilder.Entity<UserCustomerModel>().ToTable("UserCustomer");
            modelBuilder.Entity<UserCustomerModel>().Property(t => t.UserName).IsRequired().HasMaxLength(256).HasColumnAnnotation(IndexAnnotation.AnnotationName, new IndexAnnotation(new IndexAttribute("IX_UserName_CustomerId") { IsUnique = true, Order = 1 }));
            modelBuilder.Entity<UserCustomerModel>().Property(t => t.CustomerId).HasColumnAnnotation(IndexAnnotation.AnnotationName, new IndexAnnotation(new IndexAttribute("IX_UserName_CustomerId") { IsUnique = true, Order = 2 }));

            modelBuilder.Entity<AccountModel>().ToTable("Account");
            modelBuilder.Entity<AccountModel>().Property(t => t.Number).IsRequired().HasMaxLength(450).HasColumnAnnotation(IndexAnnotation.AnnotationName, new IndexAnnotation(new IndexAttribute("IX_Number") { IsUnique = true }));
            modelBuilder.Filter("Account_Enabled", (AccountModel t) => t.Enabled);
            modelBuilder.Filter("Account_Visible", (AccountModel t, string userName) => (!t.CustomerId.HasValue && t.ImportHistoryId.HasValue && t.ImportHistory.UserName == userName) || t.Customer.UserCustomers.Any(uc => uc.UserName == userName), () => Thread.CurrentPrincipal.Identity.Name);

            modelBuilder.Entity<AccountCategoryModel>().ToTable("AccountCategory");
            modelBuilder.Entity<AccountCategoryModel>().Property(t => t.Label).IsRequired().HasMaxLength(450).HasColumnAnnotation(IndexAnnotation.AnnotationName, new IndexAnnotation(new IndexAttribute("IX_Label") { IsUnique = true }));

            modelBuilder.Entity<TransactionModel>().ToTable("Transaction");
            modelBuilder.Entity<TransactionModel>().Property(t => t.Number).IsRequired().HasMaxLength(450).HasColumnAnnotation(IndexAnnotation.AnnotationName, new IndexAnnotation(new IndexAttribute("IX_Number") { IsUnique = true }));
            modelBuilder.Filter("Transaction_Visible", (TransactionModel t, string userName) => t.Account.Customer.UserCustomers.Any(uc => uc.UserName == userName), () => Thread.CurrentPrincipal.Identity.Name);

            modelBuilder.Entity<AutomationElementModel>().ToTable("AutomationElement");
            modelBuilder.Entity<AutomationElementModel>().Property(t => t.OrganizationId).HasColumnAnnotation(IndexAnnotation.AnnotationName, new IndexAnnotation(new IndexAttribute("IX_OrganizationId_AccountId") { IsUnique = true, Order = 1 }));
            modelBuilder.Entity<AutomationElementModel>().Property(t => t.AccountId).HasColumnAnnotation(IndexAnnotation.AnnotationName, new IndexAnnotation(new IndexAttribute("IX_OrganizationId_AccountId") { IsUnique = true, Order = 2 }));
            modelBuilder.Filter("AutomationElement_Visible", (AutomationElementModel t, string userName) => t.Account.Customer.UserCustomers.Any(uc => uc.UserName == userName), () => Thread.CurrentPrincipal.Identity.Name);

            modelBuilder.Entity<AutomationElementTagModel>().ToTable("AutomationElementTag");
            modelBuilder.Entity<AutomationElementTagModel>().Property(t => t.AutomationElementId).HasColumnAnnotation(IndexAnnotation.AnnotationName, new IndexAnnotation(new IndexAttribute("IX_AutomationElementId_Tag") { IsUnique = true, Order = 1 }));
            modelBuilder.Entity<AutomationElementTagModel>().Property(t => t.Label).IsRequired().HasMaxLength(450).HasColumnAnnotation(IndexAnnotation.AnnotationName, new IndexAnnotation(new IndexAttribute("IX_AutomationElementId_Tag") { IsUnique = true, Order = 2 }));

            modelBuilder.Entity<OrganizationModel>().ToTable("Organization");
            modelBuilder.Entity<OrganizationModel>().Property(t => t.Label).IsRequired();

            modelBuilder.Entity<OrganizationCategoryModel>().ToTable("OrganizationCategory");
            modelBuilder.Entity<OrganizationCategoryModel>().Property(t => t.Label).IsRequired();

            modelBuilder.Entity<TransactionForecastModel>().ToTable("TransactionForecast");
            modelBuilder.Entity<TransactionForecastModel>().Property(t => t.AccountId).HasColumnAnnotation(IndexAnnotation.AnnotationName, new IndexAnnotation(new IndexAttribute("IX_AccountId_OrganizationId") { IsUnique = true, Order = 1 }));
            modelBuilder.Entity<TransactionForecastModel>().Property(t => t.OrganizationId).HasColumnAnnotation(IndexAnnotation.AnnotationName, new IndexAnnotation(new IndexAttribute("IX_AccountId_OrganizationId") { IsUnique = true, Order = 2 }));
            modelBuilder.Filter("TransactionForecast_Visible", (TransactionForecastModel t, string userName) => t.Account.Customer.UserCustomers.Any(uc => uc.UserName == userName), () => Thread.CurrentPrincipal.Identity.Name);

            modelBuilder.Entity<TransactionForecastHitModel>().ToTable("TransactionForecastHit");
            modelBuilder.Filter("TransactionForecastHit_Visible", (TransactionForecastHitModel t, string userName) => !t.TransactionId.HasValue || t.Transaction.Account.Customer.UserCustomers.Any(uc => uc.UserName == userName), () => Thread.CurrentPrincipal.Identity.Name);

            modelBuilder.Entity<ImportHistoryModel>().ToTable("ImportHistory");
            modelBuilder.Entity<ImportHistoryModel>().Property(t => t.FileName).IsRequired();
        }
    }
}