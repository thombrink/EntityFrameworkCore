using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp1
{
    public class UserPermission : IJoinEntity
    {
        public Guid UserId { get; set; }
        public User User { get; set; }

        public Guid PermissionId { get; set; }
        public Permission Permission { get; set; }
    }
}
