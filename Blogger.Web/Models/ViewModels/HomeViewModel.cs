﻿using Blogger.Web.Models.Domain;

namespace Blogger.Web.Models.ViewModels
{
    public class HomeViewModel
    {
        public IEnumerable<BlogPost> BlogPosts { get; set; }
        public IEnumerable<Tags> Tags { get; set; }
    }
}
