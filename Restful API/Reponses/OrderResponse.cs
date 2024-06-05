using Restful_API.Modules;

namespace Restful_API.Reponses
{
    public class OrderResponse : BaseResponse
    {
        public Order? Data { get; set; }

        public OrderResponse(Order order)
        {
            Data = order;
        }

        public OrderResponse(string message)
        {
            Message = message;
        }
    }
}
