using System.Linq.Expressions;

namespace BeYou.Common.Helper;

/// <summary>
/// Helper class for LINQ expressions.
/// </summary>
public static class LinqHelper
{
    /// <summary>
    /// Gets the name of the member from a LINQ expression.
    /// </summary>
    /// <typeparam name="T">Any object</typeparam>
    /// <param name="expression">Expression function</param>
    /// <returns>Name of member</returns>
    public static string? GetMemberName<T>(Expression<Func<T, object>> expression)
    {
        return expression.Body switch
        {
            MemberExpression m => m.Member.Name,
            UnaryExpression u when u.Operand is MemberExpression m => m.Member.Name,
            _ => null,
        };
    }
}