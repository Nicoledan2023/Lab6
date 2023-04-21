using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BlogApi.Models;

namespace BlogApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogController : ControllerBase
    {
        private readonly BlogContext _context;

        public BlogController(BlogContext context)
        {
            _context = context;
        }

        // GET: api/Blog
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BlogModel>>> GetBlogs()
        {
          if (_context.Blogs == null)
          {
              return NotFound();
          }
            return await _context.Blogs.ToListAsync();
        }

        // GET: api/Blog/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BlogModel>> GetBlogModel(uint id)
        {
          if (_context.Blogs == null)
          {
              return NotFound();
          }
            var blogModel = await _context.Blogs.FindAsync(id);

            if (blogModel == null)
            {
                return NotFound();
            }

            return blogModel;
        }

        // PUT: api/Blog/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBlogModel(uint id, BlogModel blogModel)
        {
            if (id != blogModel.BlogModelId)
            {
                return BadRequest();
            }

            _context.Entry(blogModel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BlogModelExists(id))
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

        // POST: api/Blog
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<BlogModel>> PostBlogModel(BlogModel blogModel)
        {
          if (_context.Blogs == null)
          {
              return Problem("Entity set 'BlogContext.Blogs'  is null.");
          }
            _context.Blogs.Add(blogModel);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBlogModel", new { id = blogModel.BlogModelId }, blogModel);
        }

        // DELETE: api/Blog/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBlogModel(uint id)
        {
            if (_context.Blogs == null)
            {
                return NotFound();
            }
            var blogModel = await _context.Blogs.FindAsync(id);
            if (blogModel == null)
            {
                return NotFound();
            }

            _context.Blogs.Remove(blogModel);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool BlogModelExists(uint id)
        {
            return (_context.Blogs?.Any(e => e.BlogModelId == id)).GetValueOrDefault();
        }
    }
}
