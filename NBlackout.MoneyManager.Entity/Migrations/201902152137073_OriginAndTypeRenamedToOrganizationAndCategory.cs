namespace NBlackout.MoneyManager.Entity.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class OriginAndTypeRenamedToOrganizationAndCategory : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.TransactionOrigin", newName: "Organization");
            RenameTable(name: "dbo.TransactionType", newName: "OrganizationCategory");
            RenameTable(name: "dbo.TransactionOriginAccount", newName: "OrganizationAccount");
            RenameTable(name: "dbo.AccountType", newName: "AccountCategory");
            RenameColumn(table: "dbo.OrganizationAccount", name: "OriginId", newName: "OrganizationId");
            RenameColumn(table: "dbo.Transaction", name: "OriginId", newName: "OrganizationId");
            RenameColumn(table: "dbo.TransactionForecast", name: "OriginId", newName: "OrganizationId");
            RenameIndex(table: "dbo.Transaction", name: "IX_OriginId", newName: "IX_OrganizationId");
        }

        public override void Down()
        {
            RenameIndex(table: "dbo.Transaction", name: "IX_OrganizationId", newName: "IX_OriginId");
            RenameColumn(table: "dbo.TransactionForecast", name: "OrganizationId", newName: "OriginId");
            RenameColumn(table: "dbo.Transaction", name: "OrganizationId", newName: "OriginId");
            RenameColumn(table: "dbo.OrganizationAccount", name: "OrganizationId", newName: "OriginId");
            RenameTable(name: "dbo.AccountCategory", newName: "AccountType");
            RenameTable(name: "dbo.OrganizationAccount", newName: "TransactionOriginAccount");
            RenameTable(name: "dbo.OrganizationCategory", newName: "OrganizationType");
            RenameTable(name: "dbo.Organization", newName: "TransactionOrigin");
        }
    }
}
