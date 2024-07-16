namespace ProductManagementBackend.Models.Entities
{
    public class Product
    {
        public Guid Id { get; set; }

        public required string Name { get; set; }

        public string? Description { get; set; }

        public DateTime CreatedAt { get; set; }

        public Product()
        {
            CreatedAt = DateTime.UtcNow;
        }
    }
}
