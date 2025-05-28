using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using IronCore.BusinessLogic.Core;
using IronCore.BusinessLogic.Interfaces;
using IronCore.Domain.Entities.Contact;
using IronCore.Domain.Entities.User;
using IronCore.Domain.Enums.User;
using IronCore.Helpers.LoginRegisterHelper;

namespace IronCore.BusinessLogic.BL
{
    public class UserBL : UserApi, IUser
    {
        public List<UserDTO> GetAllUsers()
        {
            return GetAllUsersAPI().Select(MapToUser).ToList();
        }
        public UserDTO GetById(int id)
        {
            var userId = GetByIdAPI(id);
            return userId != null ? MapToUser(userId) : null;
        }

        public bool Update(UserDTO user)
        {
            var dbUser = GetByIdAPI(user.Id);
            if (dbUser == null) return false;
            var mappedUser = MapToDb(user);
            var result = UpdateAPI(mappedUser);
            return true;
        }
        public bool Delete(int id)
        {
            var dbUser = GetByIdAPI(id);
            if (dbUser == null) return false;
            DeleteAPI(id);
            return true;
        }
        public bool ChangePassword(int userId, string oldPassword, string newPassword, out string message)
        {
            var user = GetById(userId);
            if (user == null)
            {
                message = "Пользователь не найден";
                return false;
            }
            var oldPasswordHash = LoginRegisterHelper.HashGen(oldPassword);
            if (user.Password != oldPasswordHash)
            {
                message = "Старый пароль неверен";
                return false;
            }
            user.Password = LoginRegisterHelper.HashGen(newPassword);
            Update(user);
            message = "Пароль успешно изменён!";
            return true;
        }

        private UserDTO MapToUser(UserDbModel db)
        {
            return new UserDTO
            {
                Id = db.Id,
                UserName = db.UserName,
                Password = db.Password,
                Email = db.Email,
                LastLogin = db.LastLogin,
                Role = db.Level.ToString(),
                FirstName = db.FirstName,
                LastName = db.LastName,
                BirthDate = db.BirthDate,
                PhoneNumber = db.PhoneNumber,
                RegistrationDate = db.RegistrationDate,
                Balance = db.Balance
            };
        }
        private UserDbModel MapToDb(UserDTO user)
        {
            return new UserDbModel
            {
                Id = user.Id,
                UserName = user.UserName,
                Password = user.Password,
                Email = user.Email,
                LastLogin = user.LastLogin,
                Level = (URole)Enum.Parse(typeof(URole), user.Role),
                FirstName = user.FirstName,
                LastName = user.LastName,
                BirthDate = user.BirthDate,
                PhoneNumber = user.PhoneNumber,
                RegistrationDate = user.RegistrationDate,
                Balance = user.Balance
            };
        }
    }
}