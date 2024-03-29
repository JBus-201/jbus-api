using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using API.Interfaces;

namespace API.Services;


public class CryptoService(string key, string iv) : ICryptoService
{
    private readonly byte[] _key = SHA256.HashData(Encoding.UTF8.GetBytes(key));
    private readonly byte[] _iv = Encoding.UTF8.GetBytes(iv[..16]);

    public string Encrypt(string plainText)
    {
        using var aes = Aes.Create();
        aes.Key = _key;
        aes.IV = _iv;

        var encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

        using var msEncrypt = new MemoryStream();
        using (var csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
        using (var swEncrypt = new StreamWriter(csEncrypt))
        {
            swEncrypt.Write(plainText);
        }
        string encryptedText = Convert.ToBase64String(msEncrypt.ToArray());

        
        return encryptedText;
    }

    public string Decrypt(string cipherText)
    {
        if (string.IsNullOrWhiteSpace(cipherText) || cipherText.Length % 4 != 0 || !Regex.IsMatch(cipherText, @"^[a-zA-Z0-9\+/]*={0,3}$", RegexOptions.None))
        {
            throw new ArgumentException("The encryptedText is not a valid Base-64 string.", nameof(cipherText));
        }

        using var aes = Aes.Create();
        aes.Key = _key;
        aes.IV = _iv;

        var decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

        using var msDecrypt = new MemoryStream(Convert.FromBase64String(cipherText));
        using var csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read);
        using var srDecrypt = new StreamReader(csDecrypt);
        return srDecrypt.ReadToEnd();
    }
}
