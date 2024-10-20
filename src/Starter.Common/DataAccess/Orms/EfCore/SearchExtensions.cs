using System.Linq.Expressions;
using System.Reflection;
using Starter.Common.ErrorHandling.Exceptions;

namespace Microsoft.Linq.Queryable;

public static class SearchExtensions
{
    public static IQueryable<T> Search<T>(this IQueryable<T> query, Expression<Func<T, string>> field, string? searchTerm) where T : class
    {
        if (string.IsNullOrEmpty(searchTerm))
        {
            return query;
        }

        MethodInfo containsMethod = typeof(String).GetMethod("Contains", new []{typeof(string)})
            ?? throw new InternalServerException();

        MethodInfo toLowerMethod = typeof(string).GetMethod("ToLower", Type.EmptyTypes)
                                   ?? throw new InternalServerException();
        
        var toLowerExpression = Expression.Call(field.Body, toLowerMethod);
            
        var methodCallExpression = Expression.Call(
            toLowerExpression,
            containsMethod,
            Expression.Constant(searchTerm.Trim().ToLower()));

        var predicate = Expression.Lambda<Func<T, bool>>(methodCallExpression, field.Parameters[0]);
        return query.Where(predicate);
    } 
 
}