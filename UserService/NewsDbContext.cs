using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserService.Model;

namespace UserService
{
    public class NewsDbContext:DbContext
    {
        public NewsDbContext(DbContextOptions<NewsDbContext> options):base(options)
        {

        }
        public DbSet<UserAccount> UserAccounts { get; set; }
    }
}
