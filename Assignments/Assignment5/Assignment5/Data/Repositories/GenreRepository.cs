using Assignment5.Models;
using System.Data.Entity;


namespace Assignment5.Data.Repositories
{
    internal partial class GenreRepository : BaseRepository<Genre>, IGenreRepository
    {
        internal GenreRepository(DbContext db) : base(db) { }
    }

    public partial interface IGenreRepository : IRepository<Genre> { }
}
