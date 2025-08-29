using SampleMvcproject.Application.DTOs;
using SampleMvcproject.Domain.Entities;
using SampleMvcProject.Application.DTOs;
using SampleMvcProject.Domain.Entities;
using System.Reflection.Metadata.Ecma335;

namespace SampleMvcProject.Application.Mappers
{
    public static class CustomerMapper
    {
        // Dto to Entity
       public static Customer ToEntity(this CustomerDto customerDto)
        {
            return new Customer
            {
                Id = customerDto.Id,
                Name = customerDto.Name,
                Email = customerDto.Email,
                Phone = customerDto.Phone,
                Orders = customerDto.Orders?.Select(orderDto => new Order
                {
                    Id = orderDto.Id,
                    OrderDate = orderDto.OrderDate,
                    CustomerId = orderDto.CustomerId,
                    TotalAmount = orderDto.TotalAmount,
                    OrderItems = orderDto.OrderItems?.Select(productDto => new Product
                    {
                        Id = productDto.Id,
                        Name = productDto.Name,
                        Price = productDto.Price,
                        Description = productDto.Description
                    }).ToList()
                }).ToList()
            };
        }

        // Entity to Dto

        public static CustomerDto ToDto(Customer customer)
        {
            if (customer == null) return null;
            return new CustomerDto
            {
                Id = customer.Id,
                Name = customer.Name,
                Email = customer.Email,
                Phone = customer.Phone,
                Orders = customer.Orders?.Select(orderDto => new OrderDto
                {
                    Id = orderDto.Id,
                    CustomerId = orderDto.CustomerId,
                    OrderDate = orderDto.OrderDate,
                    TotalAmount = orderDto.TotalAmount,
                    OrderItems = orderDto.OrderItems?.Select(productDto => new ProductDto
                    {
                        Id = productDto.Id,
                        Name = productDto.Name,
                        Price = productDto.Price,
                        Description = productDto.Description
                    }).ToList()
                }).ToList()
            };
        }
    }
}
