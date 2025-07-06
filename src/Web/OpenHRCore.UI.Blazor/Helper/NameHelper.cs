namespace OpenHRCore.UI.Blazor.Helper
{
    public static class NameHelper
    {
        public static string GetInitials(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                return "?";

            name = name.Trim();

            // Case 1: Space-separated words
            if (name.Contains(' '))
            {
                var initials = name
                    .Split(' ', StringSplitOptions.RemoveEmptyEntries)
                    .Take(2)
                    .Select(w => char.ToUpperInvariant(w[0]));

                return string.Concat(initials);
            }

            // Case 2: PascalCase or camelCase (extract uppercase letters)
            var upperChars = name
                .Where(char.IsUpper)
                .Take(2)
                .ToArray();

            if (upperChars.Length > 0)
                return new string(upperChars);

            // Case 3: Fallback to first 1–2 characters
            return name.Length >= 2
                ? name.Substring(0, 2).ToUpperInvariant()
                : name.Substring(0, 1).ToUpperInvariant();
        }
    }
}
