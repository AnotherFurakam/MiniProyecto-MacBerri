using MiniProyectoMacBerri.Dto;
using MiniProyectoMacBerri.Models;

namespace MiniProyectoMacBerri.Services
{
    public interface IServiceService
    {
        public Task<List<Service>> GetAll();
        public Task<Service?> GetById(string id_service);
        public Task AddService(ServiceDto serviceDto);
        public Task DeleteById(string id_service);
        public Task UpdateService(ServiceDto serviceDto, string id_service);
    }
}
