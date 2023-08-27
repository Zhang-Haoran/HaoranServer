using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HaoranServer.Context;
using HaoranServer.Models;

namespace HaoranServer.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    // 注意这里controller的单复数s 会影响 swagger里 路径显示
    public class CommentController : ControllerBase
    {
        private readonly CommentContext _context;

        public CommentController(CommentContext context)
        {
            _context = context;
        }

        // GET: api/Comment
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Comment>>> Getcomments()
        {
          if (_context.comment == null)
          {
              return NotFound();
          }
            return await _context.comment.ToListAsync();
        }

        // GET: api/Comment/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Comment>> GetComment(int id)
        {
          if (_context.comment == null)
          {
              return NotFound();
          }
            var comment = await _context.comment.FindAsync(id);

            if (comment == null)
            {
                return NotFound();
            }

            return comment;
        }

        // PUT: api/Comment/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutComment(int id, Comment comment)
        {
            if (id != comment.Id)
            {
                return BadRequest();
            }
            comment.UpdatedTime = DateTime.Now.ToString();
            _context.Entry(comment).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CommentExists(id))
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

        // POST: api/Comment
        [HttpPost]
        public async Task<ActionResult<Comment>> PostComment(Comment comment)
        {
          if (_context.comment == null)
          {
              return Problem("Entity set 'CommentContext.comments'  is null.");
          }
            comment.CreatedTime = DateTime.Now.ToString();
            comment.UpdatedTime = DateTime.Now.ToString();
            _context.comment.Add(comment);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetComment", new { id = comment.Id }, comment);
        }

        // DELETE: api/Comment/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteComment(int id)
        {
            if (_context.comment == null)
            {
                return NotFound();
            }
            var comment = await _context.comment.FindAsync(id);
            if (comment == null)
            {
                return NotFound();
            }

            _context.comment.Remove(comment);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CommentExists(int id)
        {
            return (_context.comment?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
