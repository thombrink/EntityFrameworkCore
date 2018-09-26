using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using ManyToMany.ConsoleDemo.DAO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

namespace ManyToMany.ConsoleDemo
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
                if (property == null)
                {
                    property = p;
                }
                else
                {
                    throw new Exception($"Multiple properties of the type '{propertyType.Name}' are not allowed.");
                }
            }

            if (property == null)
            {
                throw new Exception($"The type '{joinEntityType.Name} does not contain a property of the type '{propertyType.Name}.");
            }

            var propertyName = property.Name;

            //return EntityFrameworkQueryableExtensions.Include(source, navigationPropertyPath).ThenInclude(;
            return source.Include($"{memberName}.{propertyName}");
        }

        //public static IEnumerable<TResult> Select<TSource, TResult>(this IEnumerable<TSource> source, Func<TSource, TResult> selector);
        public static IEnumerable<TResult> Select<TJoinEntity, TProperty, TResult>(this ManyToManyList<TJoinEntity, TProperty> source, Func<TProperty, TResult> selector) where TProperty : Entity where TJoinEntity : class, IJoinEntity, new()
        {
            var enumarator = source.GetEnumerator();

            while (enumarator.MoveNext())
            {
                yield return selector(enumarator.Current);
            }
        }

        //public static IEnumerable<TSource> AsEnumerable<TSource>(this IEnumerable<TSource> source)
        public static IEnumerable<TProperty> AsEnumerable<TJoinEntity, TProperty>(this ManyToManyList<TJoinEntity, TProperty> source) where TProperty : Entity where TJoinEntity : class, IJoinEntity, new()
        {
            var enumarator = source.GetEnumerator();
            while (enumarator.MoveNext())
            {
                yield return enumarator.Current;
            }
        }
    }
}
