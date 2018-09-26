using System;
using System.ComponentModel.DataAnnotations;

namespace Microsoft.EntityFrameworkCore
{
    public class Entity : Entity<Guid>
    {
    }

    public class Entity<T>
    {
        [Key]
        public T Id { get; set; }
    }
}
