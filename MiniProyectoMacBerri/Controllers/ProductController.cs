using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MiniProyectoMacBerri.Dto;
using MiniProyectoMacBerri.ModelsViews;
using MiniProyectoMacBerri.Services;

namespace MiniProyectoMacBerri.Controllers
{
    [Authorize(Roles = "ADMIN")]
    public class ProductController : Controller
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [Route("/Admin/Products")]
        public async Task<IActionResult> Index()
        {
            var products = await _productService.GetAll();
            return View(products);
        }

        [Route("/Admin/Product/Create")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost("/Admin/Product/Create")]
        public async Task<IActionResult> Create(ProductDto productDto)
        {
            await _productService.AddProduct(productDto);
            return RedirectToAction("Index", "Product");
        }

        [Route("/Admin/Product/Update/{id_product}")]
        public async Task<IActionResult> Update(string id_product)
        {
            var product = await _productService.GetById(id_product);
            if (product == null) return RedirectToAction("Index", "Product");

            ProductViewModel viewModel = new() {
                ProductDto = new ProductDto()
                {
                    Name = product.Name,
                    Description = product.Description,
                    Price = product.Price,
                    UrlImage = product.UrlImage,
                }
            };
            return View(viewModel);
        }

        [HttpPost("/Admin/Product/Update/{id_product}")]
        public async Task<IActionResult> Update(ProductDto productDto, string id_product)
        {
            await _productService.UpdateProduct(productDto, id_product);
            return RedirectToAction("Index","Product");
        }

        [Route("/Admin/Product/Delete/{id_product}")]
        public async Task<IActionResult> Delete(string id_product )
        {
            await _productService.DeleteById(id_product);
            return RedirectToAction("Index","Product");
        }



    }
}
