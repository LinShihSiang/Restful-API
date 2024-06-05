using Restful_API.Exceptions;
using Restful_API.Modules;
using Restful_API.Reponses;
using Restful_API.Repositories;

namespace Restful_API.Services
{
    public class OrderService
    {
        private readonly IOrderRepo _orderRepo;

        public OrderService(IOrderRepo orderRepo)
        {
            _orderRepo = orderRepo;
        }

        public BaseResponse Update(Order currentOrder, Order editedOrder)
        {
            var message = string.Empty;

            if (currentOrder.CanEdited())
            {
                var checkResult = editedOrder.CheckUpdateInfo();

                if (!checkResult.IsSuccess)
                    throw new OrderException(checkResult.Message)
                    {
                        Order = editedOrder
                    };

                _orderRepo.Update(editedOrder);
            }
            else
            {
                message = "The Order Had Completed, Can't Updated.";
            }

            return new BaseResponse()
            {
                IsSuccess = string.IsNullOrEmpty(message),
                Message = message
            };
        }

        public OrderExistChecked CheckExist(long id)
        {
            var order = _orderRepo.Get(id);

            if (order == null)
            {
                return new OrderExistChecked
                {
                    Message = "The Order Not Found."
                };
            }

            return new OrderExistChecked
            {
                IsSuccess = true,
                Data = order
            };
        }
    }
}
