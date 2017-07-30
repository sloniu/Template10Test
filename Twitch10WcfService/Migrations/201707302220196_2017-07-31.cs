namespace Twitch10WcfService.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _20170731 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Builds", "Region", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Builds", "Region");
        }
    }
}
