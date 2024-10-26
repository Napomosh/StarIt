﻿using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace StarIt.Bl;

public class Encrypt : IEncrypt
{
    public string HashPassword(string password, string salt)
    {
        return Convert.ToBase64String(KeyDerivation.Pbkdf2(password, System.Text.Encoding.ASCII.GetBytes(salt)
                                , KeyDerivationPrf.HMACSHA512, 5000, 64));
    }
}