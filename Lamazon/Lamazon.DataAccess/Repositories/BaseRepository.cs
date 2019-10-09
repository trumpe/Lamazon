using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lamazon.DataAccess.Repositories
{
    public abstract class BaseRepository<TDbContext>
        where TDbContext : DbContext
    {
        protected readonly TDbContext _db;
        public BaseRepository(TDbContext context)
        {
            _db = context;
        }
    }
}
