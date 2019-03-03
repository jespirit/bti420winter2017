using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
// new...
using AutoMapper;
using Assignment5.Data;
using Assignment5.Data.Repositories;
using Assignment5.Controllers;
using Assignment5.Models;
using System.Linq.Expressions;

namespace Assignment5.Data.Managers
{
    public class ChinookDataManager : BaseUnitOfWork, IChinookDataManager
    {
        // AutoMapper components
        MapperConfiguration _config;
        public IMapper _mapper;

        public ChinookDataManager() : base(new ChinookDbContext())
        {
            this.Initialize();
        }

        public ChinookDataManager(DbContext db) : base(db)
        {
            this.Initialize();
        }

        public void Initialize()
        {
            this.Albums = new AlbumRepository(this._Db);
            this.Artists = new ArtistRepository(this._Db);
            this.Customers = new CustomerRepository(this._Db);
            this.Employees = new EmployeeRepository(this._Db);
            this.Genres = new GenreRepository(this._Db);
            this.Invoices = new InvoiceRepository(this._Db);
            this.InvoiceLines = new InvoiceLineRepository(this._Db);
            this.MediaTypes = new MediaTypeRepository(this._Db);
            this.Playlists = new PlaylistRepository(this._Db);
            this.Tracks = new TrackRepository(this._Db);

            // If necessary, add more constructor code here...

            // Configure the AutoMapper components
            _config = new MapperConfiguration(cfg =>
            {
                // Define the mappings below, for example...
                // cfg.CreateMap<SourceType, DestinationType>();
                // cfg.CreateMap<Employee, EmployeeBase>();

                cfg.CreateMap<Employee, EmployeeBase>();

                // Creating a new employee
                cfg.CreateMap<EmployeeAdd, Employee>();

                // Displaying 
                //cfg.CreateMap<EmployeeAdd, EmployeeBase>();

                // To browser form
                cfg.CreateMap<EmployeeBase, EmployeeEditProfileInfoForm>();

                cfg.CreateMap<Models.Customer, Controllers.CustomerBase>();

                // Invoice Details mappings
                //----------------------------------------

                cfg.CreateMap<Album, AlbumBase>();
                //cfg.CreateMap<Album, AlbumWithArtist>();

                cfg.CreateMap<Artist, ArtistBase>();

                cfg.CreateMap<Customer, CustomerWithDetail>();

                cfg.CreateMap<Genre, GenreBase>();

                cfg.CreateMap<Invoice, InvoiceBase>();
                cfg.CreateMap<Invoice, InvoiceWithDetail>();

                cfg.CreateMap<InvoiceLine, InvoiceLineBase>();
                cfg.CreateMap<InvoiceLine, InvoiceLineWithDetail>();

                cfg.CreateMap<MediaType, MediaTypeBase>();

                cfg.CreateMap<Track, TrackBase>();
                cfg.CreateMap<Track, TrackWithDetail>();

                cfg.CreateMap<Track, TrackAddForm>();
                cfg.CreateMap<TrackAdd, Track>();

                // Playlist mappings
                //----------------------------------------

                cfg.CreateMap<Playlist, PlaylistBase>();
                cfg.CreateMap<Playlist, PlaylistWithTracks>();
                cfg.CreateMap<Playlist, PlaylistEditTracksForm>();

                cfg.CreateMap<PlaylistEditTracks, Playlist>();
            });

            _mapper = _config.CreateMapper();

            // Turn off the Entity Framework (EF) proxy creation features
            // We do NOT want the EF to track changes - we'll do that ourselves
            this._Db.Configuration.ProxyCreationEnabled = false;

            // Also, turn off lazy loading...
            // We want to retain control over fetching related objects
            this._Db.Configuration.LazyLoadingEnabled = false;
        }

        public IAlbumRepository Albums { get; internal set; }
        public IArtistRepository Artists { get; internal set; }
        public ICustomerRepository Customers { get; internal set; }
        public IEmployeeRepository Employees { get; internal set; }
        public IGenreRepository Genres { get; internal set; }
        public IInvoiceLineRepository InvoiceLines { get; internal set; }
        public IInvoiceRepository Invoices { get; internal set; }
        public IMediaTypeRepository MediaTypes { get; internal set; }
        public IPlaylistRepository Playlists { get; internal set; }
        public ITrackRepository Tracks { get; internal set; }

        public enum IncludeLevel
        {
            None,
            Minimal,
            Comprehensive
        }

        protected Dictionary<IncludeLevel, Expression<Func<Track, object>>[]> IncludeTrackLevel = new Dictionary<IncludeLevel, Expression<Func<Track, object>>[]>
        {
            {
                IncludeLevel.None, null
            },
            {
                IncludeLevel.Minimal, null
            },
            {
                IncludeLevel.Comprehensive, new Expression<Func<Track, object>>[]
                {
                    includes => includes.Album.Artist,
                    includes => includes.Genre,
                    includes => includes.MediaType
                }
            }
        };

        public void RefreshEntityState()
        {
            ((ChinookDbContext)this._Db).RefreshEntityState();
        }

        // Add methods below
        // Controllers will call these methods
        // Ensure that the methods accept and deliver ONLY view model objects and collections
        // The collection return type is almost always IEnumerable<T>

        // Suggested naming convention: Entity + task/action
        // For example:
        // ProductGetAll()
        // ProductGetById()
        // ProductAdd()
        // ProductEdit()
        // ProductDelete()

        // ############################################################
        // Customer
        // ############################################################

        // Attention 03 - Get all customers
        // Notice the return type - it is almost ALWAYS a view model object or collection
        public IEnumerable<CustomerBase> CustomerGetAll()
        {
            // The ds object is the data store
            // It has a collection for each entity it manages

            return _mapper.Map<IEnumerable<CustomerBase>>(this.Customers.All());
        }

        // Attention 04 - Get one customer by its identifier
        public CustomerBase CustomerGetById(int id)
        {
            // Attempt to fetch the object
            var o = this.Customers.Get(id);

            // Return the result, or null if not found
            return (o == null) ? null : _mapper.Map<CustomerBase>(o);
        }

        public IEnumerable<EmployeeBase> EmployeeGetAll()
        {
            return _mapper.Map<IEnumerable<EmployeeBase>>(this.Employees.All());
        }

        // ############################################################
        // Employee
        // ############################################################

        public EmployeeBase EmployeeGetById(int id)
        {
            var e = this.Employees.Get(id);

            if (e == null)
            {
                return null;
            }
            else
            {
                return _mapper.Map<EmployeeBase>(e);
            }
        }

        public EmployeeBase EmployeeAdd(EmployeeAdd newItem)
        {
            var addedEmployee = _mapper.Map<Employee>(newItem);

            this.Employees.Add(addedEmployee);
            this.SaveChanges();

            return _mapper.Map<EmployeeBase>(addedEmployee);
        }

        public EmployeeBase EmployeeEditProfileInfo(EmployeeEditProfileInfo editItem)
        {
            var e = this.Employees.Get(editItem.Id);

            if (e == null)
            {
                return null;
            }
            else
            {
                //ds.Entry(e).CurrentValues.SetValues(editItem);
                this.Employees.Update(e);
                this.SaveChanges();

                return _mapper.Map<EmployeeBase>(e);
            }
        }

        public bool EmployeeDelete(int id)
        {
            var e = this.Employees.Get(id);

            if (e == null)
            {
                return false;
            }
            else
            {
                this.Employees.Remove(e);
                this.SaveChanges();

                return true;
            }
        }

        // ############################################################
        // Invoice
        // ############################################################

        public IEnumerable<InvoiceBase> InvoiceGetAll()
        {
            return _mapper.Map<IEnumerable<InvoiceBase>>(this.Invoices.All());
        }

        public InvoiceWithDetail InvoiceGetById(int id)
        {
            var invoice = this.Invoices.Find(
                where =>
                    where.Id == id,
                includes => includes.Customer.Employee,
                includes => includes.InvoiceLines.Select(i => i.Track.Genre),
                includes => includes.InvoiceLines.Select(i => i.Track.MediaType),
                includes => includes.InvoiceLines.Select(i => i.Track.Album),
                includes => includes.InvoiceLines.Select(i => i.Track.Album.Artist)).SingleOrDefault();

            if (invoice == null)
            {
                return null;
            }
            else
            {
                return _mapper.Map<InvoiceWithDetail>(invoice);
            }
        }

        // ############################################################
        // Album
        // ############################################################

        // Attention 03 - Get all albums
        // Notice the return type - it is almost ALWAYS a view model object or collection
        public IEnumerable<AlbumBase> AlbumGetAll()
        {
            // The ds object is the data store
            // It has a collection for each entity it manages

            return _mapper.Map<IEnumerable<AlbumBase>>(this.Albums.All().OrderBy(a => a.Title));
        }

        // Attention 04 - Get one album by its identifier
        public AlbumBase AlbumGetById(int id)
        {
            // Attempt to fetch the object
            var o = this.Albums.Get(id);

            // Return the result, or null if not found
            return (o == null) ? null : _mapper.Map<AlbumBase>(o);
        }

        // ############################################################
        // Artist
        // ############################################################

        public IEnumerable<ArtistBase> ArtistGetAll()
        {

            return _mapper.Map<IEnumerable<ArtistBase>>(this.Artists.All().OrderBy(a => a.Name));
        }

        // ############################################################
        // MediaType
        // ############################################################

        public IEnumerable<MediaTypeBase> MediaTypeGetAll()
        {
            return _mapper.Map<IEnumerable<MediaTypeBase>>(this.MediaTypes.All().OrderBy(mt => mt.Name));
        }

        public MediaTypeBase MediaTypeGetById(int id)
        {
            var e = this.MediaTypes.Get(id);

            if (e == null)
            {
                return null;
            }
            else
            {
                return _mapper.Map<MediaTypeBase>(e);
            }
        }

        // ############################################################
        // Genre
        // ############################################################

        public IEnumerable<GenreBase> GenreGetAll()
        {
            return _mapper.Map<IEnumerable<GenreBase>>(this.Genres.All().OrderBy(mt => mt.Name));
        }

        public GenreBase GenreGetById(int id)
        {
            var e = this.Genres.Get(id);

            return e == null ? null : _mapper.Map<GenreBase>(e);
        }

        // ############################################################
        // Track
        // ############################################################

        public IEnumerable<TrackWithDetail> TrackGetAll()
        {
            var tracks = this.Tracks
                .All(IncludeTrackLevel[IncludeLevel.Comprehensive]);

            return _mapper.Map<IEnumerable<TrackWithDetail>>(tracks);
        }

        public IEnumerable<TrackWithDetail> TrackGetAllSorted()
        {
            var tracks = this.Tracks
                .All(IncludeTrackLevel[IncludeLevel.Comprehensive])
                .OrderBy(o => o.AlbumId).ThenBy(o => o.Name);

            return _mapper.Map<IEnumerable<TrackWithDetail>>(tracks);
        }

        public IEnumerable<TrackWithDetail> TrackGetAllPop()
        {
            // Pop, GenreId = 9
            var tracks = this.Tracks.Find(
                where =>
                    where.Genre.Name == "Pop",
                IncludeTrackLevel[IncludeLevel.Comprehensive])
                .OrderBy(t => t.Name);

            return _mapper.Map<IEnumerable<TrackWithDetail>>(tracks);
        }

        // This legacy version when using Include() will not include associated entities
        public IEnumerable<TrackWithDetail> TrackGetAllPop_Legacy()
        {
            // Pop, GenreId = 9
            var tracks = this.Tracks
                .All(IncludeTrackLevel[IncludeLevel.Comprehensive])
                .Join(this.Genres.All(), t => t.GenreId, g => g.Id,
                (t, g) => new { Track = t, GenreName = g.Name })  // Composite object of Track + Genre
                .Where(t_g => t_g.GenreName == "Pop")
                .OrderBy(t_g => t_g.Track.Name)
                .Select(t_g => t_g.Track);  // Get back 'Track'

            return _mapper.Map<IEnumerable<TrackWithDetail>>(tracks);
        }

        public IEnumerable<TrackWithDetail> TrackGetAllDeepPurple()
        {
            var tracks = this.Tracks.Find(
                where =>
                    where.Composer.Contains("Jon Lord"),
                IncludeTrackLevel[IncludeLevel.Comprehensive])
                .OrderBy(t => t.Id);

            return _mapper.Map<IEnumerable<TrackWithDetail>>(tracks);
        }

        public IEnumerable<TrackWithDetail> TrackGetAllTop100Longest()
        {
            var tracks = this.Tracks
                .All(IncludeTrackLevel[IncludeLevel.Comprehensive])
                .OrderByDescending(t => t.Milliseconds)
                .Take(100);

            return _mapper.Map<IEnumerable<TrackWithDetail>>(tracks);
        }

        public IEnumerable<TrackWithDetail> TrackGetAllWithDetail()
        {
            var tracks = this.Tracks
                .All(IncludeTrackLevel[IncludeLevel.Comprehensive]);

            return _mapper.Map<IEnumerable<TrackWithDetail>>(tracks);
        }

        public TrackBase TrackGetById(int id)
        {
            var e = this.Tracks.Get(id);

            if (e == null)
            {
                return null;
            }
            else
            {
                return _mapper.Map<TrackBase>(e);
            }
        }

        public TrackWithDetail TrackGetByIdWithDetail(int id)
        {
            var track = this.Tracks
                .All(IncludeTrackLevel[IncludeLevel.Comprehensive])
                .SingleOrDefault(t => t.Id == id);

            return _mapper.Map<TrackWithDetail>(track);
        }

        public TrackWithDetail TrackAdd(TrackAdd newTrack)
        {
            // Locate and validate album, genre, and media type
            var a = this.Albums.Get(newTrack.AlbumId);
            var g = this.Genres.Get(newTrack.GenreId);
            var mt = this.MediaTypes.Get(newTrack.MediaTypeId);

            if (a == null || g == null || mt == null)
            {
                return null;
            }
            else
            {
                // FIXME: IRepository.Add() doesn't return the added entity
                var addedTrack = _mapper.Map<Track>(newTrack);
                addedTrack.Album = a;
                addedTrack.Genre = g;
                addedTrack.MediaType = mt;

                this.Tracks.Add(addedTrack);
                this._Db.SaveChanges();

                return _mapper.Map<TrackWithDetail>(addedTrack);
            }
        }

        // ############################################################
        // Employee
        // ############################################################

        public IEnumerable<PlaylistBase> PlaylistGetAll()
        {
            return _mapper.Map<IEnumerable<PlaylistBase>>(
                this.Playlists.All().OrderBy(p => p.Name)
            );
        }

        public IEnumerable<PlaylistBase> PlaylistGetAllWithTracks()
        {
            var playlists = this.Playlists
                .All(includes => includes.Tracks)
                .OrderBy(p => p.Name);

            return _mapper.Map<IEnumerable<PlaylistBase>>(playlists);
        }

        public PlaylistWithTracks PlaylistGetByIdWithTracks(int id)
        {
            var playlist = this.Playlists.Find(
                where => where.Id == id,
                includes => includes.Tracks).SingleOrDefault();

            return playlist == null ? null : _mapper.Map<PlaylistWithTracks>(playlist);
        }

        //public EmployeeBase PlaylistAdd(EmployeeAdd newItem)
        //{
        //    var addedEmployee = _mapper.Map<Employee>(newItem);

        //    this.Employees.Add(addedEmployee);
        //    this.SaveChanges();

        //    return _mapper.Map<EmployeeBase>(addedEmployee);
        //}

        // Attention 11 - Edit an employee's job duties
        public PlaylistWithTracks PlaylistEditTracks(PlaylistEditTracks newPlaylist)
        {
            // Attempt to fetch the object

            // When editing an object with a to-many collection,
            // and you wish to edit the collection,
            // MUST fetch its associated collection
            var playlist = this.Playlists.Get(newPlaylist.Id);

            if (playlist == null)
            {
                // Problem - object was not found, so return
                return null;
            }
            else
            {
                // Update the object with the incoming values

                // First, clear out the existing collection
                playlist.Tracks.Clear();

                // Then, go through the incoming items
                // For each one, add to the fetched object's collection
                foreach (var id in newPlaylist.TrackIds)
                {
                    var track = this.Tracks.Get(id);
                    playlist.Tracks.Add(track);
                }

                // Save changes
                this._Db.SaveChanges();

                return _mapper.Map<PlaylistWithTracks>(o);
            }
        }

        //public bool EmployeeDelete(int id)
        //{
        //    var e = this.Employees.Get(id);

        //    if (e == null)
        //    {
        //        return false;
        //    }
        //    else
        //    {
        //        this.Employees.Remove(e);
        //        this.SaveChanges();

        //        return true;
        //    }
        //}
    }
}