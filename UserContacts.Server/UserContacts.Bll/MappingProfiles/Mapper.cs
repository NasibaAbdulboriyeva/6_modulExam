using AutoMapper;

using UserContacts.Bll.Dtos;
using UserContacts.Dal.Entities;

namespace UserContacts.Bll.MappingProfiles
{
    public class Mapper : Profile
    {
        public Mapper()
        {
            CreateMap<UserCreateDto, User>().ReverseMap();
            CreateMap<UserGetDto, User>().ReverseMap();
            CreateMap<ContactCreateDto, Contact>().ReverseMap();
            CreateMap<ContactDto, Contact>().ReverseMap();
            CreateMap<UserRoleCreateDto, UserRole>().ReverseMap();
           
        }
    }
}
