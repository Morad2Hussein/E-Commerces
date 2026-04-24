


public class ProductResultDto
{
    public int Id { get; set; }
    public string Name { get; set; } = default!;
    public decimal Price { get; set; }
    public string? Description { get; set; }
    public string? PictureUrl { get; set; }
    public string? TypeName { get; set; }
    public string? BrandName { get; set; }
}