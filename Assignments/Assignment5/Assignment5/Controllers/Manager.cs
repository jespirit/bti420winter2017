using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
// new...
using AutoMapper;
using Assignment5.Models;

namespace Assignment5.Controllers
{
    public class Manager
    {
        // Reference to the data context
        private DataContext ds = new DataContext();

        // AutoMapper components
        MapperConfiguration config;
        public IMapper mapper;

        public Manager()
        {
            // If necessary, add more constructor code here...

            // Configure the AutoMapper components
            config = new MapperConfiguration(cfg =>
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

                cfg.CreateMap<Customer, CustomerWithInfo>();

                cfg.CreateMap<Genre, GenreBase>();

                cfg.CreateMap<Invoice, InvoiceBase>();
                cfg.CreateMap<Invoice, InvoiceWithInfo>();

                cfg.CreateMap<InvoiceLine, InvoiceLineBase>();
                cfg.CreateMap<InvoiceLine, InvoiceLineWithInfo>();

                cfg.CreateMap<MediaType, MediaTypeBase>();

                cfg.CreateMap<Track, TrackBase>();
                cfg.CreateMap<Track, TrackWithDetail>();

                cfg.CreateMap<Track, TrackAddForm>();
                cfg.CreateMap<TrackAdd, Track>();
            });

            mapper = config.CreateMapper();

            // Turn off the Entity Framework (EF) proxy creation features
            // We do NOT want the EF to track changes - we'll do that ourselves
            ds.Configuration.ProxyCreationEnabled = false;

            // Also, turn off lazy loading...
            // We want to retain control over fetching related objects
            ds.Configuration.LazyLoadingEnabled = false;
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

            return mapper.Map<IEnumerable<CustomerBase>>(ds.Customers);
        }

        // Attention 04 - Get one customer by its identifier
        public CustomerBase CustomerGetById(int id)
        {
            // Attempt to fetch the object
            var o = ds.Customers.Find(id);

            // Return the result, or null if not found
            return (o == null) ? null : mapper.Map<CustomerBase>(o);
        }

        public IEnumerable<EmployeeBase> EmployeeGetAll()
        {
            return mapper.Map<IEnumerable<EmployeeBase>>(ds.Employees);
        }

        // ############################################################
        // Employee
        // ############################################################

        public EmployeeBase EmployeeGetById(int id)
        {
            var e = ds.Employees.Find(id);

            if (e == null)
            {
                return null;
            }
            else
            {
                return mapper.Map<EmployeeBase>(e);
            }
        }

        public EmployeeBase EmployeeAdd(EmployeeAdd newItem)
        {
            var addedItem = ds.Employees.Add(mapper.Map<Employee>(newItem));

            if (addedItem == null)
            {
                return null;
            }
            else
            {
                // Display "details"
                return mapper.Map<EmployeeBase>(addedItem);
            }
        }

        public EmployeeBase EmployeeEditProfileInfo(EmployeeEditProfileInfo editItem)
        {
            var e = ds.Employees.Find(editItem.EmployeeId);

            if (e == null)
            {
                return null;
            }
            else
            {
                ds.Entry(e).CurrentValues.SetValues(editItem);
                ds.SaveChanges();

                return mapper.Map<EmployeeBase>(e);
            }
        }

        public bool EmployeeDelete(int id)
        {
            var e = ds.Employees.Find(id);

            if (e == null)
            {
                return false;
            }
            else
            {
                ds.Employees.Remove(e);
                ds.SaveChanges();

                return true;
            }
        }

        // ############################################################
        // Invoice
        // ############################################################

        public IEnumerable<InvoiceBase> InvoiceGetAll()
        {
            return mapper.Map<IEnumerable<InvoiceBase>>(ds.Invoices);
        }

        public InvoiceWithInfo InvoiceGetById(int id)
        {
            var invoice = ds.Invoices
                //.Include("Customer")
                .Include("Customer.Employee")
                //.Include("InvoiceLines.Track")
                .Include("InvoiceLines.Track.Genre")
                .Include("InvoiceLines.Track.MediaType")
                .Include("InvoiceLines.Track.Album.Artist")
                .SingleOrDefault(i => i.InvoiceId == id);

            if (invoice == null)
            {
                return null;
            }
            else
            {
                return mapper.Map<InvoiceWithInfo>(invoice);
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

            return mapper.Map<IEnumerable<AlbumBase>>(ds.Albums.OrderBy(a => a.Title));
        }

        // Attention 04 - Get one album by its identifier
        public AlbumBase AlbumGetById(int id)
        {
            // Attempt to fetch the object
            var o = ds.Albums.Find(id);

            // Return the result, or null if not found
            return (o == null) ? null : mapper.Map<AlbumBase>(o);
        }

        // ############################################################
        // Artist
        // ############################################################

        public IEnumerable<ArtistBase> ArtistGetAll()
        {

            return mapper.Map<IEnumerable<ArtistBase>>(ds.Artists.OrderBy(a => a.Name));
        }

        // ############################################################
        // MediaType
        // ############################################################

        public IEnumerable<MediaTypeBase> MediaTypeGetAll()
        {
            return mapper.Map<IEnumerable<MediaTypeBase>>(ds.MediaTypes.OrderBy(mt => mt.Name));
        }

        public MediaTypeBase MediaTypeGetById(int id)
        {
            var e = ds.MediaTypes.Find(id);

            if (e == null)
            {
                return null;
            }
            else
            {
                return mapper.Map<MediaTypeBase>(e);
            }
        }

        // ############################################################
        // Genre
        // ############################################################

        public IEnumerable<GenreBase> GenreGetAll()
        {
            return mapper.Map<IEnumerable<GenreBase>>(ds.Genres.OrderBy(mt => mt.Name));
        }

        public GenreBase GenreGetById(int id)
        {
            var e = ds.Genres.Find(id);

            return e == null ? null : mapper.Map<GenreBase>(e);
        }

        // ############################################################
        // Track
        // ############################################################

        public IEnumerable<TrackWithDetail> TrackGetAll()
        {
            var tracks = ds.Tracks
                .Include("Album.Artist")
                .Include("MediaType")
                .Include("Genre");

            return mapper.Map<IEnumerable<TrackWithDetail>>(tracks);
        }

        public IEnumerable<TrackWithDetail> TrackGetAllSorted()
        {
            var tracks = ds.Tracks
                .Include("Album.Artist")
                .Include("MediaType")
                .Include("Genre")
                .OrderBy(o => o.AlbumId).ThenBy(o => o.Name);

            return mapper.Map<IEnumerable<TrackWithDetail>>(tracks);
        }

        public IEnumerable<TrackWithDetail> TrackGetAllPop()
        {
            // Pop, GenreId = 9
            var tracks = ds.Tracks
                .Include("Album.Artist")
                .Include("MediaType")
                .Include("Genre")
                .Where(t => t.Genre.Name == "Pop")
                .OrderBy(t => t.Name);

            return mapper.Map<IEnumerable<TrackWithDetail>>(tracks);
        }

        // This legacy version when using Include() will not include associated entities
        public IEnumerable<TrackWithDetail> TrackGetAllPop_Legacy()
        {
            // Pop, GenreId = 9
            var tracks = ds.Tracks
                .Include("Album.Artist")
                .Include("MediaType")
                .Include("Genre")
                .Join(ds.Genres, t => t.GenreId, g => g.GenreId,
                (t, g) => new { Track = t, GenreName = g.Name })  // Composite object of Track + Genre
                .Where(t_g => t_g.GenreName == "Pop")
                .OrderBy(t_g => t_g.Track.Name)
                .Select(t_g => t_g.Track);  // Get back 'Track'

            return mapper.Map<IEnumerable<TrackWithDetail>>(tracks);
        }

        public IEnumerable<TrackWithDetail> TrackGetAllDeepPurple()
        {
            var tracks = ds.Tracks
                .Include("Album.Artist")
                .Include("MediaType")
                .Include("Genre")
                // Same as t.Composer LIKE '%Jon Lord%'
                .Where(t => t.Composer.Contains("Jon Lord"))
                .OrderBy(t => t.TrackId);

            return mapper.Map<IEnumerable<TrackWithDetail>>(tracks);
        }

        public IEnumerable<TrackWithDetail> TrackGetAllTop100Longest()
        {
            var tracks = ds.Tracks
                .Include("Album.Artist")
                .Include("MediaType")
                .Include("Genre")
                .OrderByDescending(t => t.Milliseconds)
                .Take(100);

            return mapper.Map<IEnumerable<TrackWithDetail>>(tracks);
        }

        public IEnumerable<TrackWithDetail> TrackGetAllWithDetail()
        {
            var tracks = ds.Tracks
                .Include("Album.Artist")
                .Include("MediaType")
                .Include("Genre");

            return mapper.Map<IEnumerable<TrackWithDetail>>(tracks);
        }

        public TrackBase TrackGetById(int id)
        {
            var e = ds.Tracks.Find(id);

            if (e == null)
            {
                return null;
            }
            else
            {
                return mapper.Map<TrackBase>(e);
            }
        }

        public TrackWithDetail TrackGetByIdWithDetail(int id)
        {
            var track = ds.Tracks
                .Include("Album.Artist")
                .Include("MediaType")
                .Include("Genre")
                .SingleOrDefault(t => t.TrackId == id);

            return mapper.Map<TrackWithDetail>(track);
        }

        public TrackWithDetail TrackAdd(TrackAdd newTrack)
        {
            // Locate and validate album, genre, and media type
            var a = ds.Albums.Find(newTrack.AlbumId);
            var g = ds.Genres.Find(newTrack.GenreId);
            var mt = ds.MediaTypes.Find(newTrack.MediaTypeId);

            if (a == null || g == null || mt == null)
            {
                return null;
            }
            else
            {
                var addedTrack = ds.Tracks.Add(mapper.Map<Track>(newTrack));
                addedTrack.Album = a;
                addedTrack.Genre = g;
                addedTrack.MediaType = mt;

                ds.SaveChanges();

                return addedTrack == null ? null : mapper.Map<TrackWithDetail>(addedTrack);
            }
        }
    }
}