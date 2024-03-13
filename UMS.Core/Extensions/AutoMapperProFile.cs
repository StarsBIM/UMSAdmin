using AutoMapper;
using Newtonsoft.Json;
using UMS.Core.DB.Entities;
using UMS.Core.DTO;

namespace UMS.Core.Extensions
{
    public class AutoMapperProFile : Profile
    {
        public AutoMapperProFile()
        {
            CreateMap<UserEntity, UserDTO>()
                 .ForMember(dest => dest.RoleIds, opt => opt.MapFrom(src => (from m in src.Roles select m.Id)))
                .ForMember(dest => dest.CreateDataTime, opt => opt.MapFrom(src => src.CreateDateTime))
             .ForMember(dest => dest.City, opt => opt.MapFrom(src => JsonConvert.DeserializeObject(src.City)));
            CreateMap<RoleEntity, RoleDTO>()
                .ForMember(dest => dest.MenuIds, opt => opt.MapFrom(src => (from m in src.Menus select m.Id)))
                .ForMember(dest => dest.CreateDataTime, opt => opt.MapFrom(src => src.CreateDateTime));
            CreateMap<MenuEntity, MenuDTO>()
                     .ForMember(dest => dest.PaterName, opt => opt.MapFrom(src => src.Pater.Name))
                .ForMember(dest => dest.CreateDataTime, opt => opt.MapFrom(src => src.CreateDateTime));
            CreateMap<AdminUserEntity, AdminUserDTO>()
                .ForMember(dest => dest.Roles, opt => opt.MapFrom(src => src.Roles))
                .ForMember(dest => dest.CreateDataTime, opt => opt.MapFrom(src => src.CreateDateTime))
             .ForMember(dest => dest.City, opt => opt.MapFrom(src => JsonConvert.DeserializeObject(src.City)));
            CreateMap<AdminLogEntity, AdminLogDTO>();
        }
    }
}
