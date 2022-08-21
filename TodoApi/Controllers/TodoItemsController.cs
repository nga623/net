using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System.Dynamic;
 
using TodoApi.Models;

namespace TodoApi.Controllers
{
    public class HttpResponseException : Exception
    {
        public HttpResponseException(int statusCode, object? value = null) =>
            (StatusCode, Value) = (statusCode, value);

        public int StatusCode { get; }

        public object? Value { get; }
    }
    public class HttpResponseExceptionFilter : IActionFilter 
    {
        public int Order => int.MaxValue - 10;

        public void OnActionExecuting(ActionExecutingContext context) { }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            
            if (context.Exception is HttpResponseException httpResponseException)
            {
                context.Result = new ObjectResult(httpResponseException.Value)
                {
                    StatusCode = httpResponseException.StatusCode
                };

                context.ExceptionHandled = true;
            }
        }
    }
    public static class MyAppConventions
    {
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public static void GetTodoItems(int id)
        {
        }
    }
    [Route("api/[controller]")]
    [ApiController]
    [FormatFilter]
    public class TodoItemsController : ControllerBase
    {
        private readonly TodoContext _context;

        
        public TodoItemsController(TodoContext context)
        {
            _context = context;
        }
        
        [HttpPatch]
         
        public IActionResult JsonPatchForDynamic([FromBody] JsonPatchDocument patch)
        {
            dynamic obj = new ExpandoObject();
            patch.ApplyTo(obj);

            return Ok(obj);
        }

        /// <summary>
        /// GetTodoItems
        /// </summary>
        /// <returns></returns>
        [HttpGet]
         
        public async Task<ActionResult<IEnumerable<TodoItemDTO>>> GetTodoItems()
        {
             
            // return Ok();
            return NotFound();
           
            //return await _context.TodoItems
            //    .Select(x => ItemToDTO(x))
            //    .ToListAsync();
            
            //return  JsonResult()
        }

        /// <summary>
        /// GetTodoItem
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
         [HttpGet("{id}")]
        public async Task<ActionResult<TodoItemDTO>> GetTodoItem(  long id)
        {
            
            throw new HttpResponseException(123, "Sample exception.");
            var todoItem = await _context.TodoItems.FindAsync(id);

            if (todoItem == null)
            {
                return NotFound();
            }

            return ItemToDTO(todoItem);
        }
       
        // PUT: api/TodoItems/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        
        public async Task<IActionResult> UpdateTodoItem(long id, TodoItemDTO todoItemDTO)
        {
           
            if (id != todoItemDTO.Id)
            {
                return BadRequest( );
            }
             
            var todoItem = await _context.TodoItems.FindAsync(id);
            if (todoItem == null)
            {
                return NotFound();
            }

            todoItem.Name = todoItemDTO.Name;
            todoItem.IsComplete = todoItemDTO.IsComplete;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException) when (!TodoItemExists(id))
            {
                return NotFound();
            }

            return NoContent();
        }
        /// <summary>
        /// Creates a TodoItem.
        /// </summary>
        /// <param name="todoItemDTO"></param>
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
        public async Task<ActionResult<TodoItemDTO>> CreateTodoItem(TodoItemDTO todoItemDTO)
        {
            var todoItem = new TodoItem
            {
                IsComplete = todoItemDTO.IsComplete,
                Name = todoItemDTO.Name
            };

            _context.TodoItems.Add(todoItem);
            await _context.SaveChangesAsync();

            return CreatedAtAction(
                nameof(GetTodoItem),
                new { id = todoItem.Id },
                ItemToDTO(todoItem));
        }

       
         
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTodoItem(long id)
        {
            var todoItem = await _context.TodoItems.FindAsync(id);

            if (todoItem == null)
            {
                return NotFound();
            }

            _context.TodoItems.Remove(todoItem);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TodoItemExists(long id)
        {
            return _context.TodoItems.Any(e => e.Id == id);
        }

        private static TodoItemDTO ItemToDTO(TodoItem todoItem) =>
            new TodoItemDTO
            {
                Id = todoItem.Id,
                Name = todoItem.Name,
                IsComplete = todoItem.IsComplete
            };
    }
}