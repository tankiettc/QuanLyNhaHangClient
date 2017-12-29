using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyBanHangAPI.model
{
    public class Order
    {
        public int OrderId { get; set; }
        public int TableId { get; set; }
        public virtual Table Table { get; set; }
        public DateTime CreatedDate { get; set; }
        public virtual ICollection<FoodWithOrder> FoodWithOrders { get; set; }
        public decimal BillMoney
        {
            get
            {
                decimal billMoney = 0;
                if(FoodWithOrders == null) {
                    return billMoney;
                }
                foreach (var foodWithOrder in FoodWithOrders)
                {
                    billMoney += foodWithOrder.GetTotalPrice();
                }
                return billMoney;
            }
        }
        public decimal MoneyReceive { get; set; }
        public decimal MoneyReturn { get; set; }
        public String Note { get; set; }
        public Order()
        {

        }

        public Order(int orderId)
        {
            OrderId = orderId;
        }


        public override string ToString()
        {
            return $"{nameof(OrderId)}: {OrderId}, {nameof(TableId)}: {TableId}, {nameof(Table)}: {Table}, {nameof(CreatedDate)}: {CreatedDate}, {nameof(FoodWithOrders)}: {FoodWithOrders}, {nameof(BillMoney)}: {BillMoney}, {nameof(MoneyReceive)}: {MoneyReceive}, {nameof(MoneyReturn)}: {MoneyReturn}, {nameof(Note)}: {Note}";
        }
    }
}
