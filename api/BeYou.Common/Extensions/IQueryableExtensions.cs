using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;


namespace BeYou.Common.Extensions;

/// <summary>
/// Extension methods for IQueryable.
/// </summary>
public static class IQueryableExtensions
{
    /// <summary>
    /// Adds includes to the query.
    /// </summary>
    /// <typeparam name="T">Any</typeparam>
    /// <param name="query">Queryable source</param>
    /// <param name="includes">collections of includes to be added</param>
    /// <returns>Queryable objects with inclusions</returns>
    public static IQueryable<T> AddIncludes<T>(this IQueryable<T> query, params string[]? includes) where T : class
    {
        if (includes != null) query = includes.Aggregate(query, (current, include) => current.Include(include));

        return query;
    }

    /// <summary>
    /// Adds includes to the query.
    /// </summary>
    /// <typeparam name="T">Any</typeparam>
    /// <param name="query">Queryable source</param>
    /// <param name="includes">collections of includes to be added</param>
    /// <returns>Queryable objects with inclusions</returns>
    public static IQueryable<T> AddIncludes<T>(this IQueryable<T> query, Expression<Func<T, object>>[] includes) where T : class
    {
        if (includes != null) query = includes.Aggregate(query, (current, include) => current.Include(include));

        return query;
    }
}