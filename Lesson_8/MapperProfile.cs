using Lesson_8.Models;
using Lesson_8.ViewModels;
using AutoMapper;

namespace Lesson_8
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<Employee, EmployeeViewModel>();
            CreateMap<EmployeeViewModel, Employee>();
        }
    }
}
