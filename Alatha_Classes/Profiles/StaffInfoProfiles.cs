using Alatha_Classes.DTO;
using Alatha_Classes.Models;
using AutoMapper;

namespace Alatha_Classes.Profiles
{
    public class StaffInfoProfiles : Profile
    {
        public StaffInfoProfiles()
        {
            CreateMap<DTO_StaffInfo, StaffInfo>().ForMember(x => x.Id, opt => opt.MapFrom(_ => Guid.NewGuid()));
        }
    }
}
