using System.ComponentModel.DataAnnotations;

namespace StorageApi.Models.Entities
{
    public class Product
    {
        public int Id { get; set; }

        [StringLength(100, MinimumLength = 3)]
        public string Name { get; set; } = string.Empty;
        public int Price { get; set; }
        public string Category { get; set; } = string.Empty;
        public string? Shelf { get; set; } = string.Empty;
        public int Count { get; set; }
        public string? Description { get; set; } = string.Empty;
    }
}
