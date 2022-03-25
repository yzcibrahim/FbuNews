using CommentsService.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CommentsService
{
    public class CommentsDbContext:DbContext
    {
        public CommentsDbContext(DbContextOptions<CommentsDbContext> options) : base(options)
        {

        }
        public DbSet<Comment> Comments { get; set; }
    }
}
