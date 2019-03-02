using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
// new...
using AutoMapper;
using Assignment5.Data;
using Assignment5.Data.Repositories;
using Assignment5.Models;

namespace Assignment5.Data.Managers
{
    public interface IChinookDataManager : IUnitOfWork
    {
        IAlbumRepository Albums { get; }
        IArtistRepository Artists { get; }
        ICustomerRepository Customers { get; }
        IEmployeeRepository Employees { get; }
        IGenreRepository Genres { get; }
        IInvoiceLineRepository InvoiceLines { get; }
        IInvoiceRepository Invoices { get; }
        IMediaTypeRepository MediaTypes { get; }
        IPlaylistRepository Playlists { get; }
        ITrackRepository Tracks { get; }

        void RefreshEntityState();
    }
}