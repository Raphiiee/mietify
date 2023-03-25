using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Mietify.Backend.Models.WebDto;

namespace Mietify.Backend.Controllers;

public class ListingsController
{
    private readonly MietifyDbContext _dbContext;
    private readonly IMapper _mapper;

    public ListingsController(MietifyDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }
    
    [HttpGet]
    public IEnumerable<Listing> GetListingsByDistrict(string postalCode)
    {
        var dbListings = _dbContext.Listings.Where(l => l.District.PostalCode == postalCode).ToList();
        
        var listings = _mapper.Map<List<Listing>>(dbListings);
        
        return listings;
    }
}