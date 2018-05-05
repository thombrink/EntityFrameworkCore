using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

namespace ConsoleApp1
{
    public static class Extensions
    {
        public static IQueryable<TEntity> Include<TEntity, TProperty, TJoinEntity>(
            this IQueryable<TEntity> source,
            Expression<Func<TEntity, ManyToManyList<TJoinEntity, TProperty>>> navigationPropertyPath)
            where TEntity : class
            where TProperty : Entity
            where TJoinEntity : class, IJoinEntity, new()
        {
            var memberExpression = navigationPropertyPath.Body as MemberExpression;
            var memberName = memberExpression.Member.Name;

            var propertyType = typeof(TProperty);
            var joinEntityType = typeof(TJoinEntity);

            var matchingProperties = joinEntityType.GetProperties().Where(x => x.PropertyType == propertyType);
            PropertyInfo property = null;
            foreach (var p in matchingProperties)
            {
                if(property == null)
                {
                    property = p;
                }
                else
                {
                    throw new Exception($"Multiple properties of the type '{propertyType.Name}' are not allowed.");
                }
            }

            if(property == null)
            {
                throw new Exception($"The type '{joinEntityType.Name} does not contain a property of the type '{propertyType.Name}.");
            }

            var propertyName = property.Name;

            return source.Include($"{memberName}.{propertyName}");
        }
    }
}
