namespace BeYou.Utils;

public static class StringExtension
{
    /// <summary>
    /// Capitalize text/word
    /// </summary>
    /// <param name="value">Text to be capitalized</param>
    /// <returns>string</returns>
    public static string Capitalize(this string value) =>
        String.IsNullOrEmpty(value) ? String.Empty : char.ToUpper(value[0]) + value.Substring(1).ToLower();
}

