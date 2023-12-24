using System.ComponentModel.DataAnnotations;

namespace carvedrock_admin.Data;

public class Category
{
    public int Id { get; set; }
    [Required]
    public string Name { get; set; } = null!;
}