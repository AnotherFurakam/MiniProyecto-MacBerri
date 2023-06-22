using Microsoft.AspNetCore.Mvc;
using MiniProyectoMacBerri.Dto;
using MiniProyectoMacBerri.ModelsViews;
using MiniProyectoMacBerri.Services;

namespace MiniProyectoMacBerri.Controllers
{
    public class RecoveryController : Controller
    {
        private readonly IUserService _userService;

        public RecoveryController(IUserService userService)
        {
            _userService = userService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(RecoveryEmailDto emailDto)
        {
            var user = await _userService.GetByEmail(emailDto.Email);
            if (user == null)
            {
                ModelState.AddModelError("Email","Correo no encontrado");
                return View(emailDto);
            }
            return RedirectToAction("ChangePassword", "Recovery", new {id_user = user.IdUser});
        }

        [HttpPost("/Recovery/ChangePassword/{id_user}")]
        public async Task<IActionResult> ChangePassword(RecoveryPasswordDto recoveryPasswordDto, string id_user)
        {
            await _userService.ChangePassword(recoveryPasswordDto.Password, id_user);
            return RedirectToAction("Login", "Home");
        }

        [Route("/Recovery/ChangePassword/{id_user}")]
        public async Task<IActionResult> ChangePassword(string id_user)
        {
            var user = await _userService.GetById(id_user);
            RecoveryViewModel model = new()
            {
                User = user,
                RecoveryPasswordDto = new RecoveryPasswordDto()
            };
            return View(model);
        }
    }
}
