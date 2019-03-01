using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Assignment5.Controllers
{
    public class TrackAdd
    {
        [Required]
        [StringLength(200)]
        [Display(Name = "Track Name")]
        public string Name { get; set; }

        [StringLength(220)]
        public string Composer { get; set; }

        public int Milliseconds { get; set; }

        public decimal UnitPrice { get; set; }

        public int AlbumId { get; set; }

        public int MediaTypeId { get; set; }

        public int GenreId { get; set; }
    }

    public class TrackAddForm : TrackAdd
    {
        [Display(Name = "Album")]
        public SelectList AlbumList { get; set; }

        [Display(Name = "Genre")]
        public SelectList GenreList { get; set; }

        // Attention 24 - SelectList for the associated item
        [Display(Name = "Media Type")]
        public SelectList MediaTypeList { get; set; }

        public string AlbumName { get; set; }

        public string GenreName { get; set; }

        public string MediaTypeName { get; set; }
    }

    public class TrackBase : TrackAdd
    {
        [Key]
        public int TrackId { get; set; }
    }

    public class TrackWithDetail : TrackBase
    {
        // Composed properties, Album > Artist > Name
        public string AlbumTitle { get; set; }

        public string AlbumArtistName { get; set; }

        public MediaTypeBase MediaType { get; set; }

        public GenreBase Genre { get; set; }
    }
}