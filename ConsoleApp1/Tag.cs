using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp1
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
