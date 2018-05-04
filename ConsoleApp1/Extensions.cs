using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

namespace ConsoleApp1
{
    public static class Extensions
    {
        public static IIncludableQueryable<TEntity, TProperty> Include<TEntity, TProperty, TJoinEntity>(
            this IQueryable<TEntity> source,
            Expression<Func<TEntity, ManyToManyList<TJoinEntity, TProperty>>> navigationPropertyPath)
            where TEntity : class
            where TProperty : Entity
            where TJoinEntity : class, IJoinEntity, new()
        {
            var propertyType = typeof(TJoinEntity);
            var parameter = Expression.Parameter(propertyType, "x");
            var returnProperty = propertyType.GetProperties().First(x => x.PropertyType == typeof(TProperty));
            var name = returnProperty.Name;
            var body = Expression.PropertyOrField(parameter, name);
            var expr = Expression.Lambda<Func<TJoinEntity, TProperty>>(body, parameter);

            return ((IIncludableQueryable<TEntity, IEnumerable<TJoinEntity>>)EntityFrameworkQueryableExtensions.Include(source, navigationPropertyPath)).ThenInclude(expr);
        }
    }
}
