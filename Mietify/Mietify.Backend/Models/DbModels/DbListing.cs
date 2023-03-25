namespace Mietify.Backend.Models.DbModels;

public class DbListing
{
    public int Id { get; set; }
   
    public string Title { get; set; }  = null!;
    public string Description { get; set; } = null!;
    public string? HouseNumber { get; set; } = null!;
    public DbDistrict? District { get; set; }
    public string? DoorNumber { get; set; } = null!;
    public string? Stairs { get; set; } = null!;
    public double Price { get; set; }
    public double Area { get; set; }
}