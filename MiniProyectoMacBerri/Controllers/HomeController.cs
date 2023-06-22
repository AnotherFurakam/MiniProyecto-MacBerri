using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MiniProyectoMacBerri.Dto;
using MiniProyectoMacBerri.Models;
using MiniProyectoMacBerri.ModelsViews;
using MiniProyectoMacBerri.Services;
using System.Diagnostics;
using System.Security.Claims;

namespace MiniProyectoMacBerri.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUserService _userService;
        private readonly IProductService _productServices;
        private readonly ICartServices _cartServices;

        public HomeController(ILogger<HomeController> logger, IUserService userService, IProductService productServices, ICartServices cartServices)
        {
            _logger = logger;
            _userService = userService;
            _productServices = productServices;
            _cartServices = cartServices;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Login()
        {
            if (User.IsInRole("ADMIN")) return RedirectToAction("Index", "User");
            if (User.IsInRole("STANDARD")) return RedirectToAction("Index", "Home");

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            var user = await _userService.login(loginDto);
            if (user != null)
            {

                //Creando los claims con los datos del usuario
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, user.IdUser.ToString()),
                    new Claim(ClaimTypes.Name, user.Names),
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim(ClaimTypes.Role, user.IdRolNavigation.Name),
                };

                //Creando el claim identity
                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                //Añadiendo el claimsIdentity al contexto de la aplicación
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));

                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        public async Task<IActionResult> Logout()
        {
            //Cerrando la sesión, eliminando los claims identity del contexto de la aplicación
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }

        /*Products*/

        [Authorize(Roles = "STANDARD")]
        public async Task<IActionResult> Products()
        {
            string id_user = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var products = await _productServices.GetAll();
            var cartItems = await _cartServices.CountProducts(id_user);

            var model = new ProductClientViewModel()
            {
                Products = products,
                CartItems = cartItems
            };

            return View(model);
        }


        /*Services*/

        [Authorize(Roles = "STANDARD")]
        public IActionResult Services()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}