using MiniProyectoMacBerri.Models;

namespace MiniProyectoMacBerri.Services
{
    public interface ICartServices
    {
        public Task<int> CountProducts(string id_user);

        public Task AddToCart(string id_user, string id_product);
        public Task RemoveToCart(string id_user, string id_product);
        public void ClearCart(string id_user);
        public Task<List<Shopcart>> ItemsByUserId(string id_user);
        public Task DeleteItem(string id_user, string id_product);
    }
}
