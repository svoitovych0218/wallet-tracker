namespace Wallet.Tracker.Api.Extensions;

using System.Security.Cryptography;

public static class StringHashExtension
{
    public static string GetStringSha256Hash(this string text)
    {
        if (String.IsNullOrEmpty(text))
            return String.Empty;

        using (var sha = SHA256.Create())
        {
            byte[] textData = System.Text.Encoding.UTF8.GetBytes(text);
            byte[] hash = sha.ComputeHash(textData);
            return BitConverter.ToString(hash).Replace("-", String.Empty);
        }
    }
}
