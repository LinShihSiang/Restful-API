using Restful_API.Enums;
using Restful_API.Exceptions;

namespace Restful_API.Modules
{
    public class Order
    {
        public long Id { get; set; }
        public OrderContent Content { get; set; }
        public StatusEnum Status { get; set; }
        public OrderConfirmed Confirmed { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? CompletedDate { get; set; }

        public Order()
        {
            Content = new OrderContent();
            Confirmed = new OrderConfirmed();
        }

        public bool CanEdited()
        {
            return Status != StatusEnum.Completed;
        }

        public OrderChecked CheckUpdateInfo()
        {
            if (Status == StatusEnum.Completed)
            {
                if (!Confirmed.CanCompleted())
                {
                    return new OrderChecked()
                    {
                        IsSuccess = false,
                        Message = "The Order Not All Confimed By Users."
                    };
                }
            }

            return new OrderChecked()
            {
                IsSuccess = true,
            };
        }

        public override string ToString()
            => $"Id={Id}, Content={Content}, Status={Status}, Confirmed={Confirmed}";
    }
}