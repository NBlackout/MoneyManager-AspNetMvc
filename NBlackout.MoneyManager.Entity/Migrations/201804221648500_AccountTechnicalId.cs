namespace NBlackout.MoneyManager.Entity.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class AccountTechnicalId : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Account", "TechnicalId", c => c.String());
            Sql("UPDATE dbo.Account SET TechnicalId = Number");
        }

        public override void Down()
        {
            DropColumn("dbo.Account", "TechnicalId");
        }
    }
}
