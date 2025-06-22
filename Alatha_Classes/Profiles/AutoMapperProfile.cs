using Alatha_Classes.DTO;
using Alatha_Classes.Models;
using AutoMapper;

namespace Alatha_Classes.Profiles
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<DTO_StaffInfo, StaffInfo>().ForMember(x => x.Id, opt => opt.MapFrom(_ => Guid.NewGuid()));
        }
    }
}
