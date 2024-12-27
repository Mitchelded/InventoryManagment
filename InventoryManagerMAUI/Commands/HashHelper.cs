﻿using System.Security.Cryptography;
using System.Text;

namespace InventoryManagerMAUI.Commands;

public static class HashHelper
{
    public static string ComputeMD5Hash(string input)
    {
        using var md5 = MD5.Create();
        byte[] inputBytes = Encoding.UTF8.GetBytes(input);
        byte[] hashBytes = md5.ComputeHash(inputBytes);
        return BitConverter.ToString(hashBytes).Replace("-", "").ToLowerInvariant();
    }
}