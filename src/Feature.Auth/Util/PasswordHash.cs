﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Feature.Auth.Util
{
    public class PasswordHash
    {
        private static readonly RandomNumberGenerator rng = RandomNumberGenerator.Create();
        public static readonly int DefaultSaltSize = 8; // 64-bit salt
        public readonly byte[] Salt;
        public readonly byte[] Passhash;

        internal PasswordHash(byte[] salt, byte[] passhash)
        {
            Salt = salt;
            Passhash = passhash;
        }

        public override String ToString()
        {
            return String.Format("{{'salt': '{0}', 'passhash': '{1}'}}",
                                 Convert.ToBase64String(Salt),
                                 Convert.ToBase64String(Passhash));
        }

        public static PasswordHash Encode<HA>(String password) where HA : HashAlgorithm
        {
            return Encode<HA>(password, DefaultSaltSize);
        }

        public static PasswordHash Encode<HA>(String password, int saltSize) where HA : HashAlgorithm
        {
            return Encode<HA>(password, GenerateSalt(saltSize));
        }

        private static PasswordHash Encode<HA>(string password, byte[] salt) where HA : HashAlgorithm
        {
            byte[] hashedBytes;

            using (var sha512 = SHA512.Create())
            {
                using (MemoryStream hashInput = new MemoryStream())
                {
                    hashInput.Write(salt, 0, salt.Length);
                    byte[] passwordBytes = Encoding.UTF8.GetBytes(password);
                    hashInput.Write(passwordBytes, 0, passwordBytes.Length);
                    hashInput.Seek(0, SeekOrigin.Begin);

                    hashedBytes = sha512.ComputeHash(hashInput);

                    //returnBitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
                }
            }

            return new PasswordHash(salt, hashedBytes);
        }

        private static byte[] GenerateSalt(int saltSize)
        {
            // This generates salt.
            // Rephrasing Schneier:
            // "salt" is a random string of bytes that is
            // combined with password bytes before being
            // operated by the one-way function.
            byte[] salt = new byte[saltSize];
            rng.GetBytes(salt);
            return salt;
        }

        public static bool Verify<HA>(string password, byte[] salt, byte[] passhash) where HA : HashAlgorithm
        {
            //TODOL Ver como comparar bytes[]...
            return Encode<HA>(password, salt).ToString() == new PasswordHash(salt, passhash).ToString();
        }
    }

}
