using MiniProyectoMacBerri.Models;

namespace MiniProyectoMacBerri.Services
{
    public interface IRoleService
    {
        public Task<List<Rol>> GetAll();
    }
}
