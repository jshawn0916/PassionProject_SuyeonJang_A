﻿namespace PassionProject_SuyeonJang_A.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class traveler : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Travelers",
                c => new
                    {
                        TravelerId = c.Int(nullable: false, identity: true),
                        TravelerFirstName = c.String(),
                        TravelerLastName = c.String(),
                        TravelerEmail = c.String(),
                    })
                .PrimaryKey(t => t.TravelerId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Travelers");
        }
    }
}
