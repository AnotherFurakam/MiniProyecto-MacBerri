using Microsoft.EntityFrameworkCore;
using MiniProyectoMacBerri.Models;

namespace MiniProyectoMacBerri.Services
{
    public class RoleServiceImpl : IRoleService
    {

        private readonly MacberriprojectContext _context;

        public RoleServiceImpl(MacberriprojectContext context)
        {
            _context = context;
        }

        public async Task<List<Rol>> GetAll()
        {
            var rolList = await _context.Rols.ToListAsync();
            return rolList;
        }
    }
}
