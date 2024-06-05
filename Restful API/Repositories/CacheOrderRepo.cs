using Microsoft.Extensions.Caching.Memory;
using Restful_API.Modules;
using System.Collections.Generic;

namespace Restful_API.Repositories
{
    public class CacheOrderRepo : IOrderRepo
    {
        private static string ORDER_KEY = "Order_Cache";

        private readonly IOrderRepo _orderRepo;
        private readonly IMemoryCache _memroyCache;

        public CacheOrderRepo(
            IOrderRepo orderRepo,
            IMemoryCache memoryCache)
        {
            _orderRepo = orderRepo;
            _memroyCache = memoryCache;
        }

        public void Add(Order order)
        {
            _memroyCache.Remove(ORDER_KEY);
            _orderRepo.Add(order);
        }

        public void Delete(long id)
        {
            _memroyCache.Remove(ORDER_KEY);
            _orderRepo.Delete(id);
        }

        public Order Get(long id)
        {
            return GetAll()
                .FirstOrDefault(order => order.Id == id);
        }

        public IEnumerable<Order> GetAll()
        {
            IEnumerable<Order> orders = Enumerable.Empty<Order>();

            if (_memroyCache.TryGetValue(ORDER_KEY, out orders))
            {
                return orders;
            }
            else
            {
                orders = _orderRepo.GetAll();

                _memroyCache.Set(ORDER_KEY, orders,
                    new MemoryCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromHours(1)));

                return orders;
            }
        }

        public void Update(Order order)
        {
            _memroyCache.Remove(ORDER_KEY);
            _orderRepo.Update(order);
        }
    }
}
