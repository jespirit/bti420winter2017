using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Assignment5.Controllers
{
    public class PlaylistTrackBase
    {
        public int PlaylistId { get; set; }

        public int TrackId { get; set; }

        public int PlayOrder { get; set; }

        public TrackBase Track { get; set; }
    }

    public class PlaylistBase
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(120)]
        public string Name { get; set; }
    }

    public class PlaylistWithTracks : PlaylistBase
    {
        public PlaylistWithTracks()
        {
            PlaylistTracks = new List<PlaylistTrackBase>();
        }

        [Display(Name = "List of tracks")]
        public IEnumerable<PlaylistTrackBase> PlaylistTracks { get; set; }
    }

    // ############################################################

    // Attention 06 - Edit tracks for a playlist
    // Send TO the HTML Form
    public class PlaylistEditTracksForm
    {
        public int Id { get; set; }

        [Required, StringLength(100)]
        [Display(Name = "Playlist name")]
        public string Name { get; set; }

        // Attention 07 - Multiple select requires a MultiSelectList object
        [Display(Name = "Tracks")]
        public MultiSelectList TrackList { get; set; }
    }

    // Data submitted by the browser user
    public class PlaylistEditTracks
    {
        public PlaylistEditTracks()
        {
            TrackIds = new List<int>();
        }

        public int Id { get; set; }

        // Attention 08 - Incoming collection of selected job duty identifiers
        public IEnumerable<int> TrackIds { get; set; }
    }
}