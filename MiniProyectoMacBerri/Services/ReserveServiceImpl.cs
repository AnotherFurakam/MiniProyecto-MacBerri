using Microsoft.EntityFrameworkCore;
using MiniProyectoMacBerri.Dto;
using MiniProyectoMacBerri.Models;

namespace MiniProyectoMacBerri.Services
{
    public class ReserveServiceImpl : IReserveService
    {
        private readonly MacberriprojectContext _context;
        public ReserveServiceImpl(MacberriprojectContext context)
        {
            _context = context;
        }

        public async Task<List<Reserve>> GetAll(string id_user)
        {
            var reserveList = await _context.Reserves.Where(r => r.IdUser.ToString().Equals(id_user)).Include(r => r.IdServiceNavigation).ToListAsync();
            return reserveList;
        }

        public async Task MakeAReserve(ReserveDto reserveDto, string id_service, string id_user)
        {
            var existReserve = await _context.Reserves.FirstOrDefaultAsync(r => r.IdService.ToString().Equals(id_service) && r.LimitDate == reserveDto.LimitDate);

            if (existReserve != null) throw new Exception("Servicio reservado en esa fecha");

            var reserve = new Reserve()
            {
                IdService = Guid.Parse(id_service),
                IdUser = Guid.Parse(id_user),
                LimitDate = reserveDto.LimitDate,
            };
            await _context.Reserves.AddAsync(reserve);
            await _context.SaveChangesAsync();
        }
    }
}
