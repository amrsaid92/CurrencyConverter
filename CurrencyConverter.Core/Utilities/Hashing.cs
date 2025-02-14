namespace CurrencyConverter.Core.Utilities
{
    public static class Hashing
    {
        private static string HashingSalt => "*&SaltConverter&Hashing#";
        public static string HashString(string text)
        {
            return BCrypt.Net.BCrypt.HashPassword(text + HashingSalt);
        }

        public static bool VerifyHashString(string text, string hashedText)
        {
            return BCrypt.Net.BCrypt.Verify(text + HashingSalt, hashedText);
        }
    }
}
