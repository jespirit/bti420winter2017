using Assignment5.Models;
using System.Data.Entity;


namespace Assignment5.Data.Repositories
{
    internal partial class ArtistRepository : BaseRepository<Artist>, IArtistRepository
    {
        internal ArtistRepository(DbContext db) : base(db) { }
    }

    public partial interface IArtistRepository : IRepository<Artist> { }
}
