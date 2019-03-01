using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Assignment5.Controllers
{
    public class InvoiceLineBase
    {
        [Key]
        [Display(Name = "Invoice Line ID")]
        public int InvoiceLineId { get; set; }

        public int InvoiceId { get; set; }

        [Display(Name = "Track ID")]
        public int TrackId { get; set; }

        [Display(Name = "Unit Price")]
        public decimal UnitPrice { get; set; }

        public int Quantity { get; set; }
    }

    public class InvoiceLineWithInfo : InvoiceLineBase
    {
        public TrackWithDetail Track { get; set; }
    }
}