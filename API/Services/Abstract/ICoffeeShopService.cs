using API.Models;

namespace API.Services.Abstract
{
    public interface ICoffeeShopService
    {
        Task<List<CoffeeShopModel>> List();

    }
}
