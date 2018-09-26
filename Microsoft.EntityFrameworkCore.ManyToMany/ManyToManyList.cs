﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace Microsoft.EntityFrameworkCore
{
    public class ManyToManyList<TSource, TResult> : List<TSource> where TSource : IJoinEntity, new() where TResult : Entity
    {
        private PropertyInfo sourceIdProperty;
        private PropertyInfo resultIdProperty;
        private PropertyInfo resultEntityProperty;

        private readonly Guid entityKey;

        /// <summary>
        /// Initializes a new instance of the <see cref="ManyToManyList{TSource, TResult}"/> class, to keep EF happy.
        /// </summary>
        public ManyToManyList() {
            var properties = typeof(TSource).GetProperties();

            resultIdProperty = properties.FirstOrDefault(x => x.Name == typeof(TResult).Name + "Id" && x.PropertyType == typeof(Guid));
            if (resultIdProperty == null) throw new Exception($"Property of type 'Guid' and name '{typeof(TResult).Name}Id' not found inside '{typeof(TSource).Name}'");

            resultEntityProperty = properties.FirstOrDefault(x => x.PropertyType == typeof(TResult));
            if (resultEntityProperty == null) throw new Exception($"Property of type '{typeof(TResult).Name}' not found inside '{typeof(TSource).Name}'");
        }

        public ManyToManyList(Entity entity) : this()
        {
            this.entityKey = entity.Id;

            var name = entity.GetType().Name;
            var properties = typeof(TSource).GetProperties();

            sourceIdProperty = properties.FirstOrDefault(x => x.Name == name + "Id" && x.PropertyType == typeof(Guid));
            if (sourceIdProperty == null) throw new Exception($"Property of type 'Guid' and name '{name}Id' not found inside '{typeof(TSource).Name}'");
        }

        public ManyToManyList(Entity entity, IEnumerable<TResult> collection) : this(entity)
        {
            foreach(var item in collection)
            {
                Add(item);
            }
        }

        public bool IsReadOnly => throw new NotImplementedException();

        public new int Count => throw new NotImplementedException();

        public new TResult this[int index] { get => (TResult)resultEntityProperty.GetValue(base[index]); set => throw new NotImplementedException(); }

        public new IEnumerator<TResult> GetEnumerator()
        {
            using (var ie = base.GetEnumerator())
            {
                while (ie.MoveNext())
                {
                    yield return (TResult)resultEntityProperty.GetValue(ie.Current);
                }
            }
        }

        public void Add(TResult item)
        {
            var parameter = Expression.Parameter(typeof(TSource), "x");
            var delegateType = typeof(Func<,>).MakeGenericType(typeof(TSource), typeof(bool));

            Expression prop = Expression.PropertyOrField(parameter, resultIdProperty.Name);
            Expression predicate = Expression.Equal(prop, Expression.Constant(item.Id));

            var lambda = Expression.Lambda<Func<TSource, bool>>(predicate, parameter).Compile();

            var existing = base.Find(new Predicate<TSource>(lambda));
            if (existing != null) return;

            var newItem = new TSource();
            sourceIdProperty.SetValue(newItem, entityKey);
            resultIdProperty.SetValue(newItem, item.Id);

            base.Add(newItem);
        }

        public bool Contains(TResult item)
        {
            throw new NotImplementedException();
        }

        public void CopyTo(TResult[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }

        public bool Remove(TResult item)
        {
            throw new NotImplementedException();
        }

        public int IndexOf(TResult item)
        {
            throw new NotImplementedException();
        }

        public void Insert(int index, TResult item)
        {
            throw new NotImplementedException();
        }

        public new void RemoveAt(int index)
        {
            throw new NotImplementedException();
        }

        public new void Clear()
        {
            throw new NotImplementedException();
        }
    }
}