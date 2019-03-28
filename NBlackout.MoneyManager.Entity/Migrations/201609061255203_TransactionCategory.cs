namespace NBlackout.MoneyManager.Entity.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class TransactionCategory : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TransactionType", "Category", c => c.Int());
        }

        public override void Down()
        {
            DropColumn("dbo.TransactionType", "Category");
        }
    }
}
