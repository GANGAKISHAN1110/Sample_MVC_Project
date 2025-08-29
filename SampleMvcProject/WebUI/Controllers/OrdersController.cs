using Microsoft.AspNetCore.Mvc;
using SampleMvcProject.Application.DTOs;
using SampleMvcProject.Application.Interfaces;
using SampleMvcProject.Application.Services;

namespace SampleMvcProject.Web.Controllers
{
    public class OrdersController : Controller
    {
        private readonly IOrderService _orderService;

        public OrdersController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        public async Task<IActionResult> Index()
        {
            var orders = await _orderService.GetAllAsync();
            return View(orders);
        }

        public async Task<IActionResult> Details(int id)
        {
            var order = await _orderService.GetByIdAsync(id);
            if (order == null) return NotFound();
            return View(order);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(OrderDto orderDto)
        {
            if (ModelState.IsValid)
            {
                await _orderService.AddAsync(orderDto);
                return RedirectToAction(nameof(Index));
            }
            return View(orderDto);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var order = await _orderService.GetByIdAsync(id);
            if (order == null) return NotFound();
            return View(order);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(OrderDto orderDto)
        {
            if (ModelState.IsValid)
            {
                await _orderService.UpdateAsync(orderDto);
                return RedirectToAction(nameof(Index));
            }
            return View(orderDto);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var order = await _orderService.GetByIdAsync(id);
            if (order == null) return NotFound();
            return View(order);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _orderService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
