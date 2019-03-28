namespace NBlackout.MoneyManager.Entity.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class TransactionOriginAmountAndTolerance : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TransactionOrigin", "Amount", c => c.Decimal(precision: 18, scale: 2));
            AddColumn("dbo.TransactionOrigin", "Tolerance", c => c.Decimal(precision: 18, scale: 2));
        }

        public override void Down()
        {
            DropColumn("dbo.TransactionOrigin", "Tolerance");
            DropColumn("dbo.TransactionOrigin", "Amount");
        }
    }
}
