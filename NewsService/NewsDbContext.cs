using Microsoft.EntityFrameworkCore;
using NewsService.Model;

namespace NewsService
{
    public class NewsDbContext : DbContext
    {
        public NewsDbContext(DbContextOptions<NewsDbContext> options) : base(options)
        {

        }

        public DbSet<News> News { get; set; }
    }
}
