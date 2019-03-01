using Assignment5.Models;
using System.Data.Entity;


namespace Assignment5.Data.Repositories
{
    internal partial class CustomerRepository : BaseRepository<Customer>, ICustomerRepository
    {
        internal CustomerRepository(DbContext db) : base(db) { }
    }

    public partial interface ICustomerRepository : IRepository<Customer> { }
}
