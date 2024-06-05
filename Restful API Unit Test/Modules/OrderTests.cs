using FluentAssertions;
using Restful_API.Enums;
using Restful_API.Modules;

namespace Restful_API_Unit_Test.Modules
{
    public class OrderTests
    {
        private Order _order;

        [SetUp]
        public void Setup()
        {
            _order = new Order();
        }

        [TestCase(StatusEnum.None)]
        [TestCase(StatusEnum.Waiting)]
        [TestCase(StatusEnum.Assignment)]
        [TestCase(StatusEnum.Repairing)]
        [TestCase(StatusEnum.Processing)]
        public void order_can_be_edited(StatusEnum status)
        {
            _order.Status = status;

            var actual = _order.CanEdited();

            actual.Should().BeTrue();
        }

        [Test]
        public void order_can_not_be_edited_when_is_completed()
        {
            _order.Status = StatusEnum.Completed;

            var actual = _order.CanEdited();

            actual.Should().BeFalse();
        }

        [Test]
        public void check_update_info_will_correct_when_status_is_not_completed()
        {
            _order.Status = StatusEnum.Processing;

            var actual = _order.CheckUpdateInfo();
            var expected = new OrderChecked()
            {
                IsSuccess = true,
            };

            actual.Should().BeEquivalentTo(expected);
        }

        [Test]
        public void check_update_info_will_correct_when_completed_and_all_confirmed()
        {
            _order.Status = StatusEnum.Completed;
            _order.Confirmed.ReporterConfimed = true;
            _order.Confirmed.MaintenanceConfirmed = true;
            _order.Confirmed.HeadquartersConfirmed = true;

            var actual = _order.CheckUpdateInfo();
            var expected = new OrderChecked()
            {
                IsSuccess = true,
            };

            actual.Should().BeEquivalentTo(expected);
        }

        [Test]
        public void check_update_info_incorrect_when_not_confirmed_by_users()
        {
            _order.Status = StatusEnum.Completed;

            var actual = _order.CheckUpdateInfo();

            actual.IsSuccess.Should().BeFalse();
        }
    }
}