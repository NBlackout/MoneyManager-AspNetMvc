namespace NBlackout.MoneyManager.Entity.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class ImportHistoryUserName : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ImportHistory", "UserName", c => c.String());
            DropColumn("dbo.ImportHistory", "UserId");
        }

        public override void Down()
        {
            AddColumn("dbo.ImportHistory", "UserId", c => c.Long(nullable: false));
            DropColumn("dbo.ImportHistory", "UserName");
        }
    }
}
