namespace WebApi.Extensions.TextExtensions
{
    public static class StringExtensions
    {
        public static string SplitAfter(this string s, char c)
        {
            int pos = s.IndexOf(c);
            if (pos == -1)
            {
                return string.Empty;
            }
            return s.Substring(pos + 1);
        }
    }
}
