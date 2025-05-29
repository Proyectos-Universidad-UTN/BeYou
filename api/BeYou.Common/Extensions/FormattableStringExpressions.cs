namespace BeYou.Common.Extensions;
/// <summary>
/// Extension methods for FormattableString.
/// </summary>
public static class FormattableStringExpressions
{
    /// <summary>
    /// Converts a FormattableString to a SQL string.
    /// </summary>
    /// <param name="instance">FormattableString instance</param>
    /// <returns>SQL string to be executed</returns>
    public static string GetSQL(this FormattableString instance) => FormattableString.Invariant(instance);
}
