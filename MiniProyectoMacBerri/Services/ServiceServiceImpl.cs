using Microsoft.EntityFrameworkCore;
using MiniProyectoMacBerri.Dto;
using MiniProyectoMacBerri.Models;

namespace MiniProyectoMacBerri.Services
{
    public class ServiceServiceImpl : IServiceService
    {
        private readonly MacberriprojectContext _context;
        public ServiceServiceImpl(MacberriprojectContext context)
        {
            _context = context;
        }
        public async Task AddService(ServiceDto serviceDto)
        {
            var service = new Service()
            {
                Name = serviceDto.Name,
                Description = serviceDto.Description,
                UrlImage = serviceDto.UrlImage,
            };
            await _context.Services.AddAsync(service);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteById(string id_service)
        {
            var service = await _context.Services.FirstOrDefaultAsync(p => p.IdService.ToString().Equals(id_service));
            if (service != null)
            {
                _context.Services.Remove(service);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<Service>> GetAll()
        {
            var serviceList = await _context.Services.ToListAsync();
            return serviceList;
        }

        public async Task<Service?> GetById(string id_service)
        {
            var service = await _context.Services.FirstOrDefaultAsync(p => p.IdService.ToString().Equals(id_service));
            return service;
        }

        public async Task UpdateService(ServiceDto serviceDto, string id_service)
        {
            var service = await _context.Services.FirstOrDefaultAsync(p => p.IdService.ToString().Equals(id_service));
            if (service != null)
            {
                service.Name = serviceDto.Name;
                service.Description = serviceDto.Description;
                service.UrlImage = serviceDto.UrlImage;
            }
            await _context.SaveChangesAsync();
        }
    }
}
