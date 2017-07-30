namespace Twitch10WcfService.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Builds",
                c => new
                    {
                        BuildId = c.Int(nullable: false, identity: true),
                        PlayerName = c.String(),
                        ChampionId = c.Int(nullable: false),
                        Item1Id = c.Int(nullable: false),
                        Item2Id = c.Int(nullable: false),
                        Item3Id = c.Int(nullable: false),
                        Item4Id = c.Int(nullable: false),
                        Item5Id = c.Int(nullable: false),
                        Item6Id = c.Int(nullable: false),
                        Spell1Id = c.Int(nullable: false),
                        Spell2Id = c.Int(nullable: false),
                        MatchId = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.BuildId);
            
            CreateTable(
                "dbo.UserHasBuilds",
                c => new
                    {
                        UserId = c.Int(nullable: false),
                        BuildId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.UserId, t.BuildId })
                .ForeignKey("dbo.Builds", t => t.BuildId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.BuildId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        UserId = c.Int(nullable: false, identity: true),
                        UserName = c.String(),
                    })
                .PrimaryKey(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserHasBuilds", "UserId", "dbo.Users");
            DropForeignKey("dbo.UserHasBuilds", "BuildId", "dbo.Builds");
            DropIndex("dbo.UserHasBuilds", new[] { "BuildId" });
            DropIndex("dbo.UserHasBuilds", new[] { "UserId" });
            DropTable("dbo.Users");
            DropTable("dbo.UserHasBuilds");
            DropTable("dbo.Builds");
        }
    }
}
