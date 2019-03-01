using Assignment5.Models;
using System.Data.Entity;


namespace Assignment5.Data.Repositories
{
    internal partial class PlaylistRepository : BaseRepository<Playlist>, IPlaylistRepository
    {
        internal PlaylistRepository(DbContext db) : base(db) { }
    }

    public partial interface IPlaylistRepository : IRepository<Playlist> { }
}
