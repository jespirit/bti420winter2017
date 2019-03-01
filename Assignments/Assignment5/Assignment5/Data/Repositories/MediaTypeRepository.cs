using Assignment5.Models;
using System.Data.Entity;


namespace Assignment5.Data.Repositories
{
    internal partial class MediaTypeRepository : BaseRepository<MediaType>, IMediaTypeRepository
    {
        internal MediaTypeRepository(DbContext db) : base(db) { }
    }

    public partial interface IMediaTypeRepository : IRepository<MediaType> { }
}
