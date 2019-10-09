using Lamazon.DataAccess.Interfaces;
using Lamazon.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lamazon.DataAccess.Repositories
{
    public class UserRespository : BaseRepository<LamazonDbContext>, IUserRepository<User>
    {
        public UserRespository(LamazonDbContext context) : base(context) { }

        public IEnumerable<User> GetAll()
        {
            return _db.Users
                .ToList();
        }

        public User GetById(string id)
        {
            return _db.Users
                .FirstOrDefault(u => u.Id == id);
        }

        public User GetByUsername(string username)
        {
            return _db.Users
                .FirstOrDefault(u => u.UserName == username);
        }

        public int Insert(User entity)
        {
            _db.Users.Add(entity);
            return _db.SaveChanges();
        }

        public int Update(User entity)
        {
            User user = _db.Users
                .FirstOrDefault(u => u.Id == entity.Id);
            if (user == null)
                return -1;

            user.NormalizedEmail = entity.Email.ToUpper();
            user.Email = entity.Email;
            user.NormalizedUserName = entity.UserName.ToUpper();
            user.UserName = entity.UserName;

            return _db.SaveChanges();
        }

        public int Delete(string id)
        {
            User user = _db.Users.FirstOrDefault(u => u.Id == id);
            if (user == null)
                return -1;

            _db.Users.Remove(user);
            return _db.SaveChanges();
        }
    }
}
