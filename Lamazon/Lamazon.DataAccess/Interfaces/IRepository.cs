using System;
using System.Collections.Generic;
using System.Text;

namespace Lamazon.DataAccess.Interfaces
{
    public interface IRepository<TEntity>
        where TEntity : class
    {
        IEnumerable<TEntity> GetAll();
        TEntity GetById(int id);
        int Insert(TEntity entity);
        int Update(TEntity entity);
        int Delete(int id);
    }
}
