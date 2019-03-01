using Assignment5.Models;
using System.Data.Entity;


namespace Assignment5.Data.Repositories
{
    internal partial class InvoiceRepository : BaseRepository<Invoice>, IInvoiceRepository
    {
        internal InvoiceRepository(DbContext db) : base(db) { }
    }

    public partial interface IInvoiceRepository : IRepository<Invoice> { }
}
