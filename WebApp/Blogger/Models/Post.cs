﻿using System;
using System.Collections.Generic;

namespace Blogger.Models
{
    public partial class Post
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? Author { get; set; }
        public string? PostedOn { get; set; }
    }
}
