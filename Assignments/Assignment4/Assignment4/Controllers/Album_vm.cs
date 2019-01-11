using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Assignment4.Controllers
{
    public class AlbumBase
    {
        [Key]
        public int AlbumId { get; set; }

        [Required]
        [StringLength(160)]
        public string Title { get; set; }

        public int ArtistId { get; set; }
    }

    public class AlbumWithArtist : AlbumBase
    {
        public ArtistBase Artist { get; set; }
    }
}