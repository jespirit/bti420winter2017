using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Assignment5.Controllers
{
    public class AlbumBase
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(160)]
        public string Title { get; set; }
    }
}