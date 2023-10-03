using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

namespace BookStore.Helpers
{
    public static class Slug
    {
        public static string Slugify(string input)
        {
            if (input == null)
                return string.Empty;
            input = RemoveDiacritics(input);
            input = Regex.Replace(input, @"[^a-z0-9\s-]", string.Empty, RegexOptions.IgnoreCase);
            input = input.Replace(" ", "-").Trim();
            input = Regex.Replace(input, @"-+", "-");
            return input.ToLower();
        }

        private static string RemoveDiacritics(string text)
        {
            string normalized = text.Normalize(NormalizationForm.FormD);
            StringBuilder sb = new StringBuilder();
            foreach (char c in normalized)
            {
                UnicodeCategory category = CharUnicodeInfo.GetUnicodeCategory(c);
                if (category != UnicodeCategory.NonSpacingMark)
                    sb.Append(c);
            }
            return sb.ToString().Normalize(NormalizationForm.FormC);
        }
    }
}