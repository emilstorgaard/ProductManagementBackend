namespace ProductManagementBackend.Models.Dtos
{
    public class UpdateProductDto
    {
        public required string Name { get; set; }

        public string? Description { get; set; }
    }
}
