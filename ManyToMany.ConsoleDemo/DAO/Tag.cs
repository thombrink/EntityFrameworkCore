using Microsoft.EntityFrameworkCore;
using System;

namespace ManyToMany.ConsoleDemo.DAO
{
    public class Tag : Entity
    {
        public int Level { get; set; } = new Random().Next();

        public ManyToManyList<PostTag, Post> Posts { get; set; }

        public Tag()
        {
            Posts = new ManyToManyList<PostTag, Post>(this);
        }
    }
}
