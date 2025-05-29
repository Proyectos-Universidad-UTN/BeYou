using System.Security.Cryptography;
using System.Text;

namespace BeYou.Utils;

public static class Hashing
{
    /// <summary>
    /// Create a hash text 
    /// </summary>
    /// <param name="input">Text to be hashed</param>
    /// <returns>Hashed text string</returns>
    public static string HashMd5(string input)
    {
        using var md5Hash = MD5.Create();

        var byteArrayResult = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

        return string.Concat(Array.ConvertAll(byteArrayResult, h => h.ToString("X2")));
    }
}