using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using MiniProyectoMacBerri.Dto;
using MiniProyectoMacBerri.ModelsViews;
using MiniProyectoMacBerri.Services;

namespace MiniProyectoMacBerri.Controllers
{
    [Authorize(Roles = "ADMIN")]
    public class ServiceController : Controller
    {
        private readonly IServiceService _serviceService;
        public ServiceController(IServiceService serviceService)
        {
            _serviceService = serviceService;
        }

        [Route("/Admin/Services")]
        public async Task<IActionResult> Index()
        {
            var services = await _serviceService.GetAll();
            return View(services);
        }

        [Route("/Admin/Service/Create")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost("/Admin/Service/Create")]
        public async Task<IActionResult> Create(ServiceDto serviceDto)
        {
            await _serviceService.AddService(serviceDto);
            return RedirectToAction("Index", "Service");
        }

        [Route("/Admin/Service/Update/{id_service}")]
        public async Task<IActionResult> Update(string id_service)
        {
            var service = await _serviceService.GetById(id_service);
            if (service == null) return RedirectToAction("Index","Service");
            ServiceViewModel viewModal = new()
            {
                ServiceDto = new ServiceDto()
                {
                    Name = service.Name,
                    Description = service.Description,
                    UrlImage = service.UrlImage,
                }
            };
            return View(viewModal);
        }

        [HttpPost("/Admin/Service/Update/{id_service}")]
        public async Task<IActionResult> Update(ServiceDto serviceDto, string id_service)
        {
            await _serviceService.UpdateService(serviceDto, id_service);
            return RedirectToAction("Index", "Service");
        }

        [Route("/Admin/Service/Delete/{id_service}")]
        public async Task<IActionResult> Delete(string id_service)
        {
            await _serviceService.DeleteById(id_service);
            return RedirectToAction("Index", "Product");
        }

    }
}
