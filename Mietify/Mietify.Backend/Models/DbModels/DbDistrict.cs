using Mietify.Backend.Models.WebDto;

namespace Mietify.Backend.Models.DbModels;

public class DbDistrict
{
    public int Id { get; set; }
    
    public double AveragePrice { get; set; }
    
    public string PostalCode { get; set; }
    
    public string Name { get; set; }
    
    public string City { get; set; }
    
    public IList<DbListing> Listings { get; set; }

}