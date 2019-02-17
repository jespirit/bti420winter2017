using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;

namespace Assignment4.Controllers
{
    public class InvoiceApiController : ApiController
    {
        Manager m = new Manager();

        [HttpGet]
        [ResponseType(typeof(object))]
        [ActionName("get")]
        public IHttpActionResult GetInvoice(int id)
        {
            try
            {
                var invoice = m.InvoiceGetById(id);

                return Ok(invoice);
            }
            catch (Exception ex)
            {
                // Ignore
            }

            return BadRequest();
        }
    }
}
