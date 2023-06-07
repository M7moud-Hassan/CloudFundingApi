using AutoMapper;
using Core.Identity;
using CroudFundingApi.Dto;

namespace CroudFundingApi.Helper
{
    public class MapProfile:Profile
    {
        public MapProfile()
        {
           // CreateMap<AppUser, UserDto>().ForMember(d => d.DisplayNaem, o => o.MapFrom(s => s.address.FirstName + " " + s.address.LastName));
            //CreateMap<AppUser, RegisterDto>().ForMember(d => d.DisplayName, o => o.MapFrom(s => s.address.FirstName + " " + s.address.LastName));
        }
    }
}
