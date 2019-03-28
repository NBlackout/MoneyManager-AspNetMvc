namespace NBlackout.MoneyManager.Entity.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class MandatoryTransactionCategory : DbMigration
    {
        public override void Up()
        {
            Sql("UPDATE dbo.TransactionType SET Category = 2 WHERE Category = 1");
            Sql("UPDATE dbo.TransactionType SET Category = 1 WHERE Category = 0");
            Sql("UPDATE dbo.TransactionType SET Category = 0 WHERE Category IS NULL");
            AlterColumn("dbo.TransactionType", "Category", c => c.Int(nullable: false));
        }

        public override void Down()
        {
            AlterColumn("dbo.TransactionType", "Category", c => c.Int());
            Sql("UPDATE dbo.TransactionType SET Category = NULL WHERE Category = 0");
            Sql("UPDATE dbo.TransactionType SET Category = 0 WHERE Category = 1");
            Sql("UPDATE dbo.TransactionType SET Category = 1 WHERE Category = 2");
        }
    }
}
