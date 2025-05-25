using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using IronCore.Domain.Entities.User;
using IronCore.Domain.Entities.User.Login;
using IronCore.BusinessLogic.DBModel;
using IronCore.Domain.Enums.User;
using System.Security.Cryptography;
using System.Text;
using IronCore.Domain.Entities.User.Registration;
using System.ComponentModel.DataAnnotations;
using System.Web;
using IronCore.Helpers;
using IronCore.Helpers.LoginRegisterHelper;
using AutoMapper;

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
        public UserLoginResult LoginAPI(UserLoginData data)
        {
            try
            {
                using (var db = new UserContext())
                {
                    UserDbModel user = db.Users
                                 .FirstOrDefault(u =>
                                     u.UserName == data.UserName);

                    if (user == null)
                        return new UserLoginResult
                        {
                            Status = false,
                            Message = "UserNotFound"
                        };

                    var encPassword = LoginRegisterHelper.HashGen(data.Password);
                    if (user.Password != encPassword)
                        return new UserLoginResult
                        {
                            Status = false,
                            Message = "PasswordIsWrong"
                        };

                    user.LastLogin = data.LastLogin; 
                    db.Entry(user).Property(u => u.LastLogin).IsModified = true;
                    db.SaveChanges();

                    return new UserLoginResult
                    {
                        Status = true,
                        Message = "UserExist",
                        UserDTO = new UserDTO
                        {
                            Id = user.Id,
                            UserName = user.UserName,
                            Password = user.Password,
                            Email = user.Email,
                            LastLogin = user.LastLogin,
                            Role = user.Level.ToString(),
                            FirstName = user.FirstName,
                            LastName = user.LastName,
                            BirthDate = user.BirthDate,
                            PhoneNumber = user.PhoneNumber,
                            RegistrationDate = user.RegistrationDate,
                            Balance = user.Balance
                        }
                    };
                }
            }
            catch (Exception ex)
            {
                return new UserLoginResult
                {
                    Status = false,
                    Message = ex.Message
                };
            }
        }
        public UserRegistrationResult RegisterAPI(UserRegistrationData data)
        {
            try
            {
                using (var db = new UserContext())
                {
                    if (db.Users.Any(u => u.UserName == data.UserName))
                        return new UserRegistrationResult
                        {
                            Status = false,
                            Message = "SuchCredentialsIsExist"
                        };

                    var encPassword = LoginRegisterHelper.HashGen(data.Password);
                    var user = new UserDbModel
                    {
                        UserName = data.UserName,
                        Password = encPassword,
                        Email = data.Email,
                        LastLogin = DateTime.Now,
                        Level = URole.User,
                        FirstName = data.FirstName,
                        LastName = data.LastName,
                        BirthDate = data.BirthDate,
                        PhoneNumber = data.PhoneNumber,
                        RegistrationDate = DateTime.UtcNow,
                        Balance = 0
                    };

                    db.Users.Add(user);
                    db.SaveChanges();

                    return new UserRegistrationResult
                    {
                        Status = true,
                        Message = "Successful",
                        User = user
                    };
                }
            }
            catch (Exception ex)
            {
                return new UserRegistrationResult
                {
                    Status = false,
                    Message = ex.Message,
                    User = null
                };
            }
        }
        protected void SaveLastLoginInternal(string userName, DateTime whenUtc, string ip)
        {
            var db = new UserContext();
            var u = db.Users.FirstOrDefault(x => x.UserName == userName);
            if (u == null) return;

            u.LastLogin = whenUtc;
            db.Entry(u).Property(x => x.LastLogin).IsModified = true;
            db.SaveChanges();
        }

    }
}
