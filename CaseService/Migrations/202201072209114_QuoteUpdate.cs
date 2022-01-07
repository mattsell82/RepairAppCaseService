namespace CaseService.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class QuoteUpdate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Quotes", "Measure", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Quotes", "Measure");
        }
    }
}
