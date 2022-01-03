namespace CaseService.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddQuotDBset : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Quotes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CaseId = c.Int(nullable: false),
                        DateTime = c.DateTime(nullable: false),
                        Cost = c.Double(nullable: false),
                        Answered = c.Boolean(nullable: false),
                        Accepted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Cases", t => t.CaseId, cascadeDelete: true)
                .Index(t => t.CaseId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Quotes", "CaseId", "dbo.Cases");
            DropIndex("dbo.Quotes", new[] { "CaseId" });
            DropTable("dbo.Quotes");
        }
    }
}
