using System.Text;
using Microsoft.AspNetCore.WebUtilities;

namespace Shared.Helpers
{
    public static class UrlConverter
    {
        public static string Edit(string url)
        {

            return url is not null ? url.Trim()
                        .ToLower()
                        .Replace(" ", "-")
                        .Replace(".", "-")
                        .Replace("ş", "s")
                        .Replace("ç", "c")
                        .Replace("ü", "u")
                        .Replace("ğ", "g")
                        .Replace("ı", "i")
                        .Replace("ö", "o")
                        .Replace("'", "-")

            : string.Empty;
        }

        public static string EncodeUrl(string token) 
        {
            return WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(token));
        }

        public static string DecodeUrl(string token)
        {
            return Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(token));
        }
    }
}