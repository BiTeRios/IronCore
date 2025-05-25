using System;
using System.Security.Cryptography;
using System.Text;

namespace IronCore.Helpers.LoginRegisterHelper
{
    public class LoginRegisterHelper
    {
        public static string HashGen(string password)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            var originalBytes = Encoding.Default.GetBytes(password + "IronCore");
            var encodedByte = md5.ComputeHash(originalBytes);
            return BitConverter.ToString(encodedByte).Replace("-", "").ToLower();
        }
    }
}