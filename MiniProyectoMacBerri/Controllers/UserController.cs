using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MiniProyectoMacBerri.Dto;
using MiniProyectoMacBerri.ModelsViews;
using MiniProyectoMacBerri.Services;

namespace MiniProyectoMacBerri.Controllers
{
    [Authorize(Roles = "ADMIN")]
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        private readonly IRoleService _roleService;
        public UserController(IUserService userService, IRoleService roleService)
        {
            _userService = userService;
            _roleService = roleService;
        }

        [Route("/Admin/Users")]
        public async Task<IActionResult> Index()
        {
            var users = await _userService.getAll();
            return View(users);
        }

        [Route("/Admin/Create")]
        public async Task<IActionResult> Create()
        {
            var rolList = await _roleService.GetAll();
            var userModelView = new UserViewModel
            {
                roles = rolList,
            };
            return View(userModelView);
        }


        [HttpPost("/Admin/Create")]
        public async Task<IActionResult> Create(UserDto userDto)
        {
            await _userService.AddUser(userDto);
            return RedirectToAction("Index", "User");
        }

        [HttpPost("/Admin/Update/{id_user}")]
        public async Task<IActionResult> Update(UserDto userDto, string id_user)
        {
            await _userService.UpdateUser(userDto, id_user);
            return RedirectToAction("Index", "User");
        }

        [Route("/Admin/Update/{id_user}")]
        public async Task<IActionResult> Update(string id_user)
        {
            var rolList = await _roleService.GetAll();
            var user = await _userService.GetById(id_user);
            var userDto = new UserDto
            {
                Names = user.Names,
                Lastnames = user.Lastnames,
                Email = user.Email,
                Password = user.Password,
                Id_rol = user.IdRol
            };
            var userModelView = new UserViewModel
            {
                roles = rolList,
                userDto = userDto,

            };
            return View(userModelView);
        }

        [Route("/Admin/Delete/{id_user}")]
        public async Task<IActionResult> Delete(string id_user)
        {
            await _userService.DeleteById(id_user);
            return RedirectToAction("Index","User");
        }


    }
}
