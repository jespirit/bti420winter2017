using Assignment5.Models;
using System.Data.Entity;


namespace Assignment5.Data.Repositories
{
    internal partial class AlbumRepository : BaseRepository<Album>, IAlbumRepository
    {
        internal AlbumRepository(DbContext db) : base(db) { }
    }

    public partial interface IAlbumRepository : IRepository<Album> { }
}
