using TodoApi.Model;

namespace TodoApi.Interfaces
{
    public interface ITodoRepository
    {
        Task<IEnumerable<Todo>> GetAllAsync();
        Task<Todo?> GetByIdAsync(int id);
        Task<Todo> CreateAsync(Todo todo);
        Task<Todo?> UpdateAsync(int id, Todo todo);
        Task<bool> DeleteAsync(int id);
        Task<bool> ExistsAsync(int id);
    }
}