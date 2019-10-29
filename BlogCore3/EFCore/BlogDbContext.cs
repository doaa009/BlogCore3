using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using BlogCore3.Models;


namespace BlogCore3.EFCore
{
    public class BlogDbContext:DbContext
    {
        public BlogDbContext(DbContextOptions<BlogDbContext> options)
            : base(options)
        {
        }
        public DbSet<BlogPosts> BlogPosts { get; set; }
        public DbSet<TodoItem> TodoItems { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BlogPosts>()
                .Property(b => b.ImageUrl)
                .IsRequired();
            #region BlogSeed
            modelBuilder.Entity<BlogPosts>().HasData(new BlogPosts { BlogPostsId = 1, ImageUrl = "https://via.placeholder.com/150", Title = "Title1",Subtitle ="sub1" });
            modelBuilder.Entity<BlogPosts>().HasData(new BlogPosts { BlogPostsId = 2, ImageUrl = "https://via.placeholder.com/150", Title = "Title1q", Subtitle = "sub1q" });
            modelBuilder.Entity<BlogPosts>().HasData(new BlogPosts { BlogPostsId = 3, ImageUrl = "https://via.placeholder.com/150", Title = "Title2", Subtitle = "sub2" });
            modelBuilder.Entity<BlogPosts>().HasData(new BlogPosts { BlogPostsId = 4, ImageUrl = "https://via.placeholder.com/150", Title = "Title3", Subtitle = "sub3" });

            #endregion
            #region OwnedTypeSeed
            modelBuilder.Entity<BlogPosts>().OwnsOne(p => p.Author).HasData(
                new { BlogPostsId = 1, First = "Doaa", Last = "Hashim" },
                new { BlogPostsId = 2, First = "Deena", Last = "Hashim" },
                new { BlogPostsId = 3, First = "Emy", Last = "Hashim" },
                new { BlogPostsId = 4, First = "Noor", Last = "Hashim" });
            #endregion
        }
    }

}
