using Assignment5.Models;
using System.Data.Entity;


namespace Assignment5.Data.Repositories
{
    internal partial class InvoiceLineRepository : BaseRepository<InvoiceLine>, IInvoiceLineRepository
    {
        internal InvoiceLineRepository(DbContext db) : base(db) { }
    }

    public partial interface IInvoiceLineRepository : IRepository<InvoiceLine> { }
}
