using Microsoft.AspNetCore.Mvc;
using TodoApi.DTOs;
using TodoApi.Interfaces;

namespace TodoApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class TodoController : ControllerBase
    {
        private readonly ITodoService _todoService;
        private readonly ILogger<TodoController> _logger;

        public TodoController(ITodoService todoService, ILogger<TodoController> logger)
        {
            _todoService = todoService;
            _logger = logger;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<TodoDto>>> GetAllTodos()
        {
            try
            {
                var todos = await _todoService.GetAllTodosAsync();
                return Ok(todos);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting all todos");
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while retrieving todos");
            }
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<TodoDto>> GetTodoById(int id)
        {
            try
            {
                var todo = await _todoService.GetTodoByIdAsync(id);
                if (todo == null)
                {
                    return NotFound(new { message = $"Todo with ID {id} not found" });
                }
                return Ok(todo);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting todo with ID {TodoId}", id);
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while retrieving the todo");
            }
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<TodoDto>> CreateTodo([FromBody] CreateTodoDto createTodoDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var createdTodo = await _todoService.CreateTodoAsync(createTodoDto);
                return CreatedAtAction(nameof(GetTodoById), new { id = createdTodo.Id }, createdTodo);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating todo");
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while creating the todo");
            }
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<TodoDto>> UpdateTodo(int id, [FromBody] UpdateTodoDto updateTodoDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var updatedTodo = await _todoService.UpdateTodoAsync(id, updateTodoDto);
                if (updatedTodo == null)
                {
                    return NotFound(new { message = $"Todo with ID {id} not found" });
                }
                return Ok(updatedTodo);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating todo with ID {TodoId}", id);
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while updating the todo");
            }
        }

        [HttpPatch("{id}/toggle")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<TodoDto>> ToggleTodo(int id)
        {
            try
            {
                var updatedTodo = await _todoService.ToggleTodoAsync(id);
                if (updatedTodo == null)
                {
                    return NotFound(new { message = $"Todo with ID {id} not found" });
                }
                return Ok(updatedTodo);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error toggling todo with ID {TodoId}", id);
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while toggling the todo");
            }
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> DeleteTodo(int id)
        {
            try
            {
                var result = await _todoService.DeleteTodoAsync(id);
                if (!result)
                {
                    return NotFound(new { message = $"Todo with ID {id} not found" });
                }
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting todo with ID {TodoId}", id);
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while deleting the todo");
            }
        }
    }
}
