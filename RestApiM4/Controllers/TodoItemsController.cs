using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestApiM4.Data;
using RestApiM4.Models;
using System.Threading.Tasks;

namespace RestApiM4.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class TodoItemsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public TodoItemsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/TodoItems/complete
        /// <summary>
        /// Get completed todo items
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// GET /api/TodoItems/complete
        /// </remarks>
        /// <returns></returns>
        [HttpGet]
        [Route("complete")]
        public async Task<List<TodoItem>> GetCompleteTodoItems()
        {
            return await _context.TodoItems.Where(x => x.IsComplete).ToListAsync();
        }

        // GET: api/TodoItems
        [HttpGet]
        public async Task<List<TodoItem>> GetTodoItems()
        {
            return await _context.TodoItems.ToListAsync();
        }

        // GET: api/TodoItems/{id}
        // GET: api/TodoItems/1
        [HttpGet("{id}")]
        public async Task<ActionResult<TodoItem>> GetTodoItem(int id)
        {
            var todoItem = await _context.TodoItems.FindAsync(id);

            if (todoItem == null)
                return NotFound();

            return todoItem;
        }

        // POST: api/TodoItems
        /// body:
        /// { "id": 0, "name": "Task 3","isComplete": false }

        [HttpPost]
        public async Task<ActionResult<TodoItem>> PostTodoItem(TodoItem todoItem)
        {
            _context.TodoItems.Add(todoItem);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetTodoItem), new { id = todoItem.Id }, todoItem);
        }

        // POST: api/TodoItems/3
        /// body:
        /// { "id": 3, "name": "Task 4","isComplete": true } <summary>
        /// body:
        /// </summary>
        /// <param name="id"></param>
        /// <param name="todoItem"></param>
        /// <returns></returns>
        /// <response code="200">Return when put item</response>
        /// <response code="404">When item didn't find</response>
        /// <response code="400">todoItem.Id != id</response>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> PutTodoItem(int id, TodoItem todoItem)
        {
            if (id != todoItem.Id)
                return BadRequest();

            if (!(await _context.TodoItems.AnyAsync(x => x.Id == id)))
                return NotFound();

            _context.TodoItems.Update(todoItem);
            await _context.SaveChangesAsync();

            return Ok();
        }

        // DELETE: api/TodoItems/1
        [HttpDelete("{id:int}")]
        public async Task<ActionResult> DeleteTodoItem(int id)
        {
            var todoItem = await _context.TodoItems.FindAsync(id);

            if (todoItem == null)
                return NotFound();

            _context.TodoItems.Remove(todoItem);
            await _context.SaveChangesAsync();

            return Ok();
        }
    }
}
