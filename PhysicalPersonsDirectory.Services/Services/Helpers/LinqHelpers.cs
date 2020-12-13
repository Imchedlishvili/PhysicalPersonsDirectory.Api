using System;
using System.Linq;
using System.Linq.Expressions;

namespace PhysicalPersonsDirectory.Services.Services.Helpers
{
    public static class LinqHelpers
    {
        public static IQueryable<T> OrderByAsQueryable<T>(this IQueryable<T> source, string columnName, bool isAscending = true)
        {
            if (String.IsNullOrEmpty(columnName))
            {
                return source;
            }

            string command = isAscending ? "OrderBy" : "OrderByDescending";
            var type = typeof(T);
            var property = type.GetProperty(columnName);
            var parameter = Expression.Parameter(type, "p");
            var propertyAccess = Expression.MakeMemberAccess(parameter, property);
            var orderByExpression = Expression.Lambda(propertyAccess, parameter);
            var resultExpression = Expression.Call(typeof(Queryable), command, new Type[] { type, property.PropertyType }, source.Expression, Expression.Quote(orderByExpression));

            return source.Provider.CreateQuery<T>(resultExpression);
        }
    }
}
