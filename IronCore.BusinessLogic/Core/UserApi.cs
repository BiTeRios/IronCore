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
                                     u.Credential == data.Credential ||
                                     u.Email == data.Credential);

                    if (user == null)
                        return new UserLoginResult
                        {
                            Status = false,
                            Message = "UserNotFound"
                        };

                    var passwordHash = HashPassword(data.Password);
                    if (user.Password != passwordHash)
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
                            Credential = user.Credential,
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
                    if (db.Users.Any(u => u.Credential == data.Credential))
                        return new UserRegistrationResult
                        {
                            Status = false,
                            Message = "SuchCredentialsIsExist"
                        };

                    var user = new UserDbModel
                    {
                        Credential = data.Credential,
                        UserName = data.UserName,
                        Password = HashPassword(data.Password),
                        Email = data.Email,
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

        private static string HashPassword(string password)
        {
            using (var sha = SHA256.Create())
            {
                var bytes = Encoding.UTF8.GetBytes(password);
                var hash = sha.ComputeHash(bytes);
                return Convert.ToBase64String(hash);
            }
        }
    }
}
