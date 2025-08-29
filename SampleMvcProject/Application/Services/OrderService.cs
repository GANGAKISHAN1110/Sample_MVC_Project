using Microsoft.AspNetCore.Http.HttpResults;
using SampleMvcproject.Application.DTOs;
using SampleMvcproject.Domain.Entities;
using SampleMvcProject.Application.DTOs;
using SampleMvcProject.Application.Interfaces;
using SampleMvcProject.Domain.Entities;
using SampleMvcProject.Domain.Repositories;
using System.Security.Cryptography;

namespace SampleMvcProject.Application.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;

        public OrderService(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<IEnumerable<OrderDto>> GetAllAsync()
        {
            var orders = await _orderRepository.GetAllAsync();
            
            return orders.Select(o => new OrderDto
            {
                Id = o.Id,
                OrderDate = o.OrderDate,
                TotalAmount = o.TotalAmount,
                OrderItems = o.OrderItems.Select(Oid => new ProductDto
                {
                    Id = Oid.Id,
                    Name = Oid.Name,
                    Price = Oid.Price,
                    Description = Oid.Description
                }).ToList()
            }).ToList();
        }

        public async Task<OrderDto?> GetByIdAsync(int id)
        {
            var orderDto = await _orderRepository.GetByIdAsync(id);
            if (orderDto == null)
            {
                return null;
            }
            return new OrderDto
            {
                Id = orderDto.Id,
                OrderDate = orderDto.OrderDate,
                TotalAmount = orderDto.TotalAmount,
                OrderItems = orderDto.OrderItems.Select(Oid => new ProductDto
                {
                    Id = Oid.Id,
                    Name = Oid.Name,
                    Price = Oid.Price,
                    Description = Oid.Description
                }).ToList()
            };
        }

        //public async Task AddAsync(OrderDto orderDto)
        //{
        //    var orders = await _orderRepository.AddAsync(orderDto);
                
        //    return new OrderDto
        //    {
        //        Id = orders.Id,
        //        OrderDate = orders.OrderDate,
        //        TotalAmount = orders.TotalAmount,
        //        ProductItems = orders.ProductItems.Select(Oid => new ProductDto
        //        {
        //            Id = Oid.Id,
        //            Name = Oid.Name,
        //            Price = Oid.Price,
        //            Description = Oid.Description
        //        })
        //    };
        //}

        public async Task<OrderDto> UpdateAsync(OrderDto orderDto)
        {
            var existing = await _orderRepository.GetByIdAsync(orderDto.Id);
            if (existing == null)
            {
                return null;
            }
            var newOrder =  _orderRepository.UpdateAsync(existing);
            return new OrderDto
            {
                Id = newOrder.Id,
                OrderDate = orderDto.OrderDate,
                TotalAmount = orderDto.TotalAmount,
                OrderItems = orderDto.OrderItems.Select(Oid => new ProductDto
                {
                    Id = Oid.Id,
                    Name = Oid.Name,
                    Price = Oid.Price,
                    Description = Oid.Description,
                }).ToList()
            };
        }

        public async Task DeleteAsync(int id)
        {
            var existing = await _orderRepository.GetByIdAsync(id);

            if (existing == null)
            {
                return;
            }
            await _orderRepository.DeleteAsync(id);
        }

        public async Task<OrderDto> AddAsync(OrderDto orderDto)
        {
            // convert dto to entity
            var order = new Order
            {
                OrderDate = orderDto.OrderDate,
                TotalAmount = orderDto.TotalAmount,
                OrderItems = orderDto.OrderItems.Select(p => new Product
                {
                    Name = p.Name,
                    Price = p.Price,
                    Description = p.Description
                }).ToList()
            };

            // save to DB and get back entity
            var savedOrder = await _orderRepository.AddAsync(order);

            // map back to dto
            return new OrderDto
            {
                Id = savedOrder.Id,
                OrderDate = savedOrder.OrderDate,
                TotalAmount = savedOrder.TotalAmount,
                OrderItems = savedOrder.OrderItems.Select(Oid => new ProductDto
                {
                    Id = Oid.Id,
                    Name = Oid.Name,
                    Price = Oid.Price,
                    Description = Oid.Description
                }).ToList()
            };
        }
    }
}
