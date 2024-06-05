using Restful_API.Modules;

namespace Restful_API.Repositories
{
    public interface IOrderRepo
    {
        void Add(Order order);
        void Delete(long id);
        Order Get(long id);
        IEnumerable<Order> GetAll();
        void Update(Order order);
    }
}