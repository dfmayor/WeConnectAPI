using AutoMapper;
using WeConnectAPI.DTOs;
using WeConnectAPI.Models;
using WeConnectAPI.Models.UserModels;

namespace WeConnectAPI.Helper
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<UserProfileDto, UserProfile>();
            CreateMap<GigModelDto, GigModel>();
        }
    }
}
