namespace NBlackout.MoneyManager.Entity.Migrations
{
    using System.Collections.Generic;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Data.Entity.Migrations;

    public partial class AccountFilter : DbMigration
    {
        public override void Up()
        {
            AlterTableAnnotations(
                "dbo.Account",
                c => new
                {
                    Id = c.Long(nullable: false, identity: true),
                    CustomerId = c.Long(),
                    TypeId = c.Long(),
                    Title = c.String(),
                    Number = c.String(nullable: false, maxLength: 450),
                    Balance = c.Decimal(nullable: false, precision: 18, scale: 2),
                    TechnicalId = c.String(),
                    LastSync = c.DateTime(),
                    Enabled = c.Boolean(nullable: false),
                },
                annotations: new Dictionary<string, AnnotationValues>
                {
                    {
                        "DynamicFilter_AccountModel_AccountEnabled",
                        new AnnotationValues(oldValue: null, newValue: "EntityFramework.DynamicFilters.DynamicFilterDefinition")
                    },
                    {
                        "DynamicFilter_AccountModel_AccountVisible",
                        new AnnotationValues(oldValue: null, newValue: "EntityFramework.DynamicFilters.DynamicFilterDefinition")
                    },
                });

            AlterTableAnnotations(
                "dbo.Customer",
                c => new
                {
                    Id = c.Long(nullable: false, identity: true),
                    FirstName = c.String(nullable: false),
                    LastName = c.String(nullable: false),
                },
                annotations: new Dictionary<string, AnnotationValues>
                {
                    {
                        "DynamicFilter_CustomerModel_CustomerVisible",
                        new AnnotationValues(oldValue: null, newValue: "EntityFramework.DynamicFilters.DynamicFilterDefinition")
                    },
                });

            AlterTableAnnotations(
                "dbo.Transaction",
                c => new
                {
                    Id = c.Long(nullable: false, identity: true),
                    AccountId = c.Long(nullable: false),
                    OriginId = c.Long(),
                    Number = c.String(nullable: false, maxLength: 450),
                    Date = c.DateTime(nullable: false),
                    Label = c.String(),
                    Amount = c.Decimal(nullable: false, precision: 18, scale: 2),
                    Description = c.String(),
                    ImportHistoryId = c.Long(),
                },
                annotations: new Dictionary<string, AnnotationValues>
                {
                    {
                        "DynamicFilter_TransactionModel_TransactionVisible",
                        new AnnotationValues(oldValue: null, newValue: "EntityFramework.DynamicFilters.DynamicFilterDefinition")
                    },
                });

            AlterTableAnnotations(
                "dbo.TransactionOriginAccount",
                c => new
                {
                    Id = c.Long(nullable: false, identity: true),
                    OriginId = c.Long(nullable: false),
                    AccountId = c.Long(nullable: false),
                    Pattern = c.String(),
                    Amount = c.Decimal(precision: 18, scale: 2),
                    Tolerance = c.Decimal(precision: 18, scale: 2),
                },
                annotations: new Dictionary<string, AnnotationValues>
                {
                    {
                        "DynamicFilter_TransactionOriginAccountModel_TransactionOriginAccountVisible",
                        new AnnotationValues(oldValue: null, newValue: "EntityFramework.DynamicFilters.DynamicFilterDefinition")
                    },
                });

            AlterTableAnnotations(
                "dbo.TransactionForecastHit",
                c => new
                {
                    Id = c.Long(nullable: false, identity: true),
                    ForecastId = c.Long(nullable: false),
                    Date = c.DateTime(nullable: false),
                    TransactionId = c.Long(),
                },
                annotations: new Dictionary<string, AnnotationValues>
                {
                    {
                        "DynamicFilter_TransactionForecastHitModel_TransactionForecastHitVisible",
                        new AnnotationValues(oldValue: null, newValue: "EntityFramework.DynamicFilters.DynamicFilterDefinition")
                    },
                });

            AlterTableAnnotations(
                "dbo.TransactionForecast",
                c => new
                {
                    Id = c.Long(nullable: false, identity: true),
                    AccountId = c.Long(nullable: false),
                    OriginId = c.Long(nullable: false),
                    AutoReschedule = c.Boolean(nullable: false),
                },
                annotations: new Dictionary<string, AnnotationValues>
                {
                    {
                        "DynamicFilter_TransactionForecastModel_TransactionForecastVisible",
                        new AnnotationValues(oldValue: null, newValue: "EntityFramework.DynamicFilters.DynamicFilterDefinition")
                    },
                });

        }

        public override void Down()
        {
            AlterTableAnnotations(
                "dbo.TransactionForecast",
                c => new
                {
                    Id = c.Long(nullable: false, identity: true),
                    AccountId = c.Long(nullable: false),
                    OriginId = c.Long(nullable: false),
                    AutoReschedule = c.Boolean(nullable: false),
                },
                annotations: new Dictionary<string, AnnotationValues>
                {
                    {
                        "DynamicFilter_TransactionForecastModel_TransactionForecastVisible",
                        new AnnotationValues(oldValue: "EntityFramework.DynamicFilters.DynamicFilterDefinition", newValue: null)
                    },
                });

            AlterTableAnnotations(
                "dbo.TransactionForecastHit",
                c => new
                {
                    Id = c.Long(nullable: false, identity: true),
                    ForecastId = c.Long(nullable: false),
                    Date = c.DateTime(nullable: false),
                    TransactionId = c.Long(),
                },
                annotations: new Dictionary<string, AnnotationValues>
                {
                    {
                        "DynamicFilter_TransactionForecastHitModel_TransactionForecastHitVisible",
                        new AnnotationValues(oldValue: "EntityFramework.DynamicFilters.DynamicFilterDefinition", newValue: null)
                    },
                });

            AlterTableAnnotations(
                "dbo.TransactionOriginAccount",
                c => new
                {
                    Id = c.Long(nullable: false, identity: true),
                    OriginId = c.Long(nullable: false),
                    AccountId = c.Long(nullable: false),
                    Pattern = c.String(),
                    Amount = c.Decimal(precision: 18, scale: 2),
                    Tolerance = c.Decimal(precision: 18, scale: 2),
                },
                annotations: new Dictionary<string, AnnotationValues>
                {
                    {
                        "DynamicFilter_TransactionOriginAccountModel_TransactionOriginAccountVisible",
                        new AnnotationValues(oldValue: "EntityFramework.DynamicFilters.DynamicFilterDefinition", newValue: null)
                    },
                });

            AlterTableAnnotations(
                "dbo.Transaction",
                c => new
                {
                    Id = c.Long(nullable: false, identity: true),
                    AccountId = c.Long(nullable: false),
                    OriginId = c.Long(),
                    Number = c.String(nullable: false, maxLength: 450),
                    Date = c.DateTime(nullable: false),
                    Label = c.String(),
                    Amount = c.Decimal(nullable: false, precision: 18, scale: 2),
                    Description = c.String(),
                    ImportHistoryId = c.Long(),
                },
                annotations: new Dictionary<string, AnnotationValues>
                {
                    {
                        "DynamicFilter_TransactionModel_TransactionVisible",
                        new AnnotationValues(oldValue: "EntityFramework.DynamicFilters.DynamicFilterDefinition", newValue: null)
                    },
                });

            AlterTableAnnotations(
                "dbo.Customer",
                c => new
                {
                    Id = c.Long(nullable: false, identity: true),
                    FirstName = c.String(nullable: false),
                    LastName = c.String(nullable: false),
                },
                annotations: new Dictionary<string, AnnotationValues>
                {
                    {
                        "DynamicFilter_CustomerModel_CustomerVisible",
                        new AnnotationValues(oldValue: "EntityFramework.DynamicFilters.DynamicFilterDefinition", newValue: null)
                    },
                });

            AlterTableAnnotations(
                "dbo.Account",
                c => new
                {
                    Id = c.Long(nullable: false, identity: true),
                    CustomerId = c.Long(),
                    TypeId = c.Long(),
                    Title = c.String(),
                    Number = c.String(nullable: false, maxLength: 450),
                    Balance = c.Decimal(nullable: false, precision: 18, scale: 2),
                    TechnicalId = c.String(),
                    LastSync = c.DateTime(),
                    Enabled = c.Boolean(nullable: false),
                },
                annotations: new Dictionary<string, AnnotationValues>
                {
                    {
                        "DynamicFilter_AccountModel_AccountEnabled",
                        new AnnotationValues(oldValue: "EntityFramework.DynamicFilters.DynamicFilterDefinition", newValue: null)
                    },
                    {
                        "DynamicFilter_AccountModel_AccountVisible",
                        new AnnotationValues(oldValue: "EntityFramework.DynamicFilters.DynamicFilterDefinition", newValue: null)
                    },
                });

        }
    }
}
