﻿using IronCore.BusinessLogic.Core;
using IronCore.BusinessLogic.DBModel;
using IronCore.BusinessLogic.Interfaces;
using IronCore.Domain.Entities.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IronCore.BusinessLogic.BL
{
    public class UserBL
    {
        private readonly UserContext _context;

        public UserBL()
        {
            _context = new UserContext();
        }

        public ULoginData Login(string email, string password)
        {
            return _context.Users
                           .FirstOrDefault(u => u.Credential == email
                                             && u.PasswordHash == password);
        }

        public bool AddUser(UserDbModel user)
        {
            throw new NotImplementedException();
        }

        public void DeactivateUser(int userId)
        {
            throw new NotImplementedException();
        }

        public bool DeleteUser(int userId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<UserDbModel> GetAllUsers()
        {
            throw new NotImplementedException();
        }

        public UserDbModel GetUserById(int userId)
        {
            throw new NotImplementedException();
        }

        public bool UpdateUser(UserDbModel user)
        {
            throw new NotImplementedException();
        }

        public bool UserExists(int userId)
        {
            throw new NotImplementedException();
        }

        public bool ValidateCredentials(string username, string password)
        {
            throw new NotImplementedException();
        }
    }
}
