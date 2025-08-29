using SampleMvcproject.Application.DTOs;
using SampleMvcproject.Domain.Entities;
using SampleMvcProject.Application.DTOs;
using SampleMvcProject.Domain.Entities;

namespace SampleMvcProject.Application.Mappers
{
    public static class ProductMapper
    {
        public static Product ToEntity(this ProductDto productDto)
        {
            return new Product
            {
                Id = productDto.Id,
                Name = productDto.Name,
                Price = productDto.Price,
                Description = productDto.Description
            };
        }
        public static ProductDto ToDto(this Product product)
        {
            return new ProductDto
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
                Description = product.Description
            };
        }
    }
}
