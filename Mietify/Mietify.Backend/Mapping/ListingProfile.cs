using AutoMapper;
using Mietify.Backend.Models.DbModels;
using Mietify.Backend.Models.WebDto;

namespace Mietify.Backend.Mapping;

public class ListingProfile : Profile
{
    private readonly MietifyDbContext _dbContext;

    public ListingProfile(MietifyDbContext dbContext)
    {
        _dbContext = dbContext;
        CreateMap<DbListing, Listing>()
            .ForMember(dest => dest.District, opt => opt.MapFrom(src => src.District.Name))
            .ForMember(dest => dest.PostalCode, opt => opt.MapFrom(src => src.District.PostalCode));


        CreateMap<Mietify.Protobuf.Messages.Listing, DbListing>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.District, opt => opt.MapFrom<DbDistrict>(src => GetDistrict(dbContext, src)));
    }

    private DbDistrict GetDistrict(MietifyDbContext dbContext, Mietify.Protobuf.Messages.Listing src)
    {
        var district = _dbContext.Districts.SingleOrDefault(d => d.PostalCode == src.Address.PostalCode);
        if (district != null)
        {
            return district;
        }
        else
        {
            var newDistrict = new DbDistrict();
            newDistrict.PostalCode = src.Address.PostalCode;
            newDistrict.City = src.Address.City;

            _dbContext.Districts.Add(newDistrict);
            return newDistrict;
        }
    }
}