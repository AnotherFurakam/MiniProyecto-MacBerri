using Microsoft.AspNetCore.Mvc;
using MiniProyectoMacBerri.Services;
using System.Security.Claims;

namespace MiniProyectoMacBerri.Controllers
{
    public class CartController : Controller
    {
        private readonly ICartServices _cartService;
        private readonly ISaleService _saleService;
        public CartController(ICartServices cartServices, ISaleService saleService)
        {
            _cartService = cartServices;
            _saleService = saleService;
        }

        private string GetCurrentUserId()
        {
            return User.FindFirstValue(ClaimTypes.NameIdentifier);
        }

        public async Task<IActionResult> Index()
        {
            string id_user = GetCurrentUserId();
            var items = await _cartService.ItemsByUserId(id_user);
            if (items.Count == 0) return RedirectToAction("Products","Home");
            return View(items);
        }

        [HttpGet("/AddToCart/{id_product}")]
        public async Task<IActionResult> AddToCart(string id_product)
        {
            string id_user = GetCurrentUserId();
            await _cartService.AddToCart(id_user, id_product);

            string currentPath = HttpContext.Request.Headers["Referer"].ToString().Split("/").Last();
            if (currentPath == "Cart") return RedirectToAction("Index", "Cart");

            return RedirectToAction("Products", "Home");
        }

        [HttpGet("/RemoveToCart/{id_product}")]
        public async Task<IActionResult> RemoveToCart(string id_product)
        {
            string id_user = GetCurrentUserId();
            await _cartService.RemoveToCart(id_user, id_product);

            return RedirectToAction("Index", "Cart");
        }


        [HttpGet("/RemoveFromCart/{id_product}")]
        public async Task<IActionResult> RemoveFromCart(string id_product)
        {
            string id_user = GetCurrentUserId();
            await _cartService.DeleteItem(id_user, id_product);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> MakeSale()
        {
            string id_user = GetCurrentUserId();
            await _saleService.MakeASale(id_user);
            _cartService.ClearCart(id_user);
            return RedirectToAction("Products", "Home");
        }
    }
}
