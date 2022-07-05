using API.Models;
using API.Services.Abstract;
using DataAccess.Data;
using Microsoft.EntityFrameworkCore;

namespace API.Services.Concrete
{
    public class CoffeeShopService : ICoffeeShopService
    {
        private readonly ApplicationDbContext _dbContext;

        public CoffeeShopService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<CoffeeShopModel>> List()
        {
            var coffeeShops = await (from shop in _dbContext.CoffeeShops
                                     select new CoffeeShopModel()
                                     {
                                         Id=shop.Id,
                                         Name=shop.Name,
                                         OpeningHours=shop.OpeningHours,
                                         Address=shop.Address
                                     }
                                     ).ToListAsync();
            return coffeeShops;
        }
    }
}
