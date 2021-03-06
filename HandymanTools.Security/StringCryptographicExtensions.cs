﻿using System;
using System.Security.Cryptography;

namespace HandymanTools.Security
{
    public static class StringCryptographicExtensions
    {
        public static string ToSHA256(this String str, string salt)
        {
            var byteConverter = new StringByteConverter();
            var hasher = new SHA256CryptoServiceProvider();
            var finalString = str + salt;

            byte[] finalStringData = byteConverter.ToBytes(finalString);
            var result = hasher.ComputeHash(finalStringData);
            return byteConverter.BytesToHex(result);
        }
    }
}
