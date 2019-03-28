namespace NBlackout.MoneyManager.Entity.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class TransactionSourceAndType : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.TransactionSource", newName: "TransactionOrigin");
            RenameColumn(table: "dbo.Transaction", name: "SourceId", newName: "OriginId");
            RenameColumn(table: "dbo.TransactionAlert", name: "SourceId", newName: "OriginId");
            RenameIndex(table: "dbo.Transaction", name: "IX_SourceId", newName: "IX_OriginId");
            RenameIndex(table: "dbo.TransactionAlert", name: "IX_SourceId", newName: "IX_OriginId");
            RenameTable(name: "dbo.TransactionSourceType", newName: "TransactionType");
        }

        public override void Down()
        {
            RenameTable(name: "dbo.TransactionType", newName: "TransactionSourceType");
            RenameIndex(table: "dbo.TransactionAlert", name: "IX_OriginId", newName: "IX_SourceId");
            RenameIndex(table: "dbo.Transaction", name: "IX_OriginId", newName: "IX_SourceId");
            RenameColumn(table: "dbo.TransactionAlert", name: "OriginId", newName: "SourceId");
            RenameColumn(table: "dbo.Transaction", name: "OriginId", newName: "SourceId");
            RenameTable(name: "dbo.TransactionOrigin", newName: "TransactionSource");
        }
    }
}
