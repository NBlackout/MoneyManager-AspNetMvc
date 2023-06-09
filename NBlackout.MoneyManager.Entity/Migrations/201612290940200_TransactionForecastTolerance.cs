namespace NBlackout.MoneyManager.Entity.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class TransactionForecastTolerance : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TransactionForecast", "Tolerance", c => c.Decimal(precision: 18, scale: 2));
        }

        public override void Down()
        {
            DropColumn("dbo.TransactionForecast", "Tolerance");
        }
    }
}
