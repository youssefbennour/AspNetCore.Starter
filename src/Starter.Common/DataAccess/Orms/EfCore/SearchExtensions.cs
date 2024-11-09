using System.Linq.Expressions;
using System.Reflection;
using Starter.Common.ErrorHandling.Exceptions;

namespace Starter.Common.DataAccess.Orms.EfCore;

public static class SearchExtensions
{
    private static readonly Lazy<MethodInfo> ContainsMethodInfo = new(Contains);
    private static readonly Lazy<MethodInfo> ToLowerMethodInfo = new(ToLower);
    private static MethodInfo Contains()=> typeof(String).GetMethod("Contains", [typeof(string)])
                                          ?? throw new InternalServerException(); 
    
    private static MethodInfo ToLower()=> typeof(string).GetMethod("ToLower", Type.EmptyTypes)
                                          ?? throw new InternalServerException(); 
    /// <summary>
    /// Filters the sequence by checking if the specified string field contains the search term (case-insensitive).
    /// </summary>
    /// <typeparam name="T">The type of elements in the sequence.</typeparam>
    /// <param name="query">The source IQueryable to filter.</param>
    /// <param name="field">Expression to select the string field to search in.</param>
    /// <param name="searchTerm">The term to search for.</param>
    /// <returns>An IQueryable with the search filter applied.</returns>
    /// <remarks>
    /// ⚠️ WARNING: Multiple consecutive calls to this method will combine conditions using AND operator.
    /// 
    /// Example of multiple calls:
    /// <code>
    /// var query = dbContext.Users
    ///     .Search(x => x.FirstName, "John")    // First condition
    ///     .Search(x => x.LastName, "Doe");     // AND second condition
    /// 
    /// // This is equivalent to: 
    /// // WHERE LOWER(FirstName) CONTAINS 'john' AND LOWER(LastName) CONTAINS 'doe'
    /// </code>
    /// 
    /// If you need OR conditions, consider creating a separate extension method or combine conditions before calling:
    /// <code>
    /// // Example helper method for OR conditions:
    /// public static IQueryable&lt;T&gt; SearchAny&lt;T&gt;(this IQueryable&lt;T&gt; query, 
    ///     string? searchTerm,
    ///     params Expression&lt;Func&lt;T, string&gt;&gt;[] fields) 
    /// {
    ///     // Implementation that combines fields with OR
    /// }
    /// 
    /// // Usage:
    /// var query = dbContext.Users.SearchAny("John", 
    ///     x => x.FirstName, 
    ///     x => x.LastName);
    /// </code>
    /// </remarks>
    public static IQueryable<T> Search<T>(this IQueryable<T> query, Expression<Func<T, string>> field, string? searchTerm) where T : class
    {
        if (string.IsNullOrEmpty(searchTerm))
        {
            return query;
        }

        var toLowerExpression = Expression.Call(field.Body, ToLowerMethodInfo.Value);
            
        var methodCallExpression = Expression.Call(
            toLowerExpression,
            ContainsMethodInfo.Value,
            Expression.Constant(searchTerm.Trim().ToLower()));

        var predicate = Expression.Lambda<Func<T, bool>>(methodCallExpression, field.Parameters[0]);
        return query.Where(predicate);
    } 
}