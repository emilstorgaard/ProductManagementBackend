namespace ProductManagementBackend.Models.Dtos
{
    public class AddProductDto
    {
        public required string Name { get; set; }

        public string? Description { get; set; }
    }
}
