using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using IronCore.Domain.Entities.User;
using IronCore.BusinessLogic.DBModel;

namespace IronCore.BusinessLogic.Core
{
    public class UserApi
    {
        public UserApi() { }

        public UserDbModel GetByIdAPI(int id)
        {
            try
            {
                using (var db = new UserContext())
                {
                    return db.Users.Find(id);
                }
            }
            catch
            {
                return null;
            }
        }
        public bool UpdateAPI(UserDbModel entity)
        {
            try
            {
                using (var db = new UserContext())
                {
                    db.Entry(entity).State = EntityState.Modified;
                    db.SaveChanges();
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }

        public bool DeleteAPI(int id)
        {
            try
            {
                using (var db = new UserContext())
                {
                    var user = db.Users.Find(id);
                    if (user == null) return false;
                    db.Users.Remove(user);
                    db.SaveChanges();
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }

        public bool ExistsAPI(int id)
        {
            using (var db = new UserContext())
            {
                return db.Users.Any(u => u.Id == id);
            }
        }
        public UserDbModel Login(string credential, string password)
        {
            using (var db = new UserContext())
            {
                return db.Users.FirstOrDefault(u =>
                    (u.Credential == credential || u.UserName == credential) &&
                    u.Password == password);
            }
        }
    }
}
