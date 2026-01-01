using System.ComponentModel.DataAnnotations;

namespace TodoApi.Model
{
    public class Todo
    {
        [Key]
        public int Id { get; set; }
        
        [Required(ErrorMessage = "Title is required")]
        [StringLength(200, MinimumLength = 1)]
        public string Title { get; set; } = string.Empty;
        
        [Required]
        public bool IsCompleted { get; set; } = false;
        
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        
        public DateTime? UpdatedAt { get; set; }
    }
}