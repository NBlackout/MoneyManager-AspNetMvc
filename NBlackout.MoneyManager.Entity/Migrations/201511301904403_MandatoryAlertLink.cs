namespace NBlackout.MoneyManager.Entity.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class MandatoryAlertLink : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.AccountAlert", "AccountId", "dbo.Account");
            DropForeignKey("dbo.TransactionAlert", "Source_Id", "dbo.TransactionSource");
            DropIndex("dbo.AccountAlert", new[] { "AccountId" });
            DropIndex("dbo.TransactionAlert", new[] { "Source_Id" });
            RenameColumn(table: "dbo.TransactionAlert", name: "Source_Id", newName: "SourceId");
            AlterColumn("dbo.AccountAlert", "AccountId", c => c.Long(nullable: false));
            AlterColumn("dbo.TransactionAlert", "SourceId", c => c.Long(nullable: false));
            CreateIndex("dbo.AccountAlert", "AccountId");
            CreateIndex("dbo.TransactionAlert", "SourceId");
            AddForeignKey("dbo.AccountAlert", "AccountId", "dbo.Account", "Id", cascadeDelete: true);
            AddForeignKey("dbo.TransactionAlert", "SourceId", "dbo.TransactionSource", "Id", cascadeDelete: true);
            DropColumn("dbo.TransactionAlert", "TransactionSourceId");
        }

        public override void Down()
        {
            AddColumn("dbo.TransactionAlert", "TransactionSourceId", c => c.Long());
            DropForeignKey("dbo.TransactionAlert", "SourceId", "dbo.TransactionSource");
            DropForeignKey("dbo.AccountAlert", "AccountId", "dbo.Account");
            DropIndex("dbo.TransactionAlert", new[] { "SourceId" });
            DropIndex("dbo.AccountAlert", new[] { "AccountId" });
            AlterColumn("dbo.TransactionAlert", "SourceId", c => c.Long());
            AlterColumn("dbo.AccountAlert", "AccountId", c => c.Long());
            RenameColumn(table: "dbo.TransactionAlert", name: "SourceId", newName: "Source_Id");
            CreateIndex("dbo.TransactionAlert", "Source_Id");
            CreateIndex("dbo.AccountAlert", "AccountId");
            AddForeignKey("dbo.TransactionAlert", "Source_Id", "dbo.TransactionSource", "Id");
            AddForeignKey("dbo.AccountAlert", "AccountId", "dbo.Account", "Id");
        }
    }
}
