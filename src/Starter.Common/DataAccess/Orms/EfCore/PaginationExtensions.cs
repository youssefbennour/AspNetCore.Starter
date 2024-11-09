using Microsoft.EntityFrameworkCore;
using Starter.Common.Requests.Models;
using Starter.Common.Responses.Models;

namespace Starter.Common.DataAccess.Orms.EfCore;

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
    
    public static PaginatedList<T> ToPaginatedList<T>(
        this IEnumerable<T> data, 
        QueryParameters queryParameters) 
        where T : class
    {
        var dataList = data.ToList();
        var items = dataList.Paginate(queryParameters);

        return new PaginatedList<T>(items, queryParameters, dataList.Count);
    }
    
    public static IQueryable<T> Paginate<T>(this IQueryable<T> query, QueryParameters? requestParameters)
        where T : class => 
        query.Paginate(
            pageNumber:requestParameters?.PageNumber ?? DefaultPageNumber,
            pageSize: requestParameters?.PageSize ?? DefaultPageSize);
        
    public static IQueryable<T> Paginate<T>(this IQueryable<T> query, int pageNumber, int pageSize)
        where T : class => query.Skip((pageNumber - 1) * pageSize)
        .Take(pageSize);
    
    public static List<T> Paginate<T>(this List<T> query, QueryParameters? requestParameters)
        where T : class => 
        query.Paginate(
            pageNumber:requestParameters?.PageNumber ?? DefaultPageNumber,
            pageSize: requestParameters?.PageSize ?? DefaultPageSize);
        
    public static List<T> Paginate<T>(this List<T> query, int pageNumber, int pageSize)
        where T : class => query.Skip((pageNumber - 1) * pageSize)
        .Take(pageSize)
        .ToList();
    
}