using Assignment5.Models;
using System.Data.Entity;


namespace Assignment5.Data.Repositories
{
    internal partial class EmployeeRepository : BaseRepository<Employee>, IEmployeeRepository
    {
        internal EmployeeRepository(DbContext db) : base(db) { }
    }

    public partial interface IEmployeeRepository : IRepository<Employee> { }
}
