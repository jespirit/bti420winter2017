using Assignment5.Models;
using System.Data.Entity;


namespace Assignment5.Data.Repositories
{
    internal partial class TrackRepository : BaseRepository<Track>, ITrackRepository
    {
        internal TrackRepository(DbContext db) : base(db) { }
    }

    public partial interface ITrackRepository : IRepository<Track> { }
}
