using FluentAssertions;
using Restful_API.Enums;
using Restful_API.Modules;

namespace Restful_API_Unit_Test.Modules
{
    public class OrderConfirmedTests
    {
        private OrderConfirmed _orderConfirmed;

        [SetUp]
        public void Setup()
        {
            _orderConfirmed = new OrderConfirmed();
        }

        [Test]
        public void order_can_be_completed()
        {
            _orderConfirmed.ReporterConfimed = true;
            _orderConfirmed.MaintenanceConfirmed = true;
            _orderConfirmed.HeadquartersConfirmed = true;

            var actual = _orderConfirmed.CanCompleted();

            actual.Should().BeTrue();
        }

        [Test]
        public void order_can_not_completed_when_reporter_not_confirmed()
        {
            _orderConfirmed.ReporterConfimed = false;
            _orderConfirmed.MaintenanceConfirmed = true;
            _orderConfirmed.HeadquartersConfirmed = true;

            var actual = _orderConfirmed.CanCompleted();

            actual.Should().BeFalse();
        }

        [Test]
        public void order_can_not_completed_when_maintenace_not_confirmed()
        {
            _orderConfirmed.ReporterConfimed = true;
            _orderConfirmed.MaintenanceConfirmed = false;
            _orderConfirmed.HeadquartersConfirmed = true;

            var actual = _orderConfirmed.CanCompleted();

            actual.Should().BeFalse();
        }

        [Test]
        public void order_can_not_completed_when_headquarters_not_confirmed()
        {
            _orderConfirmed.ReporterConfimed = true;
            _orderConfirmed.MaintenanceConfirmed = true;
            _orderConfirmed.HeadquartersConfirmed = false;

            var actual = _orderConfirmed.CanCompleted();

            actual.Should().BeFalse();
        }
    }
}