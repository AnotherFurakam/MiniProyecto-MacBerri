using MiniProyectoMacBerri.Dto;
using MiniProyectoMacBerri.Models;

namespace MiniProyectoMacBerri.ModelsViews
{
    public class UserViewModel
    {
        public List<Rol> roles { get; set; }
        public UserDto userDto { get; set; }
    }
}
