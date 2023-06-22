using MiniProyectoMacBerri.Dto;
using MiniProyectoMacBerri.Models;

namespace MiniProyectoMacBerri.ModelsViews
{
    public class ReserveClientViewModel
    {
        public ReserveDto ReserveDto { get; set; } = null!;
        public Service Service { get; set; } = null!;
    }
}
