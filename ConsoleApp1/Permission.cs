using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp1
{
    public class Permission : Entity
    {
        public int Level { get; set; } = new Random().Next();

        //public ManyToManyList<UserPermission, User> Users { get; set; }
    }
}
