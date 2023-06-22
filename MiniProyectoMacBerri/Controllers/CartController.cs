using Microsoft.AspNetCore.Mvc;
using MiniProyectoMacBerri.Services;
using System.Security.Claims;

namespace MiniProyectoMacBerri.Controllers
{
    public class CartController : Controller
    {
        private readonly ICartServices _cartService;
        public CartController(ICartServices cartServices)
        {
            _cartService = cartServices;
        }

        public async Task<IActionResult> Index()
        {
            string id_user = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var products = await _cartService.ItemsByUserId(id_user);
            return View(products);
        }

        [HttpGet("/AddToCart/{id_product}")]
        public async Task<IActionResult> AddToCart(string id_product)
        {
            string id_user = User.FindFirstValue(ClaimTypes.NameIdentifier);
            await _cartService.AddToCart(id_user, id_product);
            return RedirectToAction("Products", "Home");
        }
    }
}
