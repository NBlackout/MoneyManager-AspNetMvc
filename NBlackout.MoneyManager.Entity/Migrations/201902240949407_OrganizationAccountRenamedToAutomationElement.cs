namespace NBlackout.MoneyManager.Entity.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Data.Entity.Migrations;
    
    public partial class OrganizationAccountRenamedToAutomationElement : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.OrganizationAccount", newName: "AutomationElement");
            CreateTable(
                "dbo.AutomationElementTag",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Label = c.String(nullable: false, maxLength: 450),
                        AutomationElementId = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AutomationElement", t => t.AutomationElementId, cascadeDelete: true)
                .Index(t => new { t.AutomationElementId, t.Label }, unique: true, name: "IX_AutomationElementId_Tag");
            
            AlterTableAnnotations(
                "dbo.AutomationElement",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        OrganizationId = c.Long(nullable: false),
                        AccountId = c.Long(nullable: false),
                        Amount = c.Decimal(precision: 18, scale: 2),
                        Tolerance = c.Decimal(precision: 18, scale: 2),
                    },
                annotations: new Dictionary<string, AnnotationValues>
                {
                    { 
                        "DynamicFilter_AutomationElementModel_AutomationElementVisible",
                        new AnnotationValues(oldValue: null, newValue: "EntityFramework.DynamicFilters.DynamicFilterDefinition")
                    },
                    { 
                        "DynamicFilter_OrganizationAccountModel_OrganizationAccountVisible",
                        new AnnotationValues(oldValue: "EntityFramework.DynamicFilters.DynamicFilterDefinition", newValue: null)
                    },
                });
            
            DropColumn("dbo.AutomationElement", "Pattern");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AutomationElement", "Pattern", c => c.String());
            DropForeignKey("dbo.AutomationElementTag", "AutomationElementId", "dbo.AutomationElement");
            DropIndex("dbo.AutomationElementTag", "IX_AutomationElementId_Tag");
            AlterTableAnnotations(
                "dbo.AutomationElement",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        OrganizationId = c.Long(nullable: false),
                        AccountId = c.Long(nullable: false),
                        Amount = c.Decimal(precision: 18, scale: 2),
                        Tolerance = c.Decimal(precision: 18, scale: 2),
                    },
                annotations: new Dictionary<string, AnnotationValues>
                {
                    { 
                        "DynamicFilter_AutomationElementModel_AutomationElementVisible",
                        new AnnotationValues(oldValue: "EntityFramework.DynamicFilters.DynamicFilterDefinition", newValue: null)
                    },
                    { 
                        "DynamicFilter_OrganizationAccountModel_OrganizationAccountVisible",
                        new AnnotationValues(oldValue: null, newValue: "EntityFramework.DynamicFilters.DynamicFilterDefinition")
                    },
                });
            
            DropTable("dbo.AutomationElementTag");
            RenameTable(name: "dbo.AutomationElement", newName: "OrganizationAccount");
        }
    }
}
