using System;
using System.Collections.Generic;
using System.Linq;

using Microsoft.EntityFrameworkCore;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            var context = new TestContext();

            context.Database.EnsureCreated();

            var user = new User();
            context.Users.Add(user);

            var permission = new Permission();
            context.Permissions.Add(permission);

            context.UserPermissions.Add(new UserPermission { UserId = user.Id, PermissionId = permission.Id });

            //context.Users.Include()
            //Extensions.IncludeJoin(context.Users, x => x.Permissions);

            context.SaveChanges();

            //var sw = new System.Diagnostics.Stopwatch();
            //sw.Start();
            //var test1 = new List<UserPermission>();
            //for (var i = 0; i < 100000; i++)
            //{
            //    test1.Add(new UserPermission { Permission = new Permission() });
            //}

            //foreach (var t in test1)
            //{
            //    Console.Write("\r" + t.Permission.Level);
            //}

            //Console.Write("\r" + test1[234].Permission.Level);

            //Console.Write(Environment.NewLine);

            //sw.Stop();

            //Console.WriteLine("Testrun 1 TOTOAL " + sw.Elapsed.TotalSeconds + " AVG " + sw.Elapsed.TotalSeconds / 1000);

            //sw.Reset();

            //sw.Start();
            //var dbUser = context.Users.First();

            ////IList<Permission> test2 = new CustomList2<UserPermission, Permission>(Guid.NewGuid());
            IList<Permission> test2 = user.Permissions;
            for (var i = 0; i < 10; i++)//100000
            {
                test2.Add(permission);
            }

            var permission2 = new Permission();
            context.Permissions.Add(permission2);

            test2.Add(permission2);

            context.SaveChanges();

            var dbUser = context.Users.Include(x => x.Permissions).First();
            //var dbUser = context.Users.First();
            test2 = dbUser.Permissions;

            foreach (var t in test2)
            {
                //Console.Write("\r" + t.Level);
                Console.WriteLine(t.Level);
            }

            //Console.Write("\r" + test2[234].Level);

            Console.Write(Environment.NewLine);

            //sw.Stop();

            //Console.Write("Testrun 2 TOTOAL " + sw.Elapsed.TotalSeconds + " AVG " + sw.Elapsed.TotalSeconds / 1000);

            Console.ReadKey(true);
        }
    }
}
