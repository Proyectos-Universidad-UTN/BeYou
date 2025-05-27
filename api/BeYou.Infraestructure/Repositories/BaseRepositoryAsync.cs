using Dapper;
using System.Collections;
using System.Linq.Expressions;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using BeYou.Infraestructure.Data;
using BeYou.Domain.Core.Models;
using BeYou.Application.Core.Interfaces;
using BeYou.Domain.Core.Specifications;
using Microsoft.EntityFrameworkCore.Query;
using BeYou.Application.Core.Models;
using BeYou.Common.Extensions;
using BeYou.Common.Helper;

namespace BeYou.Infraestructure.Repositories;

public class BaseRepositoryAsync<T>(ILoggerFactory loggerFactory, BeYouContext dbContext) : IBaseRepositoryAsync<T> where T : BaseSimpleDto
{
    private readonly ILogger<BaseRepositoryAsync<T>> _logger = loggerFactory.CreateLogger<BaseRepositoryAsync<T>>();

    private readonly BeYouContext _dbContext = dbContext;

    private readonly string[] basicIncludes = [];

    public virtual async Task<T> AddAsync(T entity, bool disableTracking = true)
    {
        if (disableTracking) DisableEntityTracking(entity);
        await _dbContext.Set<T>().AddAsync(entity);
        return entity;
    }

    public virtual async Task<IList<T>> AddRangeAsync(IList<T> entities, bool disableTracking = true)
    {
        if (disableTracking) DisableEntityTracking(entities);
        await _dbContext.Set<T>().AddRangeAsync(entities);
        return entities;
    }

    public virtual IQueryable<T> AsQueryable(params Expression<Func<T, object>>[]? includes)
    {
        var query = AsQueryable(null, includes);
        return query;
    }

    public virtual IQueryable<T> AsQueryable(ISpecification<T>? spec = null, params Expression<Func<T, object>>[]? includes)
    {
        var query = _dbContext.Set<T>().AsQueryable();
        if (spec != null) query = ApplySpecification(spec, query);
        query = query.AddIncludes(includes ?? []);
        return query;
    }

    public async Task<int> CountAsync(ISpecification<T> spec)
    {
        var query = ApplySpecification(spec);
        if (typeof(BaseEntity).IsAssignableFrom(typeof(T)))
        {
            query = query.Where(m => ((BaseEntity)(object)m).Active);
        }

        return await query.CountAsync();
    }

    public void Delete(T entity, bool disableTracking = true)
    {
        if (disableTracking) DisableEntityTracking(entity);

        _dbContext.Set<T>().Remove(entity);
    }

    public void Delete(long id, bool disableTracking = true)
    {
        T existing = _dbContext.Set<T>().Find(id)!;
        if (disableTracking) DisableEntityTracking(existing);

        _dbContext.Set<T>().Remove(existing);
    }

    public IList<T> Delete(IList<T> entities, bool disableTracking = true)
    {
        if (disableTracking) DisableEntityTracking(entities);

        _dbContext.Set<T>().RemoveRange(entities);
        return entities;
    }

    public async Task<int> ExecuteDeleteAsync(ISpecification<T> spec)
    {
        var query = _dbContext.Set<T>().AsQueryable();
        return await ApplySpecification(spec, query).ExecuteDeleteAsync();
    }

    public async Task<int> ExecuteUpdateAsync(Expression<Func<T, bool>> query, Expression<Func<SetPropertyCalls<T>, SetPropertyCalls<T>>> expression)
    {
        var updateFields = AppendModifiedProperty(expression);
        return await _dbContext.Set<T>().Where(query).ExecuteUpdateAsync(updateFields);
    }

    public async Task<int> ExecuteUpdateAsync(long id, Expression<Func<SetPropertyCalls<T>, SetPropertyCalls<T>>> expression)
    {
        var updateFields = AppendModifiedProperty(expression);
        return await _dbContext.Set<T>().Where(m => m.Id == id).ExecuteUpdateAsync(updateFields);
    }

    public async Task<bool> ExistsAsync(long id)
    {
        var entity = await _dbContext.Set<T>().FindAsync(id);
        if (entity == null) return false;

        if (entity is BaseEntity baseEntity)
        {
            return baseEntity.Active;
        }

        return true;
    }

    public async Task<T?> FirstOrDefaultAsync(ISpecification<T> spec) => await ApplySpecification(spec).FirstOrDefaultAsync();

    public async Task<T?> FirstOrDefaultAsync(ISpecification<T> spec, params string[]? includes)
    {
        var query = _dbContext.Set<T>().AsQueryable();
        query = query.AddIncludes(includes);

        query = ApplySpecification(spec, query);

        if (typeof(BaseEntity).IsAssignableFrom(typeof(T)))
        {
            query = query.Where(m => ((BaseEntity)(object)m).Active);
        }

        return await query.FirstOrDefaultAsync();
    }

    public async Task<T?> FirstOrDefaultAsync(ISpecification<T> spec, params Expression<Func<T, object>>[]? includes)
    {
        var query = _dbContext.Set<T>().AsQueryable();
        query = query.AddIncludes(includes!);

        query = ApplySpecification(spec, query);

        if (typeof(BaseEntity).IsAssignableFrom(typeof(T)))
        {
            query = query.Where(m => ((BaseEntity)(object)m).Active);
        }

        return await query.FirstOrDefaultAsync();
    }

    public async Task<T?> FirstOrDefaultAsync(IQueryable<T> query) => await FirstOrDefaultAsync(query, null as string[]);

    public async Task<T?> FirstOrDefaultAsync(IQueryable<T> query, params Expression<Func<T, object>>[]? includes)
    {
        query = query.AddIncludes(includes ?? []);
        if (typeof(BaseEntity).IsAssignableFrom(typeof(T)))
        {
            query = query.Where(m => ((BaseEntity)(object)m).Active);
        }

        return await query.FirstOrDefaultAsync();
    }

    public async Task<T?> FirstOrDefaultAsync(IQueryable<T> query, params string[]? includes)
    {
        query = query.AddIncludes(includes);

        if (typeof(BaseEntity).IsAssignableFrom(typeof(T)))
        {
            query = query.Where(m => ((BaseEntity)(object)m).Active);
        }

        return await query.FirstOrDefaultAsync();
    }

    public async Task<TResult?> FirstOrDefaultAsync<Tkey, TResult>(IQueryable<T> query, Expression<Func<T, Tkey>> grouping, Expression<Func<IGrouping<Tkey, T>, TResult>> resultSelector, params Expression<Func<T, object>>[]? includes)
    {
        query = query.AddIncludes(includes ?? []);
        if (typeof(BaseEntity).IsAssignableFrom(typeof(T)))
        {
            query = query.Where(m => ((BaseEntity)(object)m).Active);
        }

        return await query.GroupBy(grouping).Select(resultSelector).FirstOrDefaultAsync();
    }

    public virtual async Task<IList<T>> FromSqlAsync(FormattableString sql)
    {
        using var connection = new SqlConnection(_dbContext.Database.GetConnectionString());
        await connection.OpenAsync();
        var result = (await connection.QueryAsync<T>(sql.GetSQL(), new { })).ToList();

        if (typeof(BaseEntity).IsAssignableFrom(typeof(T)))
        {
            result = result.Where(m => ((BaseEntity)(object)m).Active).ToList();
        }

        return result;
    }

    public async Task<T?> GetByIdAsync(long id) => await GetByIdWithNoTrackingAsync(id, false, basicIncludes);

    public async Task<T?> GetByIdWithExpressionsAsync(long id, params Expression<Func<T, object>>[]? includes)
    {
        var query = _dbContext.Set<T>().AsQueryable();
        query = includes != null && includes.HasItems() ? query.AddIncludes(includes).AsNoTrackingWithIdentityResolution() : query.AsNoTracking();

        if (typeof(BaseEntity).IsAssignableFrom(typeof(T)))
        {
            query = query.Where(m => ((BaseEntity)(object)m).Active);
        }

        return await query.FirstOrDefaultAsync(m => m.Id == id);
    }

    public async Task<T?> GetByIdWithStringIncludesAsync(long id, params string[]? includes) => await GetByIdWithNoTrackingAsync(id, false, includes!);

    public async Task<T?> GetByIdWithNoTrackingAsync(long id, bool forceNoTracking = false, params string[] includes)
    {
        var query = _dbContext.Set<T>().AsQueryable();
        if (includes != null && includes.HasItems())
        {
            query = query.AddIncludes(includes);
            if (!forceNoTracking) query = query.AsNoTrackingWithIdentityResolution();
        }
        else
        {
            query = query.AsNoTracking();
        }
        if (forceNoTracking) query.AsNoTracking();

        if (typeof(BaseEntity).IsAssignableFrom(typeof(T)))
        {
            query = query.Where(m => ((BaseEntity)(object)m).Active);
        }

        return await query.FirstOrDefaultAsync(m => m.Id == id);
    }

    public PageResultDto<T> GetPageResult(IQueryable<T> query, PagingDetails pagingDetails)
    {
        var response = new PageResultDto<T>()
        {
            CurrentPage = pagingDetails.CurrentPage,
            PageSize = pagingDetails.PageSize,
            RowCount = query.Count()
        };

        var pageCount = (response.RowCount + response.PageSize - 1) / response.PageSize;
        response.PageCount = pageCount;
        var skip = (response.CurrentPage - 1) * response.PageSize;
        if (typeof(BaseEntity).IsAssignableFrom(typeof(T)))
        {
            query = query.Where(m => ((BaseEntity)(object)m).Active);
        }

        response.Results = query.Skip(skip).Take(response.PageSize).ToList();

        return response;
    }

    public PageResultDto<T> GetPageResult(IEnumerable<T> data, PagingDetails pagingDetails)
    {
        var response = new PageResultDto<T>()
        {
            CurrentPage = pagingDetails.CurrentPage,
            PageSize = pagingDetails.PageSize,
            RowCount = data != null ? data.Count() : 0
        };

        var pageCount = (response.RowCount + response.PageSize - 1) / response.PageSize;
        response.PageCount = pageCount;
        var skip = (response.CurrentPage - 1) * response.PageSize;

        if (typeof(BaseEntity).IsAssignableFrom(typeof(T)) && data != null)
        {
            data = data.Where(m => ((BaseEntity)(object)m).Active);
        }

        response.Results = data?.Skip(skip).Take(response.PageSize).ToList();

        return response;
    }

    public void IgnoreField(T entity, Expression<Func<T, object>> field)
    {
        var fieldName = LinqHelper.GetMemberName<T>(field);
        if (string.IsNullOrEmpty(fieldName)) return;

        _dbContext.Entry(entity).Property(fieldName).IsModified = false;
    }

    public async Task<IList<T>> ListAllAsync()
    {
        var query = _dbContext.Set<T>().AsQueryable();

        if (typeof(BaseEntity).IsAssignableFrom(typeof(T)))
        {
            query = query.Where(m => ((BaseEntity)(object)m).Active);
        }

        return await query.ToListAsync();
    }

    public async Task<IList<T>> ListAllAsync(params Expression<Func<T, object>>[] includes)
    {
        var query = _dbContext.Set<T>().AsQueryable();
        query = query.AddIncludes(includes);

        if (typeof(BaseEntity).IsAssignableFrom(typeof(T)))
        {
            query = query.Where(m => ((BaseEntity)(object)m).Active);
        }

        return await query.ToListAsync();
    }

    public async Task<IList<T>> ListAllAsync(params string[] includes)
    {
        var query = _dbContext.Set<T>().AsQueryable();
        query = query.AddIncludes(includes);

        if (typeof(BaseEntity).IsAssignableFrom(typeof(T)))
        {
            query = query.Where(m => ((BaseEntity)(object)m).Active);
        }

        return await query.ToListAsync();
    }

    public async Task<IList<T>> ListAsync(ISpecification<T> spec)
    {
        var query = ApplySpecification(spec);

        if (typeof(BaseEntity).IsAssignableFrom(typeof(T)))
        {
            query = query.Where(m => ((BaseEntity)(object)m).Active);
        }

        return await query.ToListAsync();
    }

    public async Task<IList<T>> ListAsync(ISpecification<T> spec, params Expression<Func<T, object>>[] includes)
    {
        var query = _dbContext.Set<T>().AsQueryable();
        query = query.AddIncludes(includes);

        query = ApplySpecification(spec, query);

        if (typeof(BaseEntity).IsAssignableFrom(typeof(T)))
        {
            query = query.Where(m => ((BaseEntity)(object)m).Active);
        }

        return await query.ToListAsync();
    }

    public async Task<IList<T>> ListAsync(ISpecification<T> spec, params string[] includes)
    {
        var query = _dbContext.Set<T>().AsQueryable();
        query = query.AddIncludes(includes);

        query = ApplySpecification(spec, query);

        if (typeof(BaseEntity).IsAssignableFrom(typeof(T)))
        {
            query = query.Where(m => ((BaseEntity)(object)m).Active);
        }

        return await query.ToListAsync();
    }

    public async Task<IList<TResult>> ListAsync<Tkey, TResult>(ISpecification<T> spec, Expression<Func<T, Tkey>> grouping, Expression<Func<IGrouping<Tkey, T>, TResult>> resultSelector)
    {
        var query = ApplySpecification(spec);
        if (typeof(BaseEntity).IsAssignableFrom(typeof(T)))
        {
            query = query.Where(m => ((BaseEntity)(object)m).Active);
        }

        return await query.GroupBy(grouping).Select(resultSelector).ToListAsync();
    }

    public async Task<IList<TResult>> ListAsync<Tkey, TResult>(IQueryable<T> query, Expression<Func<T, Tkey>> grouping, Expression<Func<IGrouping<Tkey, T>, TResult>> resultSelector, params Expression<Func<T, object>>[] includes)
    {
        query = query.AddIncludes(includes ?? []);
        if (typeof(BaseEntity).IsAssignableFrom(typeof(T)))
        {
            query = query.Where(m => ((BaseEntity)(object)m).Active);
        }

        return await query.GroupBy(grouping).Select(resultSelector).ToListAsync();
    }

    public async Task<IList<T>> ListAsync(IQueryable<T> query) => await ListAsync(query, null as string[]);

    public async Task<IList<T>> ListAsync(IQueryable<T> query, params string[]? includes)
    {
        query = query.AddIncludes(includes);
        if (typeof(BaseEntity).IsAssignableFrom(typeof(T)))
        {
            query = query.Where(m => ((BaseEntity)(object)m).Active);
        }

        return await query.ToListAsync();
    }

    public async Task<IList<T>> ListAsync(IQueryable<T> query, params Expression<Func<T, object>>[]? includes)
    {
        query = query.AddIncludes(includes ?? []);
        if (typeof(BaseEntity).IsAssignableFrom(typeof(T)))
        {
            query = query.Where(m => ((BaseEntity)(object)m).Active);
        }

        return await query.ToListAsync();
    }

    public void RemoveRange(IList<T> collection) => _dbContext.Set<T>().RemoveRange(collection);

    public void Update(T entity, bool disableTracking = true)
    {
        if (disableTracking) DisableEntityTracking(entity);
        _dbContext.Set<T>().Update(entity);
    }

    private void DisableEntityTracking(IList<T> entities)
    {
        foreach (var item in entities)
        {
            DisableEntityTracking(item);
        }
    }

    private void DisableEntityTracking(object entity)
    {
        try
        {
            if (!entity.GetType().IsSubclassOf(typeof(BaseEntity))) return;

            var props = entity.GetType().GetProperties();
            foreach (var prop in props)
            {
                var value = prop.GetValue(entity);
                if (value != null && prop.ReflectedType != null && prop.ReflectedType.IsSubclassOf(typeof(BaseEntity)))
                {
                    if (value is IEnumerable enumerable)
                    {
                        foreach (var item in enumerable)
                        {
                            DisableEntityTracking(item);
                        }
                    }
                    else
                    {
                        DisableEntityTracking(value);
                    }
                }
            }

            EntityTrackingAddingUpdating(entity);
        }
        catch (Exception exception)
        {
            _logger.LogError(exception, "Error ocurred on state updating");
        }
    }

    #region TrackingAndDetach
    private void EntityTrackingAddingUpdating(object entity)
    {
        var id = (long)entity.GetType().GetProperty(nameof(BaseEntity.Id))!.GetValue(entity)!;

        if (id == 0)
        {
            _dbContext.Entry(entity).State = EntityState.Added;
        }
        else
        {
            DetachCorrespondingEntityByBaseEntity(entity, id);
        }
    }

    private void DetachCorrespondingEntityByBaseEntity(object entity, long id)
    {
        var attachedEntity = _dbContext.ChangeTracker.Entries<BaseEntity>().FirstOrDefault(m => m.Entity.GetType() == entity.GetType() && m.Entity.Id == id);
        if (attachedEntity != null) _dbContext.Entry(attachedEntity.Entity).State = EntityState.Detached;

        _dbContext.Entry(entity).State = EntityState.Modified;
    }

    #endregion

    private IQueryable<T> ApplySpecification(ISpecification<T> spec)
    {
        return SpecificationEvaluator<T>.GetQuery(_dbContext.Set<T>().AsQueryable(), spec);
    }

    private static IQueryable<T> ApplySpecification(ISpecification<T> spec, IQueryable<T> query)
    {
        return SpecificationEvaluator<T>.GetQuery(query, spec);
    }

    private static Expression<Func<SetPropertyCalls<T>, SetPropertyCalls<T>>> AppendModifiedProperty(Expression<Func<SetPropertyCalls<T>, SetPropertyCalls<T>>> expression)
    {
        Expression<Func<SetPropertyCalls<T>, SetPropertyCalls<T>>> updateFields = calls => calls;
        updateFields = AppendSetProperty(updateFields, expression);
        updateFields = AppendSetProperty(updateFields, m => m.SetProperty(x => (x as BaseEntity)!.Updated, DateTime.Now));
        return updateFields;
    }

    private static Expression<Func<SetPropertyCalls<T>, SetPropertyCalls<T>>> AppendSetProperty(
        Expression<Func<SetPropertyCalls<T>, SetPropertyCalls<T>>> currentSet,
        Expression<Func<SetPropertyCalls<T>, SetPropertyCalls<T>>> nextSet)
    {
        var replace = new ReplacingExpressionVisitor(nextSet.Parameters, [currentSet.Body]);
        var combined = replace.Visit(currentSet.Body);
        return Expression.Lambda<Func<SetPropertyCalls<T>, SetPropertyCalls<T>>>(combined, currentSet.Parameters);
    }
}
