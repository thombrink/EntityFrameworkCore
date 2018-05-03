using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace ConsoleApp1
{
    public class ManyToManyList<TSource, TResult> : List<TSource>, IList<TResult> where TSource : ILinkEntity, new() where TResult : Entity
    {
        private PropertyInfo sourceGuidProperty;
        private PropertyInfo resultGuidProperty;
        private PropertyInfo resultEntityProperty;

        private Guid entityKey;

        public ManyToManyList(Entity entity)
        {
            this.entityKey = entity.Id;

            var name = entity.GetType().Name;
            var properties = typeof(TSource).GetProperties();

            sourceGuidProperty = properties.FirstOrDefault(x => x.Name == name + "Id" && x.PropertyType == typeof(Guid));
            if (sourceGuidProperty == null) throw new Exception($"Property of type 'Guid' and name '{name}Id' not found inside '{typeof(TSource).Name}'");

            resultGuidProperty = properties.FirstOrDefault(x => x.Name == typeof(TResult).Name + "Id" && x.PropertyType == typeof(Guid));
            if (sourceGuidProperty == null) throw new Exception($"Property of type 'Guid' and name '{typeof(TResult).Name}Id' not found inside '{typeof(TSource).Name}'");

            resultEntityProperty = properties.FirstOrDefault(x => x.PropertyType == typeof(TResult));
            if (resultEntityProperty == null) throw new Exception($"Property of type '{typeof(TResult).Name}' not found inside '{typeof(TSource).Name}'");
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

            Expression prop = Expression.PropertyOrField(parameter, resultGuidProperty.Name);
            Expression predicate = Expression.Equal(prop, Expression.Constant(item.Id));

            var lambda = Expression.Lambda<Func<TSource, bool>>(predicate, parameter).Compile();

            var existing = base.Find(new Predicate<TSource>(lambda));
            if (existing != null) return;

            var newItem = new TSource();
            sourceGuidProperty.SetValue(newItem, entityKey);
            resultGuidProperty.SetValue(newItem, item.Id);

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

        IEnumerator IEnumerable.GetEnumerator()
        {
            return base.GetEnumerator();
            //return GetEnumerator();
        }
    }

    public class Sample : HashSet<UserPermission>, IEnumerable<Permission>
    {
        new IEnumerator<Permission> GetEnumerator()
        {
            return null;
        }
        IEnumerator<Permission> IEnumerable<Permission>.GetEnumerator()
        {
            return null;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return null; //your logic for the enumerator
        }
    }

    //public static class CustomExtensions
    //{
    //    public static IEnumerable<TResult> Select<TSource, TResulte, TResult>(this CustomList2<TSource, TResulte> source, Func<TResulte, TResult> selector)
    //    {
    //        source.Select(x => x.)
    //    }
    //}
}
