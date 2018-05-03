using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ConsoleApp1
{
    public class User : Entity
    {
        public string Name { get; set; } = "Test" + new Random().Next();

        //public ICollection<UserPermission> UserPermissions { get; set; }
        public ManyToManyList<UserPermission, Permission> Permissions { get; set; }

        public User()
        {
            Permissions = new ManyToManyList<UserPermission, Permission>(this);
        }
    }
}
