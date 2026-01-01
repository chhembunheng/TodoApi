using TodoApi.DTOs;
using TodoApi.Interfaces;
using TodoApi.Model;

namespace TodoApi.Services
{
    public class TodoService
    {
        private readonly ITodoRepository _todoRepository;

        public TodoService(ITodoRepository todoRepository)
        {
            _todoRepository = todoRepository;
        }

        public async Task<IEnumerable<TodoDto>> GetAllTodosAsync()
        {
            var todos = await _todoRepository.GetAllAsync();
            return todos.Select(MapToDto);
        }

        public async Task<TodoDto?> GetTodoByIdAsync(int id)
        {
            var todo = await _todoRepository.GetByIdAsync(id);
            return todo != null ? MapToDto(todo) : null;
        }

        public async Task<TodoDto> CreateTodoAsync(CreateTodoDto createTodoDto)
        {
            var todo = new Todo
            {
                Title = createTodoDto.Title,
                IsCompleted = createTodoDto.IsCompleted
            };

            var createdTodo = await _todoRepository.CreateAsync(todo);
            return MapToDto(createdTodo);
        }

        public async Task<TodoDto?> UpdateTodoAsync(int id, UpdateTodoDto updateTodoDto)
        {
            var todo = new Todo
            {
                Title = updateTodoDto.Title,
                IsCompleted = updateTodoDto.IsCompleted
            };

            var updatedTodo = await _todoRepository.UpdateAsync(id, todo);
            return updatedTodo != null ? MapToDto(updatedTodo) : null;
        }

        public async Task<TodoDto?> ToggleTodoAsync(int id)
        {
            var todo = await _todoRepository.GetByIdAsync(id);
            if (todo == null)
            {
                return null;
            }

            todo.IsCompleted = !todo.IsCompleted;
            var updatedTodo = await _todoRepository.UpdateAsync(id, todo);
            return updatedTodo != null ? MapToDto(updatedTodo) : null;
        }

        public async Task<bool> DeleteTodoAsync(int id)
        {
            return await _todoRepository.DeleteAsync(id);
        }

        private static TodoDto MapToDto(Todo todo)
        {
            return new TodoDto
            {
                Id = todo.Id,
                Title = todo.Title,
                IsCompleted = todo.IsCompleted,
                CreatedAt = todo.CreatedAt,
                UpdatedAt = todo.UpdatedAt
            };
        }
    }
}