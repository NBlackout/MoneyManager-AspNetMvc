namespace NBlackout.MoneyManager.Entity.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class ImportHistoryAndDatabaseConstraints : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.TransactionForecast", new[] { "AccountId" });
            DropIndex("dbo.TransactionForecast", new[] { "OriginId" });
            CreateTable(
                "dbo.ImportHistory",
                c => new
                {
                    Id = c.Long(nullable: false, identity: true),
                    FileName = c.String(nullable: false),
                    BeginDate = c.DateTimeOffset(nullable: false, precision: 7),
                    EndDate = c.DateTimeOffset(nullable: false, precision: 7),
                    UserId = c.Long(nullable: false),
                })
                .PrimaryKey(t => t.Id);

            AddColumn("dbo.Transaction", "ImportHistoryId", c => c.Long());
            AlterColumn("dbo.Account", "Number", c => c.String(nullable: false, maxLength: 450));
            AlterColumn("dbo.Customer", "FirstName", c => c.String(nullable: false));
            AlterColumn("dbo.Customer", "LastName", c => c.String(nullable: false));
            AlterColumn("dbo.Transaction", "Number", c => c.String(nullable: false, maxLength: 450));
            AlterColumn("dbo.TransactionOrigin", "Label", c => c.String(nullable: false));
            AlterColumn("dbo.TransactionType", "Label", c => c.String(nullable: false));
            AlterColumn("dbo.AccountType", "Label", c => c.String(nullable: false, maxLength: 450));
            CreateIndex("dbo.Account", "Number", unique: true);
            CreateIndex("dbo.Transaction", "Number", unique: true);
            CreateIndex("dbo.Transaction", "ImportHistoryId");
            CreateIndex("dbo.AccountType", "Label", unique: true);
            CreateIndex("dbo.TransactionForecast", new[] { "AccountId", "OriginId" }, unique: true);
            AddForeignKey("dbo.Transaction", "ImportHistoryId", "dbo.ImportHistory", "Id");
        }

        public override void Down()
        {
            DropForeignKey("dbo.Transaction", "ImportHistoryId", "dbo.ImportHistory");
            DropIndex("dbo.TransactionForecast", new[] { "AccountId", "OriginId" });
            DropIndex("dbo.AccountType", new[] { "Label" });
            DropIndex("dbo.Transaction", new[] { "ImportHistoryId" });
            DropIndex("dbo.Transaction", new[] { "Number" });
            DropIndex("dbo.Account", new[] { "Number" });
            AlterColumn("dbo.AccountType", "Label", c => c.String());
            AlterColumn("dbo.TransactionType", "Label", c => c.String());
            AlterColumn("dbo.TransactionOrigin", "Label", c => c.String());
            AlterColumn("dbo.Transaction", "Number", c => c.String());
            AlterColumn("dbo.Customer", "LastName", c => c.String());
            AlterColumn("dbo.Customer", "FirstName", c => c.String());
            AlterColumn("dbo.Account", "Number", c => c.String());
            DropColumn("dbo.Transaction", "ImportHistoryId");
            DropTable("dbo.ImportHistory");
            CreateIndex("dbo.TransactionForecast", "OriginId");
            CreateIndex("dbo.TransactionForecast", "AccountId");
        }
    }
}
