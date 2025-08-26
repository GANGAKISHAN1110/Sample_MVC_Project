using Microsoft.AspNetCore.Mvc;
using SampleMvcproject.Application.DTOs;
using SampleMvcproject.Application.Interfaces;

namespace SampleMvcProject.WebUI.Controllers
{
    public class ProductsController : Controller
    {

        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }
        public async Task<IActionResult> Index()
        {
            var items = await _productService.GetAllAsync();
            return View(items);
        }

        public async Task<IActionResult> Details(int id)
        {
            var p = await _productService.GetByIdAsync(id);
            if(p == null)
            {
                return NotFound();
            }
            return View(p);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProductDto productDto)
        {
            if (!ModelState.IsValid) return View(productDto);

            await _productService.AddAsync(productDto);

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id, ProductDto product)
        {
            if (id != product.Id) return BadRequest();

            if (!ModelState.IsValid) return View(product);

            await _productService.UpdateAsync(product);

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> DeleteAsync(int id)
        {
            var p = await _productService.GetByIdAsync(id);
            if (p == null) return NotFound();
            await _productService.DeleteAsync(id);
            return View(p);
        }

        [HttpPost, ActionName("Delete"), ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            _productService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
