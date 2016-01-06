namespace GridironGrub.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Restart : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AdminInfoes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ParkId = c.Int(nullable: false),
                        Name = c.String(),
                        IsRetired = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Parks", t => t.ParkId, cascadeDelete: true)
                .Index(t => t.ParkId);
            
            CreateTable(
                "dbo.Parks",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        LogoUrl = c.String(),
                        PrimaryColor = c.String(),
                        SecondaryColor = c.String(),
                        TaxRate = c.Decimal(nullable: false, precision: 18, scale: 6),
                        Name = c.String(),
                        IsRetired = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Areas",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ParkId = c.Int(nullable: false),
                        Name = c.String(),
                        IsRetired = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Parks", t => t.ParkId, cascadeDelete: true)
                .Index(t => t.ParkId);
            
            CreateTable(
                "dbo.Restaurants",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ImageUrl = c.String(),
                        Description = c.String(),
                        AreaId = c.Int(nullable: false),
                        Name = c.String(),
                        IsRetired = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Areas", t => t.AreaId, cascadeDelete: true)
                .Index(t => t.AreaId);
            
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        RestaurantId = c.Int(nullable: false),
                        Name = c.String(),
                        IsRetired = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Restaurants", t => t.RestaurantId, cascadeDelete: true)
                .Index(t => t.RestaurantId);
            
            CreateTable(
                "dbo.Items",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ImageUrl = c.String(),
                        Description = c.String(),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Discount = c.Decimal(nullable: false, precision: 18, scale: 6),
                        IsAlcohol = c.Boolean(nullable: false),
                        CategoryId = c.Int(nullable: false),
                        Name = c.String(),
                        IsRetired = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Categories", t => t.CategoryId, cascadeDelete: true)
                .Index(t => t.CategoryId);
            
            CreateTable(
                "dbo.OrderItems",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Quantity = c.Int(nullable: false),
                        ItemId = c.Int(nullable: false),
                        OrderId = c.Int(nullable: false),
                        Name = c.String(),
                        IsRetired = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Items", t => t.ItemId, cascadeDelete: true)
                .ForeignKey("dbo.Orders", t => t.OrderId, cascadeDelete: true)
                .Index(t => t.ItemId)
                .Index(t => t.OrderId);
            
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Subtotal = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Taxes = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ConvenienceFee = c.Decimal(nullable: false, precision: 18, scale: 2),
                        OutOfAreaFee = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Total = c.Decimal(nullable: false, precision: 18, scale: 2),
                        PersDesc = c.String(),
                        TimeOrdered = c.DateTime(),
                        TimePrepared = c.DateTime(),
                        TimeDelivered = c.DateTime(),
                        ReceiptEmail = c.String(),
                        CustomerId = c.Int(),
                        RunnerId = c.Int(),
                        RestaurantId = c.Int(nullable: false),
                        SeatInfoId = c.Int(nullable: false),
                        Name = c.String(),
                        IsRetired = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.CustomerInfoes", t => t.CustomerId)
                .ForeignKey("dbo.Restaurants", t => t.RestaurantId, cascadeDelete: false)
                .ForeignKey("dbo.RunnerInfoes", t => t.RunnerId)
                .ForeignKey("dbo.SeatInfoes", t => t.SeatInfoId, cascadeDelete: true)
                .Index(t => t.CustomerId)
                .Index(t => t.RunnerId)
                .Index(t => t.RestaurantId)
                .Index(t => t.SeatInfoId);
            
            CreateTable(
                "dbo.CustomerInfoes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FirstName = c.String(),
                        LastName = c.String(),
                        ImageUrl = c.String(),
                        Birthday = c.DateTime(nullable: false),
                        Gender = c.String(),
                        BraintreeCustomerId = c.String(),
                        Name = c.String(),
                        IsRetired = c.Boolean(nullable: false),
                        SeatInfo_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.SeatInfoes", t => t.SeatInfo_Id)
                .Index(t => t.SeatInfo_Id);
            
            CreateTable(
                "dbo.SeatInfoes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Section = c.String(),
                        Row = c.String(),
                        Chair = c.String(),
                        BraintreeCustomerId = c.String(),
                        AreaId = c.Int(nullable: false),
                        Name = c.String(),
                        IsRetired = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Areas", t => t.AreaId, cascadeDelete: false)
                .Index(t => t.AreaId);
            
            CreateTable(
                "dbo.RunnerInfoes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        IsRetired = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.RestaurantManagers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        RestaurantId = c.Int(nullable: false),
                        ManagerInfoId = c.Int(nullable: false),
                        Name = c.String(),
                        IsRetired = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ManagerInfoes", t => t.ManagerInfoId, cascadeDelete: true)
                .ForeignKey("dbo.Restaurants", t => t.RestaurantId, cascadeDelete: true)
                .Index(t => t.RestaurantId)
                .Index(t => t.ManagerInfoId);
            
            CreateTable(
                "dbo.ManagerInfoes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        IsRetired = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.VendorInfoes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        RestaurantId = c.Int(nullable: false),
                        Name = c.String(),
                        IsRetired = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Restaurants", t => t.RestaurantId, cascadeDelete: true)
                .Index(t => t.RestaurantId);
            
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
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        CustomerInfoId = c.Int(),
                        SeatInfoId = c.Int(),
                        RunnerInfoId = c.Int(),
                        ManagerInfoId = c.Int(),
                        VendorInfoId = c.Int(),
                        AdminInfoId = c.Int(),
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
                .ForeignKey("dbo.AdminInfoes", t => t.AdminInfoId)
                .ForeignKey("dbo.CustomerInfoes", t => t.CustomerInfoId)
                .ForeignKey("dbo.ManagerInfoes", t => t.ManagerInfoId)
                .ForeignKey("dbo.RunnerInfoes", t => t.RunnerInfoId)
                .ForeignKey("dbo.SeatInfoes", t => t.SeatInfoId)
                .ForeignKey("dbo.VendorInfoes", t => t.VendorInfoId)
                .Index(t => t.CustomerInfoId)
                .Index(t => t.SeatInfoId)
                .Index(t => t.RunnerInfoId)
                .Index(t => t.ManagerInfoId)
                .Index(t => t.VendorInfoId)
                .Index(t => t.AdminInfoId)
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
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
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
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUsers", "VendorInfoId", "dbo.VendorInfoes");
            DropForeignKey("dbo.AspNetUsers", "SeatInfoId", "dbo.SeatInfoes");
            DropForeignKey("dbo.AspNetUsers", "RunnerInfoId", "dbo.RunnerInfoes");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUsers", "ManagerInfoId", "dbo.ManagerInfoes");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUsers", "CustomerInfoId", "dbo.CustomerInfoes");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUsers", "AdminInfoId", "dbo.AdminInfoes");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.AdminInfoes", "ParkId", "dbo.Parks");
            DropForeignKey("dbo.VendorInfoes", "RestaurantId", "dbo.Restaurants");
            DropForeignKey("dbo.RestaurantManagers", "RestaurantId", "dbo.Restaurants");
            DropForeignKey("dbo.RestaurantManagers", "ManagerInfoId", "dbo.ManagerInfoes");
            DropForeignKey("dbo.Categories", "RestaurantId", "dbo.Restaurants");
            DropForeignKey("dbo.OrderItems", "OrderId", "dbo.Orders");
            DropForeignKey("dbo.Orders", "SeatInfoId", "dbo.SeatInfoes");
            DropForeignKey("dbo.Orders", "RunnerId", "dbo.RunnerInfoes");
            DropForeignKey("dbo.Orders", "RestaurantId", "dbo.Restaurants");
            DropForeignKey("dbo.Orders", "CustomerId", "dbo.CustomerInfoes");
            DropForeignKey("dbo.CustomerInfoes", "SeatInfo_Id", "dbo.SeatInfoes");
            DropForeignKey("dbo.SeatInfoes", "AreaId", "dbo.Areas");
            DropForeignKey("dbo.OrderItems", "ItemId", "dbo.Items");
            DropForeignKey("dbo.Items", "CategoryId", "dbo.Categories");
            DropForeignKey("dbo.Restaurants", "AreaId", "dbo.Areas");
            DropForeignKey("dbo.Areas", "ParkId", "dbo.Parks");
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.AspNetUsers", new[] { "AdminInfoId" });
            DropIndex("dbo.AspNetUsers", new[] { "VendorInfoId" });
            DropIndex("dbo.AspNetUsers", new[] { "ManagerInfoId" });
            DropIndex("dbo.AspNetUsers", new[] { "RunnerInfoId" });
            DropIndex("dbo.AspNetUsers", new[] { "SeatInfoId" });
            DropIndex("dbo.AspNetUsers", new[] { "CustomerInfoId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.VendorInfoes", new[] { "RestaurantId" });
            DropIndex("dbo.RestaurantManagers", new[] { "ManagerInfoId" });
            DropIndex("dbo.RestaurantManagers", new[] { "RestaurantId" });
            DropIndex("dbo.SeatInfoes", new[] { "AreaId" });
            DropIndex("dbo.CustomerInfoes", new[] { "SeatInfo_Id" });
            DropIndex("dbo.Orders", new[] { "SeatInfoId" });
            DropIndex("dbo.Orders", new[] { "RestaurantId" });
            DropIndex("dbo.Orders", new[] { "RunnerId" });
            DropIndex("dbo.Orders", new[] { "CustomerId" });
            DropIndex("dbo.OrderItems", new[] { "OrderId" });
            DropIndex("dbo.OrderItems", new[] { "ItemId" });
            DropIndex("dbo.Items", new[] { "CategoryId" });
            DropIndex("dbo.Categories", new[] { "RestaurantId" });
            DropIndex("dbo.Restaurants", new[] { "AreaId" });
            DropIndex("dbo.Areas", new[] { "ParkId" });
            DropIndex("dbo.AdminInfoes", new[] { "ParkId" });
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.VendorInfoes");
            DropTable("dbo.ManagerInfoes");
            DropTable("dbo.RestaurantManagers");
            DropTable("dbo.RunnerInfoes");
            DropTable("dbo.SeatInfoes");
            DropTable("dbo.CustomerInfoes");
            DropTable("dbo.Orders");
            DropTable("dbo.OrderItems");
            DropTable("dbo.Items");
            DropTable("dbo.Categories");
            DropTable("dbo.Restaurants");
            DropTable("dbo.Areas");
            DropTable("dbo.Parks");
            DropTable("dbo.AdminInfoes");
        }
    }
}
