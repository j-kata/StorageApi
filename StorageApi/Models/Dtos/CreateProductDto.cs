using System.ComponentModel.DataAnnotations;

namespace StorageApi.Models.Dtos
{
    public class CreateProductDto
    {
        [Required]
        [StringLength(100, MinimumLength = 3)]
        public string Name { get; set; } = string.Empty;

        [Range(0, 100000)]
        public int Price { get; set; }

        [Required]
        public string Category { get; set; } = string.Empty;
        public string Shelf { get; set; } = string.Empty;

        [Range(0, 10000)]
        public int Count { get; set; }
        public string Description { get; set; } = string.Empty;
    }
}
