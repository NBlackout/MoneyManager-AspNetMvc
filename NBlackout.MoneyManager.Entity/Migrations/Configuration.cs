using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using NBlackout.MoneyManager.Models;

namespace NBlackout.MoneyManager.Entity.Migrations
{
    public class Configuration : DbMigrationsConfiguration<MoneyManagerDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(MoneyManagerDbContext context)
        {
            if (context == null)
                throw new ArgumentNullException(nameof(context));

            var accountCategories = new List<AccountCategoryModel>
            {
                new AccountCategoryModel { Type = AccountTypeModel.Checking, Label = "CCHQ" },
                new AccountCategoryModel { Type = AccountTypeModel.Saving, Label = "CEL" },
                new AccountCategoryModel { Type = AccountTypeModel.Saving, Label = "PEL" },
                new AccountCategoryModel { Type = AccountTypeModel.CertificateOfDeposit, Label = "LIV A" },
                new AccountCategoryModel { Type = AccountTypeModel.CertificateOfDeposit, Label = "LJMO" }
            };

            foreach (var accountCategory in accountCategories)
            {
                var exists = context.AccountCategories.Any(t => t.Label == accountCategory.Label);
                if (!exists)
                    context.AccountCategories.Add(accountCategory);

                context.SaveChanges();
            }

            var organizationCategories = new List<OrganizationCategoryModel>
            {
                new OrganizationCategoryModel { Label = "Aménagement" },
                new OrganizationCategoryModel { Label = "Assurance" },
                new OrganizationCategoryModel { Label = "Banque" },
                new OrganizationCategoryModel { Label = "Eau" },
                new OrganizationCategoryModel { Label = "Électricité" },
                new OrganizationCategoryModel { Label = "Emprûnt" },
                new OrganizationCategoryModel { Label = "Exceptionnel" },
                new OrganizationCategoryModel { Label = "Gaz" },
                new OrganizationCategoryModel { Label = "Impôts" },
                new OrganizationCategoryModel { Label = "Internet" },
                new OrganizationCategoryModel { Label = "Loyer" },
                new OrganizationCategoryModel { Label = "Nourriture" },
                new OrganizationCategoryModel { Label = "Salaire" },
                new OrganizationCategoryModel { Label = "Téléphone" },
                new OrganizationCategoryModel { Label = "Voiture" }
            };

            foreach (var organizationCategory in organizationCategories)
            {
                var exists = context.OrganizationCategories.Any(t => t.Label == organizationCategory.Label);
                if (!exists)
                    context.OrganizationCategories.Add(organizationCategory);

                context.SaveChanges();
            }
        }
    }
}
