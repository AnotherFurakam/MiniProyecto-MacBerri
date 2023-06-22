using Microsoft.AspNetCore.Mvc;
using MiniProyectoMacBerri.Dto;
using MiniProyectoMacBerri.ModelsViews;
using MiniProyectoMacBerri.Services;
using System.Security.Claims;

namespace MiniProyectoMacBerri.Controllers
{
    public class ReserveController : Controller
    {
        private readonly IServiceService _serviceService;
        private readonly IReserveService _reserveService;
        public ReserveController(IServiceService serviceService, IReserveService reserveService)
        {
            _serviceService = serviceService;
            _reserveService = reserveService;
        }
        private string GetCurrentUserId()
        {
            return User.FindFirstValue(ClaimTypes.NameIdentifier);
        }



        [HttpGet("/Reserve/{id_service}")]
        public async Task<IActionResult> Index(string id_service)
        {
            var service = await _serviceService.GetById(id_service);
            var model = new ReserveClientViewModel()
            {
                Service = service            };
            return View(model);
        }

        [HttpPost("/Reserve/{id_service}")]
        public async Task<IActionResult> Index(ReserveDto reserveDto, string id_service)
        {
            try
            {
                string id_user = GetCurrentUserId();
                await _reserveService.MakeAReserve(reserveDto, id_service, id_user);
                return RedirectToAction("MyReserve", "Reserve");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("ReserveDto.LimitDate", ex.Message);
                var service = await _serviceService.GetById(id_service);
                var model = new ReserveClientViewModel()
                {
                    Service = service,
                    ReserveDto = reserveDto                };
                return View(model);
            }
        }

        [HttpGet("/Reserve/MyReserve")]
        public async Task<IActionResult> MyReserve()
        {
            var id_user = GetCurrentUserId();
            var reserves = await _reserveService.GetAll(id_user);
            return View(reserves);
        }
    }
}
