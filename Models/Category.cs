using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CommunityToolkit.Mvvm.ComponentModel;

namespace Campus.Models;

public partial class Category : ObservableObject
{
    [Key]
    public int CategoryId { get; set; }

    [Required]
    [MaxLength(100)]
    public string CategoryName { get; set; } = string.Empty;

    [MaxLength(500)]
    public string? CategoryDescription { get; set; }

    public string Color { get; set; } = "#0078D4";

    public string Icon { get; set; } = "📌";

    public CategoryType CategoryType { get; set; }

    public bool IsActive { get; set; } = true;

    public DateTime CreatedDate { get; set; } = DateTime.Now;

    public DateTime UpdatedDate { get; set; } = DateTime.Now;
}
