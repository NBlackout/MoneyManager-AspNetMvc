namespace NBlackout.MoneyManager.Entity.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class TransactionForecastHit : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TransactionForecastHit",
                c => new
                {
                    Id = c.Long(nullable: false, identity: true),
                    ForecastId = c.Long(nullable: false),
                    Date = c.DateTimeOffset(nullable: false, precision: 7),
                    TransactionId = c.Long(),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.TransactionForecast", t => t.ForecastId, cascadeDelete: true)
                .ForeignKey("dbo.Transaction", t => t.TransactionId)
                .Index(t => t.ForecastId)
                .Index(t => t.TransactionId);

            DropColumn("dbo.TransactionForecast", "Date");
        }

        public override void Down()
        {
            AddColumn("dbo.TransactionForecast", "Date", c => c.DateTimeOffset(precision: 7));
            DropForeignKey("dbo.TransactionForecastHit", "TransactionId", "dbo.Transaction");
            DropForeignKey("dbo.TransactionForecastHit", "ForecastId", "dbo.TransactionForecast");
            DropIndex("dbo.TransactionForecastHit", new[] { "TransactionId" });
            DropIndex("dbo.TransactionForecastHit", new[] { "ForecastId" });
            DropTable("dbo.TransactionForecastHit");
        }
    }
}
