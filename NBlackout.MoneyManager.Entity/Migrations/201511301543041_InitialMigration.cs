namespace NBlackout.MoneyManager.Entity.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class InitialMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Account",
                c => new
                {
                    Id = c.Long(nullable: false, identity: true),
                    CustomerId = c.Long(),
                    TypeId = c.Long(),
                    Title = c.String(),
                    Number = c.String(),
                    Balance = c.Decimal(nullable: false, precision: 18, scale: 2),
                    LastSync = c.DateTimeOffset(precision: 7),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Customer", t => t.CustomerId)
                .ForeignKey("dbo.AccountType", t => t.TypeId)
                .Index(t => t.CustomerId)
                .Index(t => t.TypeId);

            CreateTable(
                "dbo.Customer",
                c => new
                {
                    Id = c.Long(nullable: false, identity: true),
                    FirstName = c.String(),
                    LastName = c.String(),
                })
                .PrimaryKey(t => t.Id);

            CreateTable(
                "dbo.Transaction",
                c => new
                {
                    Id = c.Long(nullable: false, identity: true),
                    AccountId = c.Long(nullable: false),
                    SourceId = c.Long(),
                    Date = c.DateTimeOffset(nullable: false, precision: 7),
                    Label = c.String(),
                    Amount = c.Decimal(nullable: false, precision: 18, scale: 2),
                    Number = c.String(),
                    Description = c.String(),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Account", t => t.AccountId, cascadeDelete: true)
                .ForeignKey("dbo.TransactionSource", t => t.SourceId)
                .Index(t => t.AccountId)
                .Index(t => t.SourceId);

            CreateTable(
                "dbo.TransactionSource",
                c => new
                {
                    Id = c.Long(nullable: false, identity: true),
                    TypeId = c.Long(nullable: false),
                    Label = c.String(),
                    Pattern = c.String(),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.TransactionSourceType", t => t.TypeId, cascadeDelete: true)
                .Index(t => t.TypeId);

            CreateTable(
                "dbo.TransactionSourceType",
                c => new
                {
                    Id = c.Long(nullable: false, identity: true),
                    Label = c.String(),
                })
                .PrimaryKey(t => t.Id);

            CreateTable(
                "dbo.AccountType",
                c => new
                {
                    Id = c.Long(nullable: false, identity: true),
                    Category = c.Int(nullable: false),
                    Label = c.String(),
                })
                .PrimaryKey(t => t.Id);

            CreateTable(
                "dbo.AspNetRoles",
                c => new
                {
                    Id = c.String(nullable: false, maxLength: 128),
                    Name = c.String(nullable: false, maxLength: 256),
                })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");

            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                {
                    UserId = c.String(nullable: false, maxLength: 128),
                    RoleId = c.String(nullable: false, maxLength: 128),
                })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.User", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);

            CreateTable(
                "dbo.User",
                c => new
                {
                    Id = c.String(nullable: false, maxLength: 128),
                    Email = c.String(maxLength: 256),
                    EmailConfirmed = c.Boolean(nullable: false),
                    PasswordHash = c.String(),
                    SecurityStamp = c.String(),
                    PhoneNumber = c.String(),
                    PhoneNumberConfirmed = c.Boolean(nullable: false),
                    TwoFactorEnabled = c.Boolean(nullable: false),
                    LockoutEndDateUtc = c.DateTime(),
                    LockoutEnabled = c.Boolean(nullable: false),
                    AccessFailedCount = c.Int(nullable: false),
                    UserName = c.String(nullable: false, maxLength: 256),
                })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");

            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    UserId = c.String(nullable: false, maxLength: 128),
                    ClaimType = c.String(),
                    ClaimValue = c.String(),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.User", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);

            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                {
                    LoginProvider = c.String(nullable: false, maxLength: 128),
                    ProviderKey = c.String(nullable: false, maxLength: 128),
                    UserId = c.String(nullable: false, maxLength: 128),
                })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.User", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
        }

        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.User");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.User");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.User");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.Account", "TypeId", "dbo.AccountType");
            DropForeignKey("dbo.Transaction", "SourceId", "dbo.TransactionSource");
            DropForeignKey("dbo.TransactionSource", "TypeId", "dbo.TransactionSourceType");
            DropForeignKey("dbo.Transaction", "AccountId", "dbo.Account");
            DropForeignKey("dbo.Account", "CustomerId", "dbo.Customer");
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.User", "UserNameIndex");
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.TransactionSource", new[] { "TypeId" });
            DropIndex("dbo.Transaction", new[] { "SourceId" });
            DropIndex("dbo.Transaction", new[] { "AccountId" });
            DropIndex("dbo.Account", new[] { "TypeId" });
            DropIndex("dbo.Account", new[] { "CustomerId" });
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.User");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.AccountType");
            DropTable("dbo.TransactionSourceType");
            DropTable("dbo.TransactionSource");
            DropTable("dbo.Transaction");
            DropTable("dbo.Customer");
            DropTable("dbo.Account");
        }
    }
}
