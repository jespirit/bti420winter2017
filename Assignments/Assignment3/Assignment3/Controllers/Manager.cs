using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
// new...
using AutoMapper;
using Assignment3.Models;

namespace Assignment3.Controllers
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

                cfg.CreateMap<Track, TrackBase>();
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

        public IEnumerable<EmployeeBase> EmployeeGetAll()
        {
            return mapper.Map<IEnumerable<EmployeeBase>>(ds.Employees);
        }

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

        public IEnumerable<TrackBase> TrackGetAll()
        {
            return mapper.Map<IEnumerable<TrackBase>>(ds.Tracks);
        }

        public IEnumerable<TrackBase> TrackGetAllSorted()
        {
            var tracks = ds.Tracks
                .OrderBy(o => o.AlbumId).ThenBy(o => o.Name);

            return mapper.Map<IEnumerable<TrackBase>>(tracks);
        }

        public IEnumerable<TrackBase> TrackGetAllPop()
        {
            // Pop, GenreId = 9
            var tracks = ds.Tracks
                .Join(ds.Genres, t => t.GenreId, g => g.GenreId,
                (t, g) => new { Track = t, GenreName = g.Name })  // Composite object of Track + Genre
                .Where(t_g => t_g.GenreName == "Pop")
                .OrderBy(t_g => t_g.Track.Name)
                .Select(t_g => t_g.Track);  // Get back 'Track'

            return mapper.Map<IEnumerable<TrackBase>>(tracks);
        }

        public IEnumerable<TrackBase> TrackGetAllDeepPurple()
        {
            var tracks = ds.Tracks
                .Where(t => t.Composer == "Jon Lord")
                .OrderBy(t => t.TrackId);

            return mapper.Map<IEnumerable<TrackBase>>(tracks);
        }

        public IEnumerable<TrackBase> TrackGetAllTop100Longest()
        {
            var tracks = ds.Tracks
                .OrderByDescending(t => t.Milliseconds)
                .Take(100);

            return mapper.Map<IEnumerable<TrackBase>>(tracks);
        }
    }
}