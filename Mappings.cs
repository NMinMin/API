using AutoMapper;
using API.Dtos;
using API.Entity;
using API.Models;

namespace API.Mappings
{
     public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Map Student qua StudentDto
            CreateMap<Student, StudentDto>()
                .ForMember(dest => dest.class_name,// để mapp thuộc tính class_name
                           opt => opt.MapFrom(src => src._class.name))// ánh xạ class_name từ src qua dest _class.name
                .ForMember(dest => dest.dateofbirth,
                           opt => opt.MapFrom(src => src.dateofbirth.ToString("dd/MM/yyyy")));//định dạng lại ngày tháng
             CreateMap<StudentModel, Student>();

            CreateMap<CreateStudentDto, Student>();//Khi tạo Student thì tự đổng chuyển thành Student và gửi vào DB

            CreateMap<Class, ClassDto>()
                .ForMember(dest => dest.students,
                           opt => opt.MapFrom(src => src.lst_student.Select(s => s.name).ToList()));
        }
    }
}