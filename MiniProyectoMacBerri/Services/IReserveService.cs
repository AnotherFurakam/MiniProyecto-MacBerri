using MiniProyectoMacBerri.Dto;
using MiniProyectoMacBerri.Models;

namespace MiniProyectoMacBerri.Services
{
    public interface IReserveService
    {
        public Task MakeAReserve(ReserveDto reserveDto, string id_service, string id_user);
        public Task<List<Reserve>> GetAll(string id_user);
    }
}
