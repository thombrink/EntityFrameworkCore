using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp1
{
    public class UserPermission : IJoinEntity
    {
        public Guid UserId { get; set; }
        public User User { get; set; }
        //User IJoinEntity<User>.Navigation { get => User; set => User = value; }

        public Guid PermissionId { get; set; }
        public Permission Permission { get; set; }
        //Permission IJoinEntity<Permission>.Navigation { get => Permission; set => Permission = value; }
    }
}
