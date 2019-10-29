using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlogCore3.EFCore;
using BlogCore3.Infrastructure;
using BlogCore3.Infrastructure.Interfaces;
using BlogCore3.Models;
using ReflectionIT.Mvc.Paging;

namespace BlogCore3.Services

{

    public interface IBlogPostsService: IAsyncRepository<BlogPosts>

    {
         Task<IEnumerable<BlogPosts>> ReadAll();
        void Create(BlogPosts blogposts);
        Task<BlogPosts> Read(int id);
        void Update(BlogPosts blogposts);
        void Delete(int id);
        IQueryable <BlogPosts> ReadAllNoTrack();
    }
    public class BlogPostsService : EfRepository<BlogPosts>, IBlogPostsService
    {
       
        public BlogPostsService(BlogDbContext context) : base(context) { }
        public void Create(BlogPosts blogposts)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<BlogPosts>  Read(int id)
        {
           return await GetById(id);
        }

        public async Task<IEnumerable<BlogPosts>> ReadAll()
        {
            return await GetAll();
           
        }
        public  IQueryable<BlogPosts> ReadAllNoTrack()
        {
            //var blogposts = await _service.ReadAll();
            return GetAllNoTracking();
            //return await GetAllNoTracking();

        }

        void IBlogPostsService.Update(BlogPosts blogposts)
        {
            Update(blogposts);
            throw new NotImplementedException();
        }
    }
}
