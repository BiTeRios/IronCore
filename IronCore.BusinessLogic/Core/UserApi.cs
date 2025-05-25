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
using IronCore.Domain.Entities.Session;
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
                    var user = db.Users
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

                    user.LastLogin = DateTime.UtcNow;
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
                            Level = user.Level,
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

        internal HttpCookie Cookie(string loginCredential)
        {
            var apiCookie = new HttpCookie("X-KEY")
            {
                Value = CookieGenerator.Create(loginCredential)
            };

            using (var db = new SessionContext())
            {
                Session curent;
                var validate = new EmailAddressAttribute();
                if (validate.IsValid(loginCredential))
                {
                    curent = (from e in db.Sessions where e.Username == loginCredential select e).FirstOrDefault();
                }
                else
                {
                    curent = (from e in db.Sessions where e.Username == loginCredential select e).FirstOrDefault();
                }

                if (curent != null)
                {
                    curent.CookieString = apiCookie.Value;
                    curent.ExpireTime = DateTime.Now.AddMinutes(60);
                    using (var todo = new SessionContext())
                    {
                        todo.Entry(curent).State = EntityState.Modified;
                        todo.SaveChanges();
                    }
                }
                else
                {
                    db.Sessions.Add(new Session
                    {
                        Username = loginCredential,
                        CookieString = apiCookie.Value,
                        ExpireTime = DateTime.Now.AddMinutes(60)
                    });
                    db.SaveChanges();
                }
            }

            return apiCookie;
        }
        internal UserDTO UserCookie(string cookie)
        {
            Session session;
            UserDbModel curentUser;

            using (var db = new SessionContext())
            {
                session = db.Sessions.FirstOrDefault(s => s.CookieString == cookie && s.ExpireTime > DateTime.Now);
            }

            if (session == null) return null;
            using (var db = new UserContext())
            {
                var validate = new EmailAddressAttribute();
                if (validate.IsValid(session.Username))
                {
                    curentUser = db.Users.FirstOrDefault(u => u.Email == session.Username);
                }
                else
                {
                    curentUser = db.Users.FirstOrDefault(u => u.UserName == session.Username);
                }
            }

            if (curentUser == null) return null;
            Mapper.Initialize(cfg => cfg.CreateMap<UserDbModel, UserDTO>());
            var userminimal = Mapper.Map<UserDTO>(curentUser);

            return userminimal;
        }
    }
}
