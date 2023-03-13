﻿using PostService.Model;

namespace PostComment.Model
{
    public class BlogComment
    {
        public int Id { get; set; }
        public int BlogPostId { get; set; }
        public string Title { get; set; }
        public string Comment { get; set; }
        public bool IsPublished { get; set; }
        public DateTime PublishedOn { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime UpdatedOn { get; set; }
        public BlogPost BlogPost { get; set; }
    }
}
