namespace Assignment5.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddPlaylistTrackM2nTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PlaylistTrack", "PlayOrder", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.PlaylistTrack", "PlayOrder");
        }
    }
}
