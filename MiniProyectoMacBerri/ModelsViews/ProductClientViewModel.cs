using MiniProyectoMacBerri.Models;

namespace MiniProyectoMacBerri.ModelsViews
{
    public class ProductClientViewModel
    {
        public List<Product> Products { get; set; } = null!;
        public int CartItems { get; set; }
    }
}
