using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp1
{
    public class Permission : Entity
    {
        public int Level { get; set; } = new Random().Next();

        //public ICollection<UserPermission> UserPermissions { get; set; }
        //public CustomList2<UserPermission, User> Users { get; set; }
    }
}
