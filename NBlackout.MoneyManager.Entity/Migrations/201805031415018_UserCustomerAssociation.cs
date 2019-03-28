namespace NBlackout.MoneyManager.Entity.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class UserCustomerAssociation : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.UserCustomer",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        UserName = c.String(nullable: false, maxLength: 256),
                        CustomerId = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Customer", t => t.CustomerId, cascadeDelete: true)
                .Index(t => new { t.UserName, t.CustomerId }, unique: true);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserCustomer", "CustomerId", "dbo.Customer");
            DropIndex("dbo.UserCustomer", new[] { "UserName", "CustomerId" });
            DropTable("dbo.UserCustomer");
        }
    }
}
