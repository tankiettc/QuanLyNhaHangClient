using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyBanHangAPI.model
{
    public class FoodWithOrder
    {
        public int FoodWithOrderId { get; set; }
        public int OrderId { get; set; }
        public virtual Order Order { get; set; }
        public int FoodId { get; set; }
        public virtual Food Food { get; set; }
        public int Quantities { get; set; }
        public FoodWithOrder()
        {

        }

        public FoodWithOrder(int foodWithOrderId)
        {
            FoodWithOrderId = foodWithOrderId;
        }

        public override string ToString()
        {
            return $"{nameof(FoodWithOrderId)}: {FoodWithOrderId}, {nameof(OrderId)}: {OrderId}, {nameof(Order)}: {Order}, {nameof(FoodId)}: {FoodId}, {nameof(Food)}: {Food}, {nameof(Quantities)}: {Quantities}";
        }

        public decimal GetTotalPrice()
        {
            return Food.Price * Quantities;
        }
    }
}
