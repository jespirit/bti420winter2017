using Assignment5.Data.Entity;

namespace Assignment5.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class PlaylistTrack
    {
        public PlaylistTrack()
        {
        }

        public int PlaylistId { get; set; }

        public int TrackId { get; set; }

        /// <summary>
        /// What order to play the track
        /// </summary>
        public int PlayOrder { get; set; }

        public virtual Playlist Playlist { get; set; }

        public virtual Track Track { get; set; }
    }
}
