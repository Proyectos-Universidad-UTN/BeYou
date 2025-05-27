using BeYou.Domain.Core.Models;
using BeYou.Domain.Core.Specifications;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace BeYou.Infraestructure.Repositories;

public static class SpecificationEvaluator<T> where T : BaseSimpleDto
{
    public static IQueryable<T> GetQuery(IQueryable<T> inputQuery, ISpecification<T> specification)
    {
        var query = inputQuery;

        query = ApplyCriteria(query, specification);
        query = ApplyIncludes(query, specification);
        query = ApplyOrdering(query, specification);
        query = ApplyPaging(query, specification);
        query = ApplyTracking(query, specification);

        return query;
    }

    private static IQueryable<T> ApplyCriteria(IQueryable<T> query, ISpecification<T> specification)
    {
        return specification.Criteria != null ? query.Where(specification.Criteria) : query;
    }

    private static IQueryable<T> ApplyIncludes(IQueryable<T> query, ISpecification<T> specification)
    {
        query = specification.Includes.Aggregate(query, (current, include) => current.Include(include));
        query = specification.IncludeString.Aggregate(query, (current, include) => current.Include(include));
        return query;
    }

    private static IQueryable<T> ApplyOrdering(IQueryable<T> query, ISpecification<T> specification)
    {
        IOrderedQueryable<T>? orderedQuery = null;

        if (specification.OrderBy?.Any() == true)
        {
            foreach (var item in specification.OrderBy)
            {
                orderedQuery = orderedQuery == null ? query.OrderBy(item) : orderedQuery.ThenBy(item);
            }
        }

        if (specification.OrderByDescending?.Any() == true)
        {
            foreach (var item in specification.OrderByDescending)
            {
                orderedQuery = orderedQuery == null ? query.OrderByDescending(item) : orderedQuery.ThenByDescending(item);
            }
        }

        return orderedQuery ?? query;
    }

    private static IQueryable<T> ApplyPaging(IQueryable<T> query, ISpecification<T> specification)
    {
        return specification.IsPagingEnabled
            ? query.Skip(specification.Skip).Take(specification.Take)
            : query;
    }

    private static IQueryable<T> ApplyTracking(IQueryable<T> query, ISpecification<T> specification)
    {
        if (specification.NoTracking)
            return query.AsNoTracking();

        return specification.NoTrackingWithIdentityResolution
            ? query.AsNoTrackingWithIdentityResolution()
            : query;
    }
}
