namespace Inzynierka.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class secondMigration : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ActivityPlan", "Description", c => c.String());
            AddColumn("dbo.ActivityPlan", "Type", c => c.Int(nullable: false));
            AddColumn("dbo.ActivityPlan", "WorkId", c => c.Long(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ActivityPlan", "WorkId");
            DropColumn("dbo.ActivityPlan", "Type");
            DropColumn("dbo.ActivityPlan", "Description");
        }
    }
}
