namespace NBlackout.MoneyManager.Entity.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class AccountImportHistory : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Account", "ImportHistoryId", c => c.Long());
            CreateIndex("dbo.Account", "ImportHistoryId");
            AddForeignKey("dbo.Account", "ImportHistoryId", "dbo.ImportHistory", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Account", "ImportHistoryId", "dbo.ImportHistory");
            DropIndex("dbo.Account", new[] { "ImportHistoryId" });
            DropColumn("dbo.Account", "ImportHistoryId");
        }
    }
}
