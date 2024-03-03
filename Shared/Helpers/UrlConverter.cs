namespace Shared.Helpers
{
    public static class UrlConverter
    {
        public static string Convert(string url)
        {

            return url.Trim()
                        .ToLower()
                        .Replace(" ", "-")
                        .Replace(".", "-")
                        .Replace("ş", "s")
                        .Replace("ç", "c")
                        .Replace("ü", "u")
                        .Replace("ğ", "g")
                        .Replace("ı", "i")
                        .Replace("ö", "o")
                        .Replace("'", "-");
        }
    }
}