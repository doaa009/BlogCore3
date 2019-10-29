using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BlogCore3.EFCore;
using BlogCore3.Models;
using BlogCore3.Services;
using BlogCore3.Hubs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using ReflectionIT.Mvc.Paging;

namespace BlogCore3.Controllers
{
   [Authorize]
    [Route("api/[controller]/[action]")]
  //[ApiController]
    public class BlogPostsController : ControllerBase
    {
        private readonly BlogDbContext _context;
        private readonly IBlogPostsService _service;

        private readonly IHubContext<NotficationHub> _notficationHubContext;
        public BlogPostsController(BlogDbContext context , IBlogPostsService service, IHubContext<NotficationHub> notficationHubContext)
        {
            _context = context;
            _service = service;;
            _notficationHubContext = notficationHubContext;
        }
        /// <summary>
        /// Display all   posts.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     Get all/Blog posts
        ///
        ///
        /// </remarks>
        /// <param name="item"></param>
        /// <returns>A newly created Post</returns>
        /// <response code="201">Returns the newly created item</response>
        /// <response code="400">If the item is null</response>        
        // GET: api/BlogPosts
        
        [HttpGet]
        [Produces("application/json")]
        public async Task<ActionResult<IEnumerable<BlogPosts>>> GetBlogPosts()
        {
           
            var blogposts = await _service.ReadAll();
           
            return Ok(blogposts);

        }
        [HttpGet()]
        [Produces("application/json")]
        public async Task<ActionResult<IEnumerable<BlogPosts>>> GetBlogPostsIndex(int? pageIndex)
        {

            var blogposts = await PagingList.CreateAsync(_service.ReadAllNoTrack().OrderBy(a => a.BlogPostsId), 2, pageIndex ?? 1);
            ;

            return Ok(blogposts);

        }
        // GET: api/BlogPosts/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BlogPosts>> GetBlogPosts(int id)
        {
            var blogPosts = await _service.Read(id);

            if (blogPosts == null)
            {
                return NotFound();
            }

            return blogPosts;
        }

        // PUT: api/BlogPosts/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBlogPosts(int id, BlogPosts blogPosts)
        {
            if (id != blogPosts.BlogPostsId)
            {
                return BadRequest();
            }

            _context.Entry(blogPosts).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BlogPostsExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }
        /// <summary>
        /// Creates a blogPosts.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /blogPosts
        ///      {
        ///        "blogPostsId": 0,
        ///          "author": {
        ///        "first": "string",
        ///        "last": "string"
        ///           },
        ///     "title": "string",
        ///      "subtitle": "string",
        ///       "date": "2019-10-26T20:04:41.857Z",
        ///      "imageUrl": "string",
        ///       "body": "string"
        ///               }
        ///
        /// </remarks>
        /// <param name="blogPosts"></param>
        /// <returns>A newly created TodoItem</returns>
        /// <response code="201">Returns the newly created item</response>
        /// <response code="400">If the item is null</response>            
        // POST: api/BlogPosts
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]

        [Produces("application/json")]
        public async Task<ActionResult<BlogPosts>> PostBlogPosts(BlogPosts blogPosts)
        {
            blogPosts.Date=DateTime.Now;
            _context.BlogPosts.Add(blogPosts);
            await _context.SaveChangesAsync();
            await _notficationHubContext.Clients.All.SendAsync("ReceiveMessage", blogPosts.Author.First, blogPosts.Author.Last, blogPosts.Title);
            return CreatedAtAction(nameof(GetBlogPosts), new { id = blogPosts.BlogPostsId }, blogPosts);
        }

        // DELETE: api/BlogPosts/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<BlogPosts>> DeleteBlogPosts(int id)
        {
            var blogPosts = await _context.BlogPosts.FindAsync(id);
            if (blogPosts == null)
            {
                return NotFound();
            }

            _context.BlogPosts.Remove(blogPosts);
            await _context.SaveChangesAsync();

            return blogPosts;
        }

        private bool BlogPostsExists(int id)
        {
            return _context.BlogPosts.Any(e => e.BlogPostsId == id);
        }
    }
}
