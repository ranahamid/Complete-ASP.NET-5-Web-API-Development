﻿using System;
using System.Threading.Tasks;
using WebApplication5.Data;

namespace WebApplication5.IRepository
{
    public interface IUnitofWork:IDisposable 
    {
        IGenericRepository<Country> Countries { get; }
        IGenericRepository<Hotel> Hotels { get;   }
        Task Save();
    }
}
