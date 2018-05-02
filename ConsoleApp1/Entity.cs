using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ConsoleApp1
{
    public class Entity
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
    }
}
