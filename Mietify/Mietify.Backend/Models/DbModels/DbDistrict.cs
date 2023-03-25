namespace Mietify.Backend.Models.DbModels;

public class DbDistrict
{
    public int Id { get; set; }
    
    public double AveragePrice { get; set; }
    
    public string PostalCode { get; set; }
    
    public string Name { get; set; }
    
    public DbCity City { get; set; }
    
}