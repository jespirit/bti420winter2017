using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Assignment5.Controllers
{
    public class MediaTypeBase
    {
        [Key]
        public int Id { get; set; }

        [StringLength(120)]
        [Display(Name = "Format")]
        public string Name { get; set; }
    }
}