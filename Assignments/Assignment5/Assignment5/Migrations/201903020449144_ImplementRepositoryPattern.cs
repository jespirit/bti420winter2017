namespace Assignment5.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ImplementRepositoryPattern : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Track", "AlbumId", "dbo.Album");
            DropForeignKey("dbo.Album", "ArtistId", "dbo.Artist");
            DropForeignKey("dbo.InvoiceLine", "TrackId", "dbo.Track");
            DropForeignKey("dbo.PlaylistTrack", "TrackId", "dbo.Track");
            DropForeignKey("dbo.Track", "GenreId", "dbo.Genre");
            DropForeignKey("dbo.InvoiceLine", "InvoiceId", "dbo.Invoice");
            DropForeignKey("dbo.Invoice", "CustomerId", "dbo.Customer");
            DropForeignKey("dbo.Customer", "SupportRepId", "dbo.Employee");
            DropForeignKey("dbo.Employee", "ReportsTo", "dbo.Employee");
            DropForeignKey("dbo.Track", "MediaTypeId", "dbo.MediaType");
            DropForeignKey("dbo.PlaylistTrack", "PlaylistId", "dbo.Playlist");
            DropPrimaryKey("dbo.Album");
            DropPrimaryKey("dbo.Artist");
            DropPrimaryKey("dbo.Track");
            DropPrimaryKey("dbo.Genre");
            DropPrimaryKey("dbo.InvoiceLine");
            DropPrimaryKey("dbo.Invoice");
            DropPrimaryKey("dbo.Customer");
            DropPrimaryKey("dbo.Employee");
            DropPrimaryKey("dbo.MediaType");
            DropPrimaryKey("dbo.Playlist");
            AddColumn("dbo.Album", "Id", c => c.Int(nullable: false, identity: true));
            AddColumn("dbo.Artist", "Id", c => c.Int(nullable: false, identity: true));
            AddColumn("dbo.Track", "Id", c => c.Int(nullable: false, identity: true));
            AddColumn("dbo.Genre", "Id", c => c.Int(nullable: false, identity: true));
            AddColumn("dbo.InvoiceLine", "Id", c => c.Int(nullable: false, identity: true));
            AddColumn("dbo.Invoice", "Id", c => c.Int(nullable: false, identity: true));
            AddColumn("dbo.Customer", "Id", c => c.Int(nullable: false, identity: true));
            AddColumn("dbo.Employee", "Id", c => c.Int(nullable: false, identity: true));
            AddColumn("dbo.MediaType", "Id", c => c.Int(nullable: false, identity: true));
            AddColumn("dbo.Playlist", "Id", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.Album", "Id");
            AddPrimaryKey("dbo.Artist", "Id");
            AddPrimaryKey("dbo.Track", "Id");
            AddPrimaryKey("dbo.Genre", "Id");
            AddPrimaryKey("dbo.InvoiceLine", "Id");
            AddPrimaryKey("dbo.Invoice", "Id");
            AddPrimaryKey("dbo.Customer", "Id");
            AddPrimaryKey("dbo.Employee", "Id");
            AddPrimaryKey("dbo.MediaType", "Id");
            AddPrimaryKey("dbo.Playlist", "Id");
            AddForeignKey("dbo.Track", "AlbumId", "dbo.Album", "Id");
            AddForeignKey("dbo.Album", "ArtistId", "dbo.Artist", "Id");
            AddForeignKey("dbo.InvoiceLine", "TrackId", "dbo.Track", "Id");
            AddForeignKey("dbo.PlaylistTrack", "TrackId", "dbo.Track", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Track", "GenreId", "dbo.Genre", "Id");
            AddForeignKey("dbo.InvoiceLine", "InvoiceId", "dbo.Invoice", "Id");
            AddForeignKey("dbo.Invoice", "CustomerId", "dbo.Customer", "Id");
            AddForeignKey("dbo.Customer", "SupportRepId", "dbo.Employee", "Id");
            AddForeignKey("dbo.Employee", "ReportsTo", "dbo.Employee", "Id");
            AddForeignKey("dbo.Track", "MediaTypeId", "dbo.MediaType", "Id");
            AddForeignKey("dbo.PlaylistTrack", "PlaylistId", "dbo.Playlist", "Id", cascadeDelete: true);
            DropColumn("dbo.Album", "AlbumId");
            DropColumn("dbo.Artist", "ArtistId");
            DropColumn("dbo.Track", "TrackId");
            DropColumn("dbo.Genre", "GenreId");
            DropColumn("dbo.InvoiceLine", "InvoiceLineId");
            DropColumn("dbo.Invoice", "InvoiceId");
            DropColumn("dbo.Customer", "CustomerId");
            DropColumn("dbo.Employee", "EmployeeId");
            DropColumn("dbo.MediaType", "MediaTypeId");
            DropColumn("dbo.Playlist", "PlaylistId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Playlist", "PlaylistId", c => c.Int(nullable: false, identity: true));
            AddColumn("dbo.MediaType", "MediaTypeId", c => c.Int(nullable: false, identity: true));
            AddColumn("dbo.Employee", "EmployeeId", c => c.Int(nullable: false, identity: true));
            AddColumn("dbo.Customer", "CustomerId", c => c.Int(nullable: false, identity: true));
            AddColumn("dbo.Invoice", "InvoiceId", c => c.Int(nullable: false, identity: true));
            AddColumn("dbo.InvoiceLine", "InvoiceLineId", c => c.Int(nullable: false, identity: true));
            AddColumn("dbo.Genre", "GenreId", c => c.Int(nullable: false, identity: true));
            AddColumn("dbo.Track", "TrackId", c => c.Int(nullable: false, identity: true));
            AddColumn("dbo.Artist", "ArtistId", c => c.Int(nullable: false, identity: true));
            AddColumn("dbo.Album", "AlbumId", c => c.Int(nullable: false, identity: true));
            DropForeignKey("dbo.PlaylistTrack", "PlaylistId", "dbo.Playlist");
            DropForeignKey("dbo.Track", "MediaTypeId", "dbo.MediaType");
            DropForeignKey("dbo.Employee", "ReportsTo", "dbo.Employee");
            DropForeignKey("dbo.Customer", "SupportRepId", "dbo.Employee");
            DropForeignKey("dbo.Invoice", "CustomerId", "dbo.Customer");
            DropForeignKey("dbo.InvoiceLine", "InvoiceId", "dbo.Invoice");
            DropForeignKey("dbo.Track", "GenreId", "dbo.Genre");
            DropForeignKey("dbo.PlaylistTrack", "TrackId", "dbo.Track");
            DropForeignKey("dbo.InvoiceLine", "TrackId", "dbo.Track");
            DropForeignKey("dbo.Album", "ArtistId", "dbo.Artist");
            DropForeignKey("dbo.Track", "AlbumId", "dbo.Album");
            DropPrimaryKey("dbo.Playlist");
            DropPrimaryKey("dbo.MediaType");
            DropPrimaryKey("dbo.Employee");
            DropPrimaryKey("dbo.Customer");
            DropPrimaryKey("dbo.Invoice");
            DropPrimaryKey("dbo.InvoiceLine");
            DropPrimaryKey("dbo.Genre");
            DropPrimaryKey("dbo.Track");
            DropPrimaryKey("dbo.Artist");
            DropPrimaryKey("dbo.Album");
            DropColumn("dbo.Playlist", "Id");
            DropColumn("dbo.MediaType", "Id");
            DropColumn("dbo.Employee", "Id");
            DropColumn("dbo.Customer", "Id");
            DropColumn("dbo.Invoice", "Id");
            DropColumn("dbo.InvoiceLine", "Id");
            DropColumn("dbo.Genre", "Id");
            DropColumn("dbo.Track", "Id");
            DropColumn("dbo.Artist", "Id");
            DropColumn("dbo.Album", "Id");
            AddPrimaryKey("dbo.Playlist", "PlaylistId");
            AddPrimaryKey("dbo.MediaType", "MediaTypeId");
            AddPrimaryKey("dbo.Employee", "EmployeeId");
            AddPrimaryKey("dbo.Customer", "CustomerId");
            AddPrimaryKey("dbo.Invoice", "InvoiceId");
            AddPrimaryKey("dbo.InvoiceLine", "InvoiceLineId");
            AddPrimaryKey("dbo.Genre", "GenreId");
            AddPrimaryKey("dbo.Track", "TrackId");
            AddPrimaryKey("dbo.Artist", "ArtistId");
            AddPrimaryKey("dbo.Album", "AlbumId");
            AddForeignKey("dbo.PlaylistTrack", "PlaylistId", "dbo.Playlist", "PlaylistId", cascadeDelete: true);
            AddForeignKey("dbo.Track", "MediaTypeId", "dbo.MediaType", "MediaTypeId");
            AddForeignKey("dbo.Employee", "ReportsTo", "dbo.Employee", "EmployeeId");
            AddForeignKey("dbo.Customer", "SupportRepId", "dbo.Employee", "EmployeeId");
            AddForeignKey("dbo.Invoice", "CustomerId", "dbo.Customer", "CustomerId");
            AddForeignKey("dbo.InvoiceLine", "InvoiceId", "dbo.Invoice", "InvoiceId");
            AddForeignKey("dbo.Track", "GenreId", "dbo.Genre", "GenreId");
            AddForeignKey("dbo.PlaylistTrack", "TrackId", "dbo.Track", "TrackId", cascadeDelete: true);
            AddForeignKey("dbo.InvoiceLine", "TrackId", "dbo.Track", "TrackId");
            AddForeignKey("dbo.Album", "ArtistId", "dbo.Artist", "ArtistId");
            AddForeignKey("dbo.Track", "AlbumId", "dbo.Album", "AlbumId");
        }
    }
}
