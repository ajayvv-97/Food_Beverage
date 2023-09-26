using System;
using System.Collections.Generic;
using System.Text;

namespace BLL
{
    public class BllEncryption
    {
        public Boolean encrptPassword(string password, out byte[] passwordSalt, out byte[] passwordHash)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));

            }
            return true;
        }

        public Boolean verifyPassword(string password, byte[] passwordSalt, byte[] passwordHash)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
            {
                var encryptPasswordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < encryptPasswordHash.Length; i++)
                {
                    if (encryptPasswordHash[i] != passwordHash[i])
                        return false;
                }
            }
            return true;
        }
    }
}
