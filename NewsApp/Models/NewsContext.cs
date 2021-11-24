using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewsApp.Models
{
    public class NewsContext : DbContext
    {
        public NewsContext(DbContextOptions<NewsContext> options)
            : base(options)
        {
        }

        public DbSet<Category> Categories  { get; set; }
        public DbSet<News> News { get; set; }
        public DbSet<ContactUs> Contacts { get; set; }
        public DbSet<Teammembers> Teammembers { get; set; }
        

    }
}
