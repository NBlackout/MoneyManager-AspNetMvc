namespace NBlackout.MoneyManager.Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class OrganizationCategoryHierarchyAndRecurrentFlag : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.OrganizationCategory", "CategoryId", c => c.Long());
            AddColumn("dbo.OrganizationCategory", "Recurrent", c => c.Boolean(nullable: false));
            CreateIndex("dbo.OrganizationCategory", "CategoryId");
            AddForeignKey("dbo.OrganizationCategory", "CategoryId", "dbo.OrganizationCategory", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.OrganizationCategory", "CategoryId", "dbo.OrganizationCategory");
            DropIndex("dbo.OrganizationCategory", new[] { "CategoryId" });
            DropColumn("dbo.OrganizationCategory", "Recurrent");
            DropColumn("dbo.OrganizationCategory", "CategoryId");
        }
    }
}
