using MiniProyectoMacBerri.Models;

namespace MiniProyectoMacBerri.Services
{
    public interface ICartServices
    {
        public Task<int> CountProducts(string id_user);

        public Task AddToCart(string id_user, string id_product);
        public void ClearCart(string id_user);
        public Task<List<Product>> ItemsByUserId(string id_user);
    }
}
