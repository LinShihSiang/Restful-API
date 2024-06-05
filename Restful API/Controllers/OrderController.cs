using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Restful_API.Exceptions;
using Restful_API.Modules;
using Restful_API.Reponses;
using Restful_API.Repositories;
using Restful_API.Services;

namespace Restful_API.Controllers
{
    [ApiController]
    [Route("api/order")]
    [Tags("Order 管理")]
    public class OrderController : ControllerBase
    {
        private readonly ILogger<OrderController> _logger;
        private readonly IOrderRepo _orderRepo;
        private readonly OrderService _orderService;

        public OrderController(
            ILogger<OrderController> logger,
            IOrderRepo orderRepo,
            OrderService orderService)
        {
            _logger = logger;
            _orderRepo = orderRepo;
            _orderService = orderService;
        }

        [HttpGet("{id}")]
        public ActionResult<OrderResponse> Get(long id)
        {
            var order = _orderRepo.Get(id);

            if (order == null)
            {
                return NotFound(new OrderResponse("The Order Not Found."));
            }

            return Ok(new OrderResponse(order));
        }

        [HttpPost("")]
        public ActionResult<BaseResponse> Add(Order order)
        {
            try
            {
                var checkedResult = _orderService.CheckExist(order.Id);

                if (checkedResult.IsSuccess)
                {
                    return Ok(new BaseResponse()
                    {
                        Message = "The Order Has Exist."
                    });
                }

                _orderRepo.Add(order);

                return Ok(new BaseResponse()
                {
                    IsSuccess = true,
                });
            }
            catch (Exception ex)
            {
                var result = new BaseResponse()
                {
                    Message = "Add Order Fail."
                };

                _logger.LogError(ex, result.Message);

                return Problem(detail: result.Message);
            }
        }

        [HttpPut("{id}")]
        public ActionResult<BaseResponse> Update(long id, Order editedOrder)
        {
            try
            {
                var checkedResult = _orderService.CheckExist(id);

                if (!checkedResult.IsSuccess)
                {
                    return NotFound(new BaseResponse()
                    {
                        Message = checkedResult.Message
                    });
                }

                return Ok(_orderService.Update(checkedResult.Data, editedOrder));
            }
            catch (OrderException ex)
            {
                _logger.LogError(ex, ex.Message);

                return Ok(new BaseResponse()
                {
                    Message = ex.Message
                });
            }
            catch (Exception ex)
            {
                var result = new BaseResponse()
                {
                    Message = "Update Order Fail."
                };

                _logger.LogError(ex, result.Message);

                return Problem(detail: result.Message);
            }
        }

        [HttpDelete("{id}")]
        public ActionResult<BaseResponse> Delete(long id)
        {
            try
            {
                var checkedResult = _orderService.CheckExist(id);

                if (!checkedResult.IsSuccess)
                {
                    return NotFound(new BaseResponse()
                    {
                        Message = checkedResult.Message
                    });
                }

                _orderRepo.Delete(id);

                return Ok(new BaseResponse()
                {
                    IsSuccess = true,
                });
            }
            catch (Exception ex)
            {
                var result = new BaseResponse()
                {
                    Message = "Delete Order Fail."
                };

                _logger.LogError(ex, result.Message);

                return Problem(detail: result.Message);
            }
        }
    }
}
