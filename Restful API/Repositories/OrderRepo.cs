using Newtonsoft.Json;
using Restful_API.Modules;

namespace Restful_API.Repositories
{
    public class OrderRepo : IOrderRepo
    {
        private ILogger<OrderRepo> _logger;

        private string _filePath;

        public OrderRepo(ILogger<OrderRepo> logger)
        {
            _logger = logger;
            _filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data/order.json");
        }

        public void Add(Order order)
        {
            var orders = GetAll().ToList();

            orders.Add(order);

            File.WriteAllText(_filePath, JsonConvert.SerializeObject(orders, Formatting.Indented));
        }

        public void Delete(long id)
        {
            var orders = GetAll().ToList();

            orders.Remove(orders.First(item => item.Id == id));

            File.WriteAllText(_filePath, JsonConvert.SerializeObject(orders, Formatting.Indented));
        }

        public Order Get(long id)
        {
            return GetAll()
                .FirstOrDefault(order => order.Id == id);
        }

        public IEnumerable<Order> GetAll()
        {
            return JsonConvert
                .DeserializeObject<List<Order>>(File.ReadAllText(_filePath));
        }

        public void Update(Order order)
        {
            var orders = GetAll().ToList();
            var index = orders.FindIndex(item => item.Id == order.Id);

            orders[index] = order;

            File.WriteAllText(_filePath, JsonConvert.SerializeObject(orders, Formatting.Indented));
        }
    }
}