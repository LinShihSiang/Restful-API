using FluentAssertions;
using NSubstitute;
using Restful_API.Enums;
using Restful_API.Exceptions;
using Restful_API.Modules;
using Restful_API.Repositories;
using Restful_API.Services;

namespace Restful_API_Unit_Test.Services
{
    public class OrderServiceTests
    {
        private IOrderRepo _orderRepo;
        private OrderService _orderService;

        [SetUp]
        public void Setup()
        {
            _orderRepo = Substitute.For<IOrderRepo>();
            _orderService = new OrderService(_orderRepo);
        }

        [Test]
        public void check_order_when_has_data()
        {
            _orderRepo
                .Get(default)
                .ReturnsForAnyArgs(
                    new Order()
                    {
                        Id = 1,
                        Status = StatusEnum.Waiting
                    });

            var actual = _orderService.CheckExist(1);
            var expected = new OrderExistChecked()
            {
                IsSuccess = true,
                Message = string.Empty,
                Data = new Order()
                {
                    Id = 1,
                    Status = StatusEnum.Waiting
                }
            };

            actual.Should().BeEquivalentTo(expected);
        }

        [Test]
        public void check_order_when_no_data()
        {
            _orderRepo
                .Get(default)
                .ReturnsForAnyArgs(default(Order));

            var actual = _orderService.CheckExist(1);

            actual.IsSuccess.Should().BeFalse();
        }

        [Test]
        public void order_can_not_updated_when_status_is_completed()
        {
            var currentOrder = new Order()
            {
                Id = 1,
                Status = StatusEnum.Completed
            };

            var actual = _orderService.Update(currentOrder, new Order());

            actual.IsSuccess.Should().BeFalse();
        }

        [Test]
        public void order_updated_when_has_correct_info()
        {
            var currentOrder = new Order()
            {
                Id = 1,
                Status = StatusEnum.Waiting
            };
            var editedOrder = new Order()
            {
                Id = 1,
                Status = StatusEnum.Repairing
            };

            var actual = _orderService.Update(currentOrder, editedOrder);

            actual.IsSuccess.Should().BeTrue();

            _orderRepo
                .Received(1)
                .Update(Arg.Is<Order>(x => x.Id == 1));
        }

        [Test]
        public void order_updated_when_has_incorrect_info()
        {
            var currentOrder = new Order()
            {
                Id = 1,
                Status = StatusEnum.Waiting
            };
            var editedOrder = new Order()
            {
                Id = 1,
                Status = StatusEnum.Completed
            };

            var action = () => _orderService.Update(currentOrder, editedOrder);

            action.Should().Throw<OrderException>()
                .Where(ex => ex.Order.Status == editedOrder.Status);
        }
    }
}
