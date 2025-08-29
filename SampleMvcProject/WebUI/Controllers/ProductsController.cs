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
        // GET: Products
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var items = await _productService.GetAllAsync();
            return View(items);
        }
        // GET: Products/Details/5
        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var p = await _productService.GetByIdAsync(id);
            if(p == null)
            {
                return NotFound();
            }
            return View(p);
        }
        // GET: Products/Create
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        // POST: Products/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProductDto productDto)
        {
            if (!ModelState.IsValid) return View(productDto);

            await _productService.AddAsync(productDto);

            return RedirectToAction(nameof(Index));
        }
        // GET: Products/Edit/5
        [HttpGet]

        public async Task<IActionResult> Edit(int id)
        {
            var product = await _productService.GetByIdAsync(id);
            if (product == null) return NotFound();
            return View(product);
        }
        // POST: Products/Edit/5
        [HttpPost]
        public async Task<IActionResult> Edit(int id, ProductDto product)
        {
            if (id != product.Id) return BadRequest();

            if (!ModelState.IsValid) return View(product);

            await _productService.UpdateAsync(product);

            return RedirectToAction(nameof(Index));
        }
        // GET: Products/Delete/5
        [HttpGet]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var p = await _productService.GetByIdAsync(id);
            if (p == null) return NotFound();
            await _productService.DeleteAsync(id);
            return View(p);
        }
        // POST: Products/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            _productService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
