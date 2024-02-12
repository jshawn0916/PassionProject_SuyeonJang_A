namespace PassionProject_SuyeonJang_A.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class journeydestination : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Destinations", "JourneyId", c => c.Int(nullable: false));
            CreateIndex("dbo.Destinations", "JourneyId");
            AddForeignKey("dbo.Destinations", "JourneyId", "dbo.Journeys", "JourneyId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Destinations", "JourneyId", "dbo.Journeys");
            DropIndex("dbo.Destinations", new[] { "JourneyId" });
            DropColumn("dbo.Destinations", "JourneyId");
        }
    }
}
