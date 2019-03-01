using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Assignment5.Controllers
{
    public class TrackBase
    {
        [Required]
        [StringLength(200)]
        [Display(Name = "Track Name")]
        public string Name { get; set; }

        public int? AlbumId { get; set; }

        public int MediaTypeId { get; set; }

        public int? GenreId { get; set; }

        [StringLength(220)]
        public string Composer { get; set; }

        public int Milliseconds { get; set; }

        public int? Bytes { get; set; }

        public decimal UnitPrice { get; set; }
    }

    public class TrackWithInfo : TrackBase
    {
        public AlbumBase Album { get; set; }

        public string AlbumArtistName { get; set; }

        //public GenreBase Genre { get; set; }
        public string GenreName { get; set; }

        public MediaTypeBase MediaType { get; set; }
    }
}