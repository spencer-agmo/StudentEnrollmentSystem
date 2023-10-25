using AutoMapper;
using StudentEnrollmentSystem.DTO;
using StudentEnrollmentSystem.Models;
using StudentEnrollmentSystem.Models.Auth;

namespace StudentEnrollmentSystem.AutoMapper
{
    public class AutoMapperProfile:Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<User, ProfileDTO>();
            CreateMap<ProfileDTO, User>();
            CreateMap<EnrollmentDTO, Enrollment>();
            CreateMap<CourseDTO, Course>();
            CreateMap<SignUpDTO, User>().ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Email)); ;
        }
    }
}
