namespace NBlackout.MoneyManager.Entity.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class TransactionForecastSimplified : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.TransactionForecast", "Label");
            DropColumn("dbo.TransactionForecast", "Amount");
            DropColumn("dbo.TransactionForecast", "Tolerance");
        }

        public override void Down()
        {
            AddColumn("dbo.TransactionForecast", "Tolerance", c => c.Decimal(precision: 18, scale: 2));
            AddColumn("dbo.TransactionForecast", "Amount", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.TransactionForecast", "Label", c => c.String());
        }
    }
}
