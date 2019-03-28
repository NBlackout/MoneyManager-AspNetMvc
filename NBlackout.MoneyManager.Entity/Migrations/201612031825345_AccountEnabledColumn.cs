namespace NBlackout.MoneyManager.Entity.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class AccountEnabledColumn : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Account", "Enabled", c => c.Boolean(nullable: false, defaultValue: true));
        }

        public override void Down()
        {
            DropColumn("dbo.Account", "Enabled");
        }
    }
}
