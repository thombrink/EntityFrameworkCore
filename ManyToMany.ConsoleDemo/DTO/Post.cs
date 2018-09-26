using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ManyToMany.ConsoleDemo.DTO
{
    public class Post
    {
        public string Title { get; set; }

        public IEnumerable<Tag> Tags { get; set; }
    }
}
