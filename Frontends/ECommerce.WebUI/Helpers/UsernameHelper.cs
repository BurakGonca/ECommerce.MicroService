using System.Text.RegularExpressions;

namespace ECommerce.WebUI.Helpers
{
    public class UsernameHelper
    {
        public static string NormalizeUsername(string name, string surname)
        {
            string userName = $"{name}_{surname}"; 
            userName = ReplaceTurkishCharacters(userName);
            userName = Regex.Replace(userName, @"[^a-zA-Z0-9_]", "");

            return userName.ToLower(); 
        }

        private static string ReplaceTurkishCharacters(string userName)
        {
            userName = userName.Replace("ı", "i")
                       .Replace("İ", "I")
                       .Replace("ğ", "g")
                       .Replace("Ğ", "G")
                       .Replace("ü", "u")
                       .Replace("Ü", "U")
                       .Replace("ş", "s")
                       .Replace("Ş", "S")
                       .Replace("ö", "o")
                       .Replace("Ö", "O")
                       .Replace("ç", "c")
                       .Replace("Ç", "C");
            return userName;
        }

    }
}
