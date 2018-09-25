﻿using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp1
{
    public class PostTag : IJoinEntity
    {
        public Guid PostId { get; set; }
        public Post Post { get; set; }

        public Guid TagId { get; set; }
        public Tag Tag { get; set; }
    }
}
