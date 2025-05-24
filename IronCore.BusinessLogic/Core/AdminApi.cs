using IronCore.BusinessLogic.DBModel;
using IronCore.Domain.Entities.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IronCore.BusinessLogic.Core
{
    class AdminApi
    {
        public AdminApi() { }
        public List<UserDbModel> GetAllAPI()
        {
            try
            {
                using (var db = new UserContext())
                {
                    return db.Users.ToList();
                }
            }
            catch
            {
                return new List<UserDbModel>();
            }
        }
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

    }
}
