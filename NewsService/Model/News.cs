using System;

namespace NewsService.Model
{
    public class News
    {
        public int Id { get; set; }

        public int CategoryId { get; set; }

        public int CreateUserId { get; set; }

        public string Title { get; set; }

        public string Brief { get; set; }

        public string Content { get; set; }

        public bool IsActive { get; set; }

        public bool IsShowOnSlide { get; set; }

        public string ImagePath { get; set; }

        public int ReadCount { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime ModifiedDate { get; set; }
    }
}
