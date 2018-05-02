using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp1
{
    public class CustomList1 : List<UserPermission>, IList<User>
    {
        User IList<User>.this[int index] { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public bool IsReadOnly => throw new NotImplementedException();

        public void Add(User item)
        {
            throw new NotImplementedException();
        }

        public bool Contains(User item)
        {
            throw new NotImplementedException();
        }

        public void CopyTo(User[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }

        public int IndexOf(User item)
        {
            throw new NotImplementedException();
        }

        public void Insert(int index, User item)
        {
            throw new NotImplementedException();
        }

        public bool Remove(User item)
        {
            throw new NotImplementedException();
        }

        IEnumerator<User> IEnumerable<User>.GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}
