namespace Mietify.Backend.Models.WebDto;

public class Listing
{
    public int Id { get; set; }
   
    public string Title { get; set; }  = null!;
    public string? HouseNumber { get; set; } = null!;
    public string? District { get; set; }
    public string? PostalCode { get; set; }
    public string? DoorNumber { get; set; } = null!;
    public string? Stairs { get; set; } = null!;
    public double Price { get; set; }
    public double Area { get; set; }
}