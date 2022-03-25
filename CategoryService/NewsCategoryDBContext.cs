using CategoryService.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CategoryService
{
    public class NewsCategoryDBContext : DbContext
    {
        public NewsCategoryDBContext(DbContextOptions<NewsCategoryDBContext> options) : base(options)
        {

        }

        public DbSet<Category> Categories { get; set; }
    }
}
