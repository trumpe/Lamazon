using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lamazon.DataAccess.Interfaces
{
    public interface IUserRepository<TUser>
        where TUser : IdentityUser
    {
        IEnumerable<TUser> GetAll();
        TUser GetById(string id);
        TUser GetByUsername(string username);
        int Insert(TUser entity);
        int Update(TUser entity);
        int Delete(string id);
    }
}
