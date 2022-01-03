namespace CaseService.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Cases",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        EmployeeId = c.Int(nullable: false),
                        DateTime = c.DateTime(nullable: false),
                        CustomerId = c.Int(nullable: false),
                        ProductId = c.Int(nullable: false),
                        ErrorDescription = c.String(nullable: false),
                        StatusId = c.Int(nullable: false),
                        Guid = c.Guid(nullable: false),
                        EstimatedDeliveryDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Customers", t => t.CustomerId, cascadeDelete: true)
                .ForeignKey("dbo.Status", t => t.StatusId, cascadeDelete: true)
                .Index(t => t.CustomerId)
                .Index(t => t.StatusId);
            
            CreateTable(
                "dbo.Customers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FirstName = c.String(nullable: false, maxLength: 50),
                        LastName = c.String(maxLength: 50),
                        Email = c.String(nullable: false, maxLength: 200),
                        Phone = c.String(nullable: false, maxLength: 50),
                        Address = c.String(maxLength: 50),
                        Zip = c.String(maxLength: 50),
                        City = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Status",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Cases", "StatusId", "dbo.Status");
            DropForeignKey("dbo.Cases", "CustomerId", "dbo.Customers");
            DropIndex("dbo.Cases", new[] { "StatusId" });
            DropIndex("dbo.Cases", new[] { "CustomerId" });
            DropTable("dbo.Status");
            DropTable("dbo.Customers");
            DropTable("dbo.Cases");
        }
    }
}
