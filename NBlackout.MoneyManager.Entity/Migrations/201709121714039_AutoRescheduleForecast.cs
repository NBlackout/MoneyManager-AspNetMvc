namespace NBlackout.MoneyManager.Entity.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class AutoRescheduleForecast : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TransactionForecast", "AutoReschedule", c => c.Boolean(nullable: false));
        }

        public override void Down()
        {
            DropColumn("dbo.TransactionForecast", "AutoReschedule");
        }
    }
}
