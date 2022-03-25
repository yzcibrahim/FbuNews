using System;

namespace NewsService.Dtos
{
    public class NewsDtos
    {
        public int Id { get; set; }

        public int CategoryId { get; set; }

        public int CreateUserId { get; set; }

        public string Title { get; set; }

        public string Brief { get; set; }

        public string Content { get; set; }

        public string ImagePath { get; set; }

        public int ReadCount { get; set; }
    }
}
