using IronCore.BusinessLogic.Core;
using IronCore.BusinessLogic.DBModel;
using IronCore.BusinessLogic.Interfaces;
using IronCore.Domain.Entities.User;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IronCore.BusinessLogic.BL
{
    public class UserBL : UserApi
    {
        private readonly UserContext ctx;

        public UserBL()
        {
            ctx = new UserContext();
        }
        public IEnumerable<UserDbModel> GetAllUsers() => ctx.Users.ToList();

        public UserDbModel GetById(int id) => ctx.Users.Find(id);

        public void Update(UserDbModel incoming)
        {
            var existing = ctx.Users.Find(incoming.Id);          
            if (existing == null) return;

            ctx.Entry(existing).CurrentValues.SetValues(incoming);
            ctx.SaveChanges();
        }

        public void Delete(int id)
        {
            var u = ctx.Users.Find(id);
            if (u != null)
            {
                ctx.Users.Remove(u);
                ctx.SaveChanges();
            }
        }
        public bool UserExists(int id) =>
            ctx.Users.Any(u => u.Id == id);

        public bool ValidateCredentials(string credential, string password)
        {
            return ctx.Users.Any(u =>
                   (u.Credential == credential || u.UserName == credential)
                   && u.Password == password);
        }
    }
}
