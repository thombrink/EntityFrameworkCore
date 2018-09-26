using Microsoft.EntityFrameworkCore;
using System;

namespace ManyToMany.ConsoleDemo.DAO
{
    public class Post : Entity
    {
        public string Title { get; set; } = "Test" + new Random().Next();

        public ManyToManyList<PostTag, Tag> Tags { get; set; }

        public Post()
        {
            Tags = new ManyToManyList<PostTag, Tag>(this);
        }
    }
}
