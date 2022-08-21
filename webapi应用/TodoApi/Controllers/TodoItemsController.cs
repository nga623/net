using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodoApi.Data;
using TodoApi.Models;

namespace TodoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoItemsController : ControllerBase
    {
        private readonly TodoApiContext _context;

        public TodoItemsController(TodoApiContext context)
        {
            _context = context;
        }
        /// <summary>
        /// Creates a TodoItem.
        /// </summary>
        /// <param name="item"></param>
        /// <returns>A newly created TodoItem</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /Todo
        ///     {
        ///        "id": 1,
        ///        "name": "Item #1",
        ///        "isComplete": true
        ///     }
        ///
        /// </remarks>
        /// <response code="201">Returns the newly created item</response>
        /// <response code="400">If the item is null</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create(TodoItem item)
        {
            _context.TodoItem.Add(item);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(Create), new { id = item.Id }, item);
        }
        // GET: api/TodoItems
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TodoItem>>> GetTodoItem()
        {
          if (_context.TodoItem == null)
          {
              return NotFound();
          }
            return await _context.TodoItem.ToListAsync();
        }

        // GET: api/TodoItems/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TodoItem>> GetTodoItem(long id)
        {
          if (_context.TodoItem == null)
          {
              return NotFound();
          }
            var todoItem = await _context.TodoItem.FindAsync(id);

            if (todoItem == null)
            {
                return NotFound();
            }

            return todoItem;
        }

        // PUT: api/TodoItems/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTodoItem(long id, TodoItem todoItem)
        {
            if (id != todoItem.Id)
            {
                return BadRequest();
            }

            _context.Entry(todoItem).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TodoItemExists(id))
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

        //// POST: api/TodoItems
        //// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPost]
        //public async Task<ActionResult<TodoItem>> PostTodoItem(TodoItem todoItem)
        //{
        //  if (_context.TodoItem == null)
        //  {
        //      return Problem("Entity set 'TodoApiContext.TodoItem'  is null.");
        //  }
        //    _context.TodoItem.Add(todoItem);
        //    await _context.SaveChangesAsync();

        //    return CreatedAtAction("GetTodoItem", new { id = todoItem.Id }, todoItem);
        //}

        // DELETE: api/TodoItems/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTodoItem(long id)
        {
            if (_context.TodoItem == null)
            {
                return NotFound();
            }
            var todoItem = await _context.TodoItem.FindAsync(id);
            if (todoItem == null)
            {
                return NotFound();
            }

            _context.TodoItem.Remove(todoItem);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TodoItemExists(long id)
        {
            return (_context.TodoItem?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
