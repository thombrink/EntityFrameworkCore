using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ConsoleApp1
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
