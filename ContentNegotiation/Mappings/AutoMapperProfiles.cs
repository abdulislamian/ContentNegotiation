using AutoMapper;
using ContentNegotiation.Models;
using ContentNegotiation.Models.DTO;

namespace JWTAuthentication.Mapping
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Student, StudentDTO>().ReverseMap();
            CreateMap<AddStudentRequestDto,Student>().ReverseMap();
            CreateMap<UpdateStudentRequestDTO, Student>().ReverseMap();
        }
    }
}
