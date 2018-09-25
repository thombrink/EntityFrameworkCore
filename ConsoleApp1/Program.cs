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
            Console.WriteLine("Getting things ready!");

            var context = new TestContext();

            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            var newPost = new Post();
            context.Posts.Add(newPost);

            var newTag1 = new Tag();
            context.Tags.Add(newTag1);

            var newTag2 = new Tag();
            context.Tags.Add(newTag2);

            // save all entries to the database
            context.SaveChanges();

            // get all existing permissions from the database and add them to the new post
            foreach (var permisson in context.Tags)
            {
                newPost.Tags.Add(permisson);
            }

            context.SaveChanges();

            // get the new post from the database and include the permissions
            var dbPost = context.Posts.Include(x => x.Tags).First(x => x.Id == newPost.Id);

            Console.WriteLine($"All tag levels for post '{dbPost.Title}'");

            foreach (var permission in dbPost.Tags)
            {
                Console.WriteLine(permission.Level);
            }

            Console.WriteLine(Environment.NewLine);

            // get the first tag from the database and include the posts
            var dbTag = context.Tags.Include(x => x.Posts).First();

            Console.WriteLine($"All post titles for tag level '{dbTag.Level}'");

            foreach (var post in dbTag.Posts)
            {
                Console.WriteLine(post.Title);
            }

            Console.WriteLine(Environment.NewLine);

            var query = context.Posts.Include(x => x.Tags).Select(x => new Post
            {
                Title = x.Title,
                Tags = new ManyToManyList<PostTag, Tag>(x, x.Tags.Select(y => new Tag
                {
                    Level = y.Level + 10000
                }))
            });

            IEnumerable<string> test = dbTag.Posts.Select(x => x.Title);

            Console.WriteLine("Done! Press a key to exit");

            Console.ReadKey(true);
        }
    }
}
