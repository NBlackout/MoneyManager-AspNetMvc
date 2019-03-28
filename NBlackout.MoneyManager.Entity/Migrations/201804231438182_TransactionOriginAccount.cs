namespace NBlackout.MoneyManager.Entity.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class TransactionOriginAccount : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TransactionOriginAccount",
                c => new
                {
                    Id = c.Long(nullable: false, identity: true),
                    OriginId = c.Long(nullable: false),
                    AccountId = c.Long(nullable: false),
                    Pattern = c.String(),
                    Amount = c.Decimal(precision: 18, scale: 2),
                    Tolerance = c.Decimal(precision: 18, scale: 2),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Account", t => t.AccountId, cascadeDelete: true)
                .ForeignKey("dbo.TransactionOrigin", t => t.OriginId, cascadeDelete: true)
                .Index(t => new { t.OriginId, t.AccountId }, unique: true);
            Sql(@"
                INSERT INTO dbo.TransactionOriginAccount (OriginId, AccountId, Pattern, Amount, Tolerance)
                SELECT O.Id, A.Id, O.Pattern, O.Amount, O.Tolerance
                FROM
                    dbo.TransactionOrigin O
                    CROSS APPLY (
                      SELECT TOP 1 A.Id
                      FROM 
                        dbo.Account A
                        INNER JOIN dbo.AccountType AT ON AT.ID = A.TypeId AND AT.Category = 0
                      WHERE A.Enabled = 1
                    ) A
            ");

            DropColumn("dbo.TransactionOrigin", "Pattern");
            DropColumn("dbo.TransactionOrigin", "Amount");
            DropColumn("dbo.TransactionOrigin", "Tolerance");
        }

        public override void Down()
        {
            AddColumn("dbo.TransactionOrigin", "Tolerance", c => c.Decimal(precision: 18, scale: 2));
            AddColumn("dbo.TransactionOrigin", "Amount", c => c.Decimal(precision: 18, scale: 2));
            AddColumn("dbo.TransactionOrigin", "Pattern", c => c.String());
            DropForeignKey("dbo.TransactionOriginAccount", "OriginId", "dbo.TransactionOrigin");
            DropForeignKey("dbo.TransactionOriginAccount", "AccountId", "dbo.Account");
            DropIndex("dbo.TransactionOriginAccount", new[] { "OriginId", "AccountId" });
            DropTable("dbo.TransactionOriginAccount");
        }
    }
}
