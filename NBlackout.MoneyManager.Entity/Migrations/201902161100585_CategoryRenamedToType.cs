namespace NBlackout.MoneyManager.Entity.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class CategoryRenamedToType : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Account", name: "TypeId", newName: "CategoryId");
            RenameColumn(table: "dbo.AccountCategory", name: "Category", newName: "Type");
            RenameColumn(table: "dbo.Organization", name: "TypeId", newName: "CategoryId");
            RenameColumn(table: "dbo.OrganizationCategory", name: "Category", newName: "Type");
            RenameIndex(table: "dbo.Account", name: "IX_TypeId", newName: "IX_CategoryId");
            RenameIndex(table: "dbo.Organization", name: "IX_TypeId", newName: "IX_CategoryId");
        }

        public override void Down()
        {
            RenameIndex(table: "dbo.Organization", name: "IX_CategoryId", newName: "IX_TypeId");
            RenameIndex(table: "dbo.Account", name: "IX_CategoryId", newName: "IX_TypeId");
            RenameColumn(table: "dbo.OrganizationCategory", name: "Type", newName: "Category");
            RenameColumn(table: "dbo.Organization", name: "CategoryId", newName: "TypeId");
            RenameColumn(table: "dbo.AccountCategory", name: "Type", newName: "Category");
            RenameColumn(table: "dbo.Account", name: "CategoryId", newName: "TypeId");
        }
    }
}
