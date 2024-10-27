using System.Linq.Expressions;
using System.Reflection;

namespace Softylines.Contably.Common.DataAccess.Orms.EfCore;

public static class SearchExtensions
{
    private static readonly Lazy<MethodInfo> ContainsMethodInfo = new(Contains);
    private static readonly Lazy<MethodInfo> ToLowerMethodInfo = new(ToLower);
    private static MethodInfo Contains()=> typeof(String).GetMethod("Contains", [typeof(string)])
                                          ?? throw new InternalServerException(); 
    
    private static MethodInfo ToLower()=> typeof(string).GetMethod("ToLower", Type.EmptyTypes)
                                          ?? throw new InternalServerException(); 
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
    
    public static IQueryable<T> Search<T>(this IQueryable<T> query, List<Expression<Func<T, string>>> fields, string? searchTerm) where T : class
    {
        if (string.IsNullOrEmpty(searchTerm) || fields == null || fields.Count == 0)
        {
            return query;
        }

        var parameter = Expression.Parameter(typeof(T), "x");
        var trimmedLowerSearchTerm = searchTerm.Trim().ToLower();

        Expression? combinedExpression = null;

        foreach (var field in fields)
        {
            var fieldExpression = Expression.Invoke(field, parameter);
            var toLowerExpression = Expression.Call(fieldExpression, ToLowerMethodInfo.Value);
            var containsExpression = Expression.Call(toLowerExpression, ContainsMethodInfo.Value, Expression.Constant(trimmedLowerSearchTerm));

            if (combinedExpression == null)
            {
                combinedExpression = containsExpression;
            }
            else
            {
                combinedExpression = Expression.OrElse(combinedExpression, containsExpression);
            }
        }

        if (combinedExpression == null)
        {
            return query;
        }

        var predicate = Expression.Lambda<Func<T, bool>>(combinedExpression, parameter);
        return query.Where(predicate);
    }
}