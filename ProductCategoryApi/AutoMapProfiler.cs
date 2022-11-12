using AutoMapper;

using ProductCategoryApi.EntityDto;
using ProductCategoryApi.Models;

namespace ProductCategoryApi
{
    public class AutoMapProfiler : Profile
    {
        public AutoMapProfiler()
        {
            CreateProfile();
        }

        private void CreateProfile()
        {
            CreateMap<CategoryDto, Category>();
            CreateMap<Category, CategoryDto>();            
        }
    }
}
