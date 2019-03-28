namespace NBlackout.MoneyManager.Entity.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class TransactionForecast : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TransactionForecast",
                c => new
                {
                    Id = c.Long(nullable: false, identity: true),
                    AccountId = c.Long(nullable: false),
                    OriginId = c.Long(nullable: false),
                    Date = c.DateTimeOffset(precision: 7),
                    Label = c.String(),
                    Amount = c.Decimal(nullable: false, precision: 18, scale: 2),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Account", t => t.AccountId, cascadeDelete: true)
                .ForeignKey("dbo.TransactionOrigin", t => t.OriginId, cascadeDelete: true)
                .Index(t => t.AccountId)
                .Index(t => t.OriginId);
        }

        public override void Down()
        {
            DropForeignKey("dbo.TransactionForecast", "OriginId", "dbo.TransactionOrigin");
            DropForeignKey("dbo.TransactionForecast", "AccountId", "dbo.Account");
            DropIndex("dbo.TransactionForecast", new[] { "OriginId" });
            DropIndex("dbo.TransactionForecast", new[] { "AccountId" });
            DropTable("dbo.TransactionForecast");
        }
    }
}
