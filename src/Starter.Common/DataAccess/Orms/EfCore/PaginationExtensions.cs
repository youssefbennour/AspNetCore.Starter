using Microsoft.EntityFrameworkCore;
using Starter.Common.Requests.Models;
using Starter.Common.Responses.Models;

namespace Microsoft.Linq.Queryable;

public static class PaginationExtensions
{
    private const int DefaultPageSize = 10;
    private const int DefaultPageNumber = 1;

    public static async Task<PaginatedList<T>> ToPaginatedListAsync<T>(
        this IQueryable<T> query, 
        QueryParameters queryParameters, 
        CancellationToken cancellationToken = default (CancellationToken)) 
        where T : class
    {
        var items = await query.Paginate(queryParameters)
            .ToListAsync(cancellationToken);

        var count = await query.CountAsync(cancellationToken);

        return new PaginatedList<T>(items, queryParameters, count);
    }
   
    public static async Task<PaginatedList<T>> ToPaginatedListAsync<T>(
        this IQueryable<T> query, 
        int pageNumber,
        int pageSize,
        CancellationToken cancellationToken = default (CancellationToken)) 
        where T : class
    {
        var items = await query.Paginate(pageNumber, pageSize)
            .ToListAsync(cancellationToken);

        var count = await query.CountAsync(cancellationToken);

        return new PaginatedList<T>(items, pageNumber, pageSize, count);
    }
    
    public static IQueryable<T> Paginate<T>(this IQueryable<T> query, QueryParameters? requestParameters)
        where T : class => 
        query.Paginate(
            pageNumber:requestParameters?.PageNumber ?? DefaultPageNumber,
            pageSize: requestParameters?.PageSize ?? DefaultPageSize);
        
    public static IQueryable<T> Paginate<T>(this IQueryable<T> query, int pageNumber, int pageSize)
        where T : class => query.Skip((pageNumber - 1) * pageSize)
        .Take(pageSize);
}