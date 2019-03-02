namespace Assignment5.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ImplementRepositoryPattern : DbMigration
    {
        public override void Up()
        {
            // Drop foreign and primary keys by original naming convention
            DropForeignKey("dbo.Album", "FK_AlbumArtistId");
            DropForeignKey("dbo.Customer", "FK_CustomerSupportRepId");
            DropForeignKey("dbo.Employee", "FK_EmployeeReportsTo");
            DropForeignKey("dbo.Invoice", "FK_InvoiceCustomerId");
            DropForeignKey("dbo.InvoiceLine", "FK_InvoiceLineInvoiceId");
            DropForeignKey("dbo.InvoiceLine", "FK_InvoiceLineTrackId");
            DropForeignKey("dbo.PlaylistTrack", "FK_PlaylistTrackPlaylistId");
            DropForeignKey("dbo.PlaylistTrack", "FK_PlaylistTrackTrackId");
            DropForeignKey("dbo.Track", "FK_TrackAlbumId");
            DropForeignKey("dbo.Track", "FK_TrackGenreId");
            DropForeignKey("dbo.Track", "FK_TrackMediaTypeId");

            // Rename Primary Key column to just "Id"
            RenameColumn("dbo.Album", "AlbumId", "Id");
            RenameColumn("dbo.Artist", "ArtistId", "Id");
            RenameColumn("dbo.Customer", "CustomerId", "Id");
            RenameColumn("dbo.Employee", "EmployeeId", "Id");
            RenameColumn("dbo.Genre", "GenreId", "Id");
            RenameColumn("dbo.Invoice", "InvoiceId", "Id");
            RenameColumn("dbo.InvoiceLine", "InvoiceLineId", "Id");
            RenameColumn("dbo.MediaType", "MediaTypeId", "Id");
            RenameColumn("dbo.Playlist", "PlaylistId", "Id");
            RenameColumn("dbo.Track", "TrackId", "Id");
            // Don't rename columns in the many-to-many PlaylistTrack table
            //RenameColumn("dbo.PlaylistTrack", "TrackId", "Id");
            //RenameColumn("dbo.PlaylistTrack", "PlaylistId", "Id");

            AddForeignKey("dbo.Album", "ArtistId", "dbo.Artist", "Id", name: "FK_AlbumArtistId");
            AddForeignKey("dbo.Employee", "ReportsTo", "dbo.Employee", "Id", name: "FK_EmployeeReportsTo");
            AddForeignKey("dbo.Customer", "SupportRepId", "dbo.Employee", "Id", name: "FK_CustomerSupportRepId");
            AddForeignKey("dbo.Invoice", "CustomerId", "dbo.Customer", "Id", name: "FK_InvoiceCustomerId");
            AddForeignKey("dbo.InvoiceLine", "InvoiceId", "dbo.Invoice", "Id", name: "FK_InvoiceLineInvoiceId");
            AddForeignKey("dbo.InvoiceLine", "TrackId", "dbo.Track", "Id", name: "FK_InvoiceLineTrackId");
            AddForeignKey("dbo.PlaylistTrack", "PlaylistId", "dbo.Playlist", "Id", cascadeDelete: true, name: "FK_PlaylistTrackPlaylistId");
            AddForeignKey("dbo.PlaylistTrack", "TrackId", "dbo.Track", "Id", cascadeDelete: true, name: "FK_PlaylistTrackTrackId");
            AddForeignKey("dbo.Track", "AlbumId", "dbo.Album", "Id", name: "FK_TrackAlbumId");
            AddForeignKey("dbo.Track", "GenreId", "dbo.Genre", "Id", name: "FK_TrackGenreId");
            AddForeignKey("dbo.Track", "MediaTypeId", "dbo.MediaType", "Id", name: "FK_TrackMediaTypeId");
        }
        
        public override void Down()
        {
            // Drop foreign and primary keys by original naming convention
            DropForeignKey("dbo.Album", "FK_AlbumArtistId");
            DropForeignKey("dbo.Customer", "FK_CustomerSupportRepId");
            DropForeignKey("dbo.Employee", "FK_EmployeeReportsTo");
            DropForeignKey("dbo.Invoice", "FK_InvoiceCustomerId");
            DropForeignKey("dbo.InvoiceLine", "FK_InvoiceLineInvoiceId");
            DropForeignKey("dbo.InvoiceLine", "FK_InvoiceLineTrackId");
            DropForeignKey("dbo.PlaylistTrack", "FK_PlaylistTrackPlaylistId");
            DropForeignKey("dbo.PlaylistTrack", "FK_PlaylistTrackTrackId");
            DropForeignKey("dbo.Track", "FK_TrackAlbumId");
            DropForeignKey("dbo.Track", "FK_TrackGenreId");
            DropForeignKey("dbo.Track", "FK_TrackMediaTypeId");

            // Rename Primary Key column back to original column names
            RenameColumn("dbo.Album", "Id", "AlbumId");
            RenameColumn("dbo.Artist", "Id", "ArtistId");
            RenameColumn("dbo.Customer", "Id", "CusotmerId");
            RenameColumn("dbo.Employee", "Id", "EmployeeId");
            RenameColumn("dbo.Genre", "Id", "GenreId");
            RenameColumn("dbo.Invoice", "Id", "InvoiceId");
            RenameColumn("dbo.InvoiceLine", "Id", "InvoiceLineId");
            RenameColumn("dbo.MediaType", "Id", "MediaTypeId");
            RenameColumn("dbo.Playlist", "Id", "PlaylistId");
            RenameColumn("dbo.Track", "Id", "TrackId");
            //RenameColumn("dbo.PlaylistTrack", "Id", "TrackId");
            //RenameColumn("dbo.PlaylistTrack", "Id", "PlaylistId");

            AddForeignKey("dbo.Album", "ArtistId", "dbo.Artist", "ArtistId", name: "FK_AlbumArtistId");
            AddForeignKey("dbo.Customer", "SupportRepId", "dbo.Employee", "EmployeeId", name: "FK_CustomerSupportRepId");
            AddForeignKey("dbo.Employee", "ReportsTo", "dbo.Employee", "EmployeeId", name: "FK_EmployeeReportsTo");
            AddForeignKey("dbo.Invoice", "CustomerId", "dbo.Customer", "CustomerId", name: "FK_InvoiceCustomer");
            AddForeignKey("dbo.InvoiceLine", "InvoiceId", "dbo.Invoice", "InvoiceId", name: "FK_InvoiceLineInvoiceId");
            AddForeignKey("dbo.InvoiceLine", "TrackId", "dbo.Track", "TrackId", name: "FK_InvoiceLineTrackId");
            AddForeignKey("dbo.PlaylistTrack", "PlaylistId", "dbo.Playlist", "PlaylistId", cascadeDelete: true, name: "FK_PlaylistTrackPlaylistId");
            AddForeignKey("dbo.PlaylistTrack", "TrackId", "dbo.Track", "TrackId", cascadeDelete: true, name: "FK_PlaylistTrackTrackId");
            AddForeignKey("dbo.Track", "AlbumId", "dbo.Album", "AlbumId", name: "FK_TrackAlbumId");
            AddForeignKey("dbo.Track", "GenreId", "dbo.Genre", "GenreId", name: "FK_TrackGenreId");
            AddForeignKey("dbo.Track", "MediaTypeId", "dbo.MediaType", "MediaTypeId", name: "FK_TrackMediaTypeId");
        }
    }
}
