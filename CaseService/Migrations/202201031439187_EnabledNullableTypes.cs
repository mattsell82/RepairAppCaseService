namespace CaseService.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EnabledNullableTypes : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Cases", "EstimatedDeliveryDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Cases", "EstimatedDeliveryDate", c => c.DateTime(nullable: false));
        }
    }
}
