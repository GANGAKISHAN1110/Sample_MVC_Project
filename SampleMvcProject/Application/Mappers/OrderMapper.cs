using SampleMvcproject.Application.DTOs;
using SampleMvcproject.Domain.Entities;
using SampleMvcProject.Application.DTOs;
using SampleMvcProject.Domain.Entities;

namespace SampleMvcProject.Application.Mappings
{
    public static class OrderMapper
    {
        public static Order ToEntity(this OrderDto dto)
        {
            return new Order
            {
                Id = dto.Id,
                OrderDate = dto.OrderDate,
                CustomerId = dto.CustomerId,
                TotalAmount = dto.TotalAmount,
                OrderItems = dto.OrderItems.Select(productDto => new Product
                {
                    Id = productDto.Id,
                    Name = productDto.Name,
                    Price = productDto.Price,
                    Description = productDto.Description
                }).ToList()
            };
        }

        public static OrderDto ToDto(this Order order)
        {
            return new OrderDto
            {
                Id = order.Id,
                OrderDate = order.OrderDate,
                CustomerId = order.CustomerId,
                TotalAmount = order.TotalAmount,
                CustomerDto = order.Customer == null ? null : new CustomerDto
                {
                    Id = order.Customer.Id,
                    Name = order.Customer.Name,
                    Email = order.Customer.Email,
                    Phone = order.Customer.Phone
                },
                OrderItems = order.OrderItems.Select(product => new ProductDto
                {
                    Id = product.Id,
                    Name = product.Name,
                    Price = product.Price,
                    Description = product.Description
                }).ToList()
            };
        }
    }
}
