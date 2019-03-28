namespace NBlackout.MoneyManager.Entity.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class AlertsRemoved : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.AccountAlert", "AccountId", "dbo.Account");
            DropForeignKey("dbo.TransactionAlert", "OriginId", "dbo.TransactionOrigin");
            DropIndex("dbo.AccountAlert", new[] { "AccountId" });
            DropIndex("dbo.TransactionAlert", new[] { "OriginId" });
            DropTable("dbo.AccountAlert");
            DropTable("dbo.TransactionAlert");
        }

        public override void Down()
        {
            CreateTable(
                "dbo.TransactionAlert",
                c => new
                {
                    Id = c.Long(nullable: false, identity: true),
                    OriginId = c.Long(nullable: false),
                    MinAmount = c.Decimal(nullable: false, precision: 18, scale: 2),
                    MaxAmount = c.Decimal(nullable: false, precision: 18, scale: 2),
                    SendMail = c.Boolean(nullable: false),
                    SendSms = c.Boolean(nullable: false),
                })
                .PrimaryKey(t => t.Id);

            CreateTable(
                "dbo.AccountAlert",
                c => new
                {
                    Id = c.Long(nullable: false, identity: true),
                    AccountId = c.Long(nullable: false),
                    MinBalance = c.Decimal(nullable: false, precision: 18, scale: 2),
                    SendMail = c.Boolean(nullable: false),
                    SendSms = c.Boolean(nullable: false),
                })
                .PrimaryKey(t => t.Id);

            CreateIndex("dbo.TransactionAlert", "OriginId");
            CreateIndex("dbo.AccountAlert", "AccountId");
            AddForeignKey("dbo.TransactionAlert", "OriginId", "dbo.TransactionOrigin", "Id", cascadeDelete: true);
            AddForeignKey("dbo.AccountAlert", "AccountId", "dbo.Account", "Id", cascadeDelete: true);
        }
    }
}
