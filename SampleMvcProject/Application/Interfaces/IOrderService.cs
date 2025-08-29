using SampleMvcProject.Application.DTOs;
using SampleMvcProject.Domain.Entities;

namespace SampleMvcProject.Application.Interfaces
{
    public interface IOrderService
    {
        Task<IEnumerable<OrderDto>> GetAllAsync();
        Task<OrderDto?> GetByIdAsync(int id);
        Task<OrderDto> AddAsync(OrderDto orderDto);
        Task<OrderDto> UpdateAsync(OrderDto orderDto);
        Task DeleteAsync(int id);
    }
}
