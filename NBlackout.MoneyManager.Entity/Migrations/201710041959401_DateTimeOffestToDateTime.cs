namespace NBlackout.MoneyManager.Entity.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class DateTimeOffestToDateTime : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Account", "LastSync", c => c.DateTime());
            AlterColumn("dbo.Transaction", "Date", c => c.DateTime(nullable: false));
            AlterColumn("dbo.ImportHistory", "BeginDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.ImportHistory", "EndDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.TransactionForecastHit", "Date", c => c.DateTime(nullable: false));
        }

        public override void Down()
        {
            AlterColumn("dbo.TransactionForecastHit", "Date", c => c.DateTimeOffset(nullable: false, precision: 7));
            AlterColumn("dbo.ImportHistory", "EndDate", c => c.DateTimeOffset(nullable: false, precision: 7));
            AlterColumn("dbo.ImportHistory", "BeginDate", c => c.DateTimeOffset(nullable: false, precision: 7));
            AlterColumn("dbo.Transaction", "Date", c => c.DateTimeOffset(nullable: false, precision: 7));
            AlterColumn("dbo.Account", "LastSync", c => c.DateTimeOffset(precision: 7));
        }
    }
}
