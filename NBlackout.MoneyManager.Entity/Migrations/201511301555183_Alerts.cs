namespace NBlackout.MoneyManager.Entity.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class Alerts : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AccountAlert",
                c => new
                {
                    Id = c.Long(nullable: false, identity: true),
                    AccountId = c.Long(),
                    MinBalance = c.Decimal(nullable: false, precision: 18, scale: 2),
                    SendMail = c.Boolean(nullable: false),
                    SendSms = c.Boolean(nullable: false),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Account", t => t.AccountId)
                .Index(t => t.AccountId);

            CreateTable(
                "dbo.TransactionAlert",
                c => new
                {
                    Id = c.Long(nullable: false, identity: true),
                    TransactionSourceId = c.Long(),
                    MinAmount = c.Decimal(nullable: false, precision: 18, scale: 2),
                    MaxAmount = c.Decimal(nullable: false, precision: 18, scale: 2),
                    SendMail = c.Boolean(nullable: false),
                    SendSms = c.Boolean(nullable: false),
                    Source_Id = c.Long(),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.TransactionSource", t => t.Source_Id)
                .Index(t => t.Source_Id);
        }

        public override void Down()
        {
            DropForeignKey("dbo.TransactionAlert", "Source_Id", "dbo.TransactionSource");
            DropForeignKey("dbo.AccountAlert", "AccountId", "dbo.Account");
            DropIndex("dbo.TransactionAlert", new[] { "Source_Id" });
            DropIndex("dbo.AccountAlert", new[] { "AccountId" });
            DropTable("dbo.TransactionAlert");
            DropTable("dbo.AccountAlert");
        }
    }
}
